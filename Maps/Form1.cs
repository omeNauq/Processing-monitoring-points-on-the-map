using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Maps.CalculateProcessing;
using System.Device.Location;
using Maps.ApiProcessing;

namespace Maps
{
    public partial class Form1 : Form
    {
        #region Properties
        private bool isAddingMarker = false; // Kiểm soát trạng thái thêm marker
        private GMapOverlay markersOverlay = new GMapOverlay("MARKERS"); // Overlay chứa marker

        private bool isDrawingRoute = false; // Kiểm soát trạng thái vẽ route
        private List<PointLatLng> pointRoute = new List<PointLatLng>(); // Danh sách điểm để vẽ route
        private GMapOverlay routesOverlay = new GMapOverlay("ROUTES"); // Overlay chứa route

        private bool isDrawingPolygon = false; // Kiểm soát trạng thái vẽ polygon
        private List<PointLatLng> pointPolygon = new List<PointLatLng>(); // Danh sách điểm để vẽ polygon
        private GMapOverlay PolygonOverlay = new GMapOverlay("Polygon"); // Overlay chứa polygon

        private GMapOverlay searchOverlay = new GMapOverlay("Search"); // Overlay chứa marker tìm kiếm

        private double totalDistance = 0; // Tổng khoảng cách của route
        private double totalArea = 0; // Tổng diện tích của polygon

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cấu hình bản đồ
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.Dock = DockStyle.Fill;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            gMapControl1.Position = new PointLatLng(21.028511, 105.804817);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 40;

            // Cấu hình thanh zoom
            hScrollBar1.Maximum = (int)gMapControl1.MaxZoom;
            hScrollBar1.Minimum = (int)gMapControl1.MinZoom;

            // Thêm Overlay vào bản đồ
            gMapControl1.Overlays.Add(routesOverlay);
            gMapControl1.Overlays.Add(PolygonOverlay);
            gMapControl1.Overlays.Add(searchOverlay);
            gMapControl1.Overlays.Add(markersOverlay);

            // Hiển thị placeholder cho textbox
            textBox1.Tag = true;
            textBox1.Text = "Nhập vĩ độ...";
            textBox1.ForeColor = Color.Gray;

            textBox2.Tag = true;
            textBox2.Text = "Nhập kinh độ...";
            textBox2.ForeColor = Color.Gray;


        }

