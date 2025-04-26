using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using GMap.NET.WindowsForms;

namespace Maps.CalculateProcessing
{
    public class DistanceCalculator
    {
        // Tính tổng khoảng cách giữa các điểm trong danh sách.
        public static double CalculateTotalDistance(List<PointLatLng> points)
        {
            double totalDistance = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                totalDistance += GetDistance(points[i], points[i + 1]);
            }
            return totalDistance;
        }

        // Tính khoảng cách giữa hai điểm bằng công thức Haversine.
        private static double GetDistance(PointLatLng p1, PointLatLng p2)
        {
            var sCoord = new GeoCoordinate(p1.Lat, p1.Lng);
            var eCoord = new GeoCoordinate(p2.Lat, p2.Lng);
            return sCoord.GetDistanceTo(eCoord) / 1000.0; // Trả về km
        }

        // Tính Diện tích bằng công thức Shoelace
        public static double CalculatePolygonArea(List<PointLatLng> points)
        {
            if (points == null || points.Count < 3)
                return 0;

            double earthRadius = 6378137; // Bán kính Trái Đất (m)
            double area = 0;
            int n = points.Count;

            for (int i = 0; i < n; i++)
            {
                var p1 = points[i];
                var p2 = points[(i + 1) % n];

                double lat1 = p1.Lat * Math.PI / 180;
                double lat2 = p2.Lat * Math.PI / 180;
                double dLng = (p2.Lng - p1.Lng) * Math.PI / 180;

                area += dLng * (2 + Math.Sin(lat1) + Math.Sin(lat2));
            }

            area = Math.Abs(area) * (earthRadius * earthRadius) / 2.0;
            return area; // Kết quả tính theo mét vuông (m²)
        }
    }

    // Tạo một lớp GMapTextMarker để hiển thị text trên map
    public class GMapTextMarker : GMapMarker
    {
        private readonly string text;
        private readonly Font font = new Font("Arial", 12, FontStyle.Bold);
        private readonly Brush brush = Brushes.Black;

        public GMapTextMarker(PointLatLng p, string text) : base(p)
        {
            this.text = text;
        }

        public override void OnRender(Graphics g)
        {
            g.DrawString(text, font, brush, new PointF(LocalPosition.X, LocalPosition.Y));
        }
    }
}