        // Setup click chuột
        private void gMapControl1_MouseClick_1(object sender, MouseEventArgs e)
        {
            // Điều kiện vẽ route
            if (isDrawingRoute && e.Button == MouseButtons.Left)
            {
                PointLatLng PointLatLng = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                pointRoute.Add(PointLatLng);

                UpdateRoute();
            }
            if (isDrawingRoute && e.Button == MouseButtons.Right)
            {
                if (pointRoute.Count > 0)
                {
                    // Xóa điểm cuối cùng
                    pointRoute.RemoveAt(pointRoute.Count - 1);

                    // Xóa toàn bộ route và marker cũ
                    routesOverlay.Routes.Clear();
                    routesOverlay.Markers.Clear();

                    // Nếu còn đủ điểm thì vẽ lại route mới
                    if (pointRoute.Count >= 2)
                    {
                        UpdateRoute();
                    }
                    else if (pointRoute.Count == 1)
                    {
                        // Nếu chỉ còn 1 điểm, chỉ hiển thị marker mà không vẽ route
                        var marker = new GMarkerGoogle(pointRoute[0], GMarkerGoogleType.blue_pushpin);
                        routesOverlay.Markers.Add(marker);
                        ToTal_Distance.Text = "Total Distance: 0 km";
                    }

                    // Nếu không còn điểm nào thì tắt chế độ vẽ route
                    if (pointRoute.Count < 1)
                    {
                        MessageBox.Show("Đã xóa hết route, vui lòng chọn lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            // Điều kiện vẽ polygon
            if (isDrawingPolygon && e.Button == MouseButtons.Left)
            {
                PointLatLng PointLatLng = gMapControl1.FromLocalToLatLng(e.X, e.Y);

                pointPolygon.Add(PointLatLng);
                UpdatePolygon();
            }
            if (isDrawingPolygon && e.Button == MouseButtons.Right)
            {
                if (pointPolygon.Count > 0)
                {
                    PolygonOverlay.Polygons.RemoveAt(PolygonOverlay.Polygons.Count - 1);
                    pointPolygon.RemoveAt(pointPolygon.Count - 1);
                    PolygonOverlay.Polygons.RemoveAt(PolygonOverlay.Polygons.Count - 1);

                    if (pointPolygon.Count > 2)
                    {
                        UpdatePolygon();
                    }
                    else if (pointPolygon.Count == 2)
                    {
                        // Nếu chỉ còn 2 điểm, chỉ hiển thị marker mà không vẽ polygon
                        var marker = new GMarkerGoogle(pointPolygon[0], GMarkerGoogleType.blue_pushpin);
                        PolygonOverlay.Markers.Add(marker);
                        ToTal_Distance.Text = "Total Area: 0 km²";
                    }
                    if (PolygonOverlay.Polygons.Count < 1)
                    {
                        MessageBox.Show("Đã xóa hết polygon, vui lòng chọn lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //PolygonOverlay.Markers.Clear();
                    }
                }
            }

            // Thêm marker
            PointLatLng clickedPoint = gMapControl1.FromLocalToLatLng(e.X, e.Y);

            if (e.Button == MouseButtons.Left && isAddingMarker)
            {
                // Thêm marker nếu đang ở chế độ thêm marker
                GMarkerGoogle marker = new GMarkerGoogle(clickedPoint, GMarkerGoogleType.blue_pushpin);
                // Gọi API thời tiết và hiển thị lên Tooltip
                Task.Run(async () =>
                {
                    string weatherInfo = await API.GetWeatherInfo(clickedPoint.Lat, clickedPoint.Lng);
                    this.Invoke(new Action(() =>
                    {
                        marker.ToolTipText = weatherInfo; // Hiển thị thông tin thời tiết trên Tooltip
                        marker.ToolTipMode = MarkerTooltipMode.Always; // Hiển thị luôn Tooltip
                    }));
                });
                markersOverlay.Markers.Add(marker);
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Xóa marker mới nhất (marker cuối cùng trong danh sách)
                if (markersOverlay.Markers.Count > 0)
                {
                    markersOverlay.Markers.RemoveAt(markersOverlay.Markers.Count - 1);
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                // Xóa tất cả marker
                markersOverlay.Clear();
            }
            // update map
            gMapControl1.Update();
            gMapControl1.Invalidate();
        }

        // Thêm thanh trượt zoom map
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            gMapControl1.Zoom = hScrollBar1.Value;
        }

        // Setup trạng thái để kiểm tra button on/off
        private void button_marker_Click(object sender, EventArgs e)
        {
            isAddingMarker = !isAddingMarker; // Đảo trạng thái
            button_marker.Text = isAddingMarker ? "Cancel" : "Marker"; // Đổi màu nút
        }

        #region routes
        // Setup button route click
        private void button_route_Click(object sender, EventArgs e)
        {
            isDrawingRoute = !isDrawingRoute; // Đảo trạng thái
            button_route.Text = isDrawingRoute ? "Cancel" : "Route"; // Đổi màu nút
            if (isDrawingRoute)
            {
                // Bắt đầu vẽ route, nếu ko có đoạn code này thì khi vẽ route lần 2 sẽ k xóa route cũ
                pointRoute.Clear();
                routesOverlay.Clear();
                ToTal_Distance.Text = "Total Distance: 0 km";
            }
            else
            {
                // Xóa route khỏi bản đồ
                routesOverlay.Clear();
                ToTal_Distance.Text = "Total Distance: 0 km";
            }

            gMapControl1.Update();
            gMapControl1.Invalidate();
        }

        // Update Route và thêm condition vẽ route
        private void UpdateRoute()
        {
            if (pointRoute.Count > 1)
            {
                // Tạo route mới
                GMapRoute route = new GMapRoute(pointRoute, "Route line")
                {
                    Stroke = new Pen(Color.Red, 4)
                };
                routesOverlay.Routes.Add(route);

                // Tính toán tổng khoảng cách
                totalDistance = DistanceCalculator.CalculateTotalDistance(pointRoute);

                // Duyệt qua từng đoạn để tính khoảng cách và đặt label
                for (int i = 0; i < pointRoute.Count - 1; i++)
                {
                    // Tính khoảng cách giữa hai điểm
                    GeoCoordinate start = new GeoCoordinate(pointRoute[i].Lat, pointRoute[i].Lng);
                    GeoCoordinate end = new GeoCoordinate(pointRoute[i + 1].Lat, pointRoute[i + 1].Lng);
                    double segmentDistance = start.GetDistanceTo(end); // Mét

                    // Tìm điểm chính giữa đoạn thẳng
                    double midLat = (pointRoute[i].Lat + pointRoute[i + 1].Lat) / 2;
                    double midLng = (pointRoute[i].Lng + pointRoute[i + 1].Lng) / 2;
                    PointLatLng midPoint = new PointLatLng(midLat, midLng);

                    // Chuyển sang km và tạo text marker hiển thị khoảng cách
                    string distanceText = $"{(segmentDistance / 1000):F2} km";
                    GMapTextMarker textMarker = new GMapTextMarker(midPoint, distanceText);
                    routesOverlay.Markers.Add(textMarker);
                }
                // Cập nhật tổng khoảng cách lên TextBox (chuyển sang km)
                ToTal_Distance.Text = $"Total Distance: {(totalDistance):F2} km";
            }
            else
            {
                // Nếu không đủ điểm để tạo route
                ToTal_Distance.Text = "Total Distance: 0 km";
            }
            gMapControl1.Update();
            gMapControl1.Invalidate();
        }
        #endregion

        #region polygon
        // config button polygon click
        private void button_polygon_Click(object sender, EventArgs e)
        {
            isDrawingPolygon = !isDrawingPolygon; // Đảo trạng thái
            button_polygon.Text = isDrawingPolygon ? "Cancel" : "Polygon"; // Đổi màu nút
            if (isDrawingPolygon)
            {
                // Bắt đầu vẽ polygon
                pointPolygon.Clear();
                PolygonOverlay.Clear();
                routesOverlay.Clear();

                ToTal_Distance.Text = "Total Area: 0 km²";
            }
            else
            {
                // Xóa polygon khỏi bản đồ
                PolygonOverlay.Clear();

                ToTal_Distance.Text = "Total Area: 0 km²";
            }
            gMapControl1.Update();
            gMapControl1.Invalidate();
        }

        // Update Polygon và thêm condition vẽ polygon
        private void UpdatePolygon()
        {
            if (pointPolygon.Count >= 0)
            {
                GMapPolygon polygon = new GMapPolygon(pointPolygon, "Polygon")
                {
                    Fill = new SolidBrush(Color.FromArgb(50, Color.Blue)),
                    Stroke = new Pen(Color.Blue, 4)
                };
                PolygonOverlay.Polygons.Add(polygon);

                totalArea = DistanceCalculator.CalculatePolygonArea(pointPolygon);
                ToTal_Distance.Text = $"Total Area: {(totalArea / 1e6):F2} km2";
            }
            gMapControl1.Update();
            gMapControl1.Invalidate();
        }
        #endregion

        #region search position
        private void button_search_position_Click(object sender, EventArgs e)
        {
            try
            {
                double latitude = double.Parse(textBox1.Text, CultureInfo.InvariantCulture); // vĩ độ
                double longitude = double.Parse(textBox2.Text, CultureInfo.InvariantCulture);// kinh độ
                // Tạo điểm mới
                PointLatLng point = new PointLatLng(latitude, longitude);
                // Cập nhật vị trí bản đồ
                gMapControl1.Position = point;
                gMapControl1.Zoom = 4; // Tăng mức zoom để nhìn rõ

                // Xóa các marker cũ (nếu có)
                searchOverlay.Clear();

                // Tạo marker mới
                GMarkerGoogle marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                searchOverlay.Markers.Add(marker);

                gMapControl1.Update();
                gMapControl1.Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng tọa độ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if ((bool)textBox1.Tag && textBox1.Text == "Nhập vĩ độ...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
                textBox1.Tag = false; // Đánh dấu đã nhập
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Tag = true;
                textBox1.Text = "Nhập vĩ độ...";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if ((bool)textBox2.Tag && textBox2.Text == "Nhập kinh độ...")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.Tag = false;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Tag = true;
                textBox2.Text = "Nhập kinh độ...";
                textBox2.ForeColor = Color.Gray;
            }
        }

        #endregion


    }
}
