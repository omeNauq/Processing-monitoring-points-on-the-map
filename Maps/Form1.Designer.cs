namespace Maps
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            button_marker = new Button();
            panel5 = new Panel();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button_search_position = new Button();
            button_polygon = new Button();
            button_route = new Button();
            panel2 = new Panel();
            hScrollBar1 = new HScrollBar();
            panel3 = new Panel();
            ToTal_Distance = new TextBox();
            gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.None;
            panel1.Controls.Add(button_marker);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(button_polygon);
            panel1.Controls.Add(button_route);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 442);
            panel1.TabIndex = 1;
            // 
            // button_marker
            // 
            button_marker.BackgroundImageLayout = ImageLayout.None;
            button_marker.Location = new Point(43, 18);
            button_marker.Name = "button_marker";
            button_marker.Size = new Size(94, 29);
            button_marker.TabIndex = 7;
            button_marker.Text = "Marker";
            button_marker.UseVisualStyleBackColor = true;
            button_marker.Click += button_marker_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(0, 0, 192);
            panel5.Controls.Add(textBox3);
            panel5.Controls.Add(textBox2);
            panel5.Controls.Add(textBox1);
            panel5.Controls.Add(button_search_position);
            panel5.Location = new Point(0, 193);
            panel5.Name = "panel5";
            panel5.Size = new Size(200, 156);
            panel5.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(0, 0, 192);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Dock = DockStyle.Top;
            textBox3.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.ForeColor = Color.White;
            textBox3.Location = new Point(0, 0);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(200, 31);
            textBox3.TabIndex = 8;
            textBox3.Text = "Position";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(0, 91);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(200, 27);
            textBox2.TabIndex = 7;
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1, 47);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(200, 27);
            textBox1.TabIndex = 6;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // button_search_position
            // 
            button_search_position.Dock = DockStyle.Bottom;
            button_search_position.Location = new Point(0, 127);
            button_search_position.Name = "button_search_position";
            button_search_position.Size = new Size(200, 29);
            button_search_position.TabIndex = 5;
            button_search_position.Text = "Search Position";
            button_search_position.UseVisualStyleBackColor = true;
            button_search_position.Click += button_search_position_Click;
            // 
            // button_polygon
            // 
            button_polygon.Location = new Point(43, 135);
            button_polygon.Name = "button_polygon";
            button_polygon.Size = new Size(94, 29);
            button_polygon.TabIndex = 4;
            button_polygon.Text = "Polygon";
            button_polygon.UseVisualStyleBackColor = true;
            button_polygon.Click += button_polygon_Click;
            // 
            // button_route
            // 
            button_route.BackgroundImageLayout = ImageLayout.None;
            button_route.Location = new Point(43, 75);
            button_route.Name = "button_route";
            button_route.Size = new Size(94, 29);
            button_route.TabIndex = 0;
            button_route.Text = "Route";
            button_route.UseVisualStyleBackColor = true;
            button_route.Click += button_route_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(hScrollBar1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(200, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(704, 47);
            panel2.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            hScrollBar1.Location = new Point(0, 0);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(704, 47);
            hScrollBar1.TabIndex = 0;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(ToTal_Distance);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(200, 395);
            panel3.Name = "panel3";
            panel3.Size = new Size(704, 47);
            panel3.TabIndex = 2;
            // 
            // ToTal_Distance
            // 
            ToTal_Distance.Dock = DockStyle.Right;
            ToTal_Distance.Location = new Point(473, 0);
            ToTal_Distance.Name = "ToTal_Distance";
            ToTal_Distance.Size = new Size(231, 27);
            ToTal_Distance.TabIndex = 4;
            // 
            // gMapControl1
            // 
            gMapControl1.Bearing = 0F;
            gMapControl1.CanDragMap = true;
            gMapControl1.Dock = DockStyle.Fill;
            gMapControl1.EmptyTileColor = Color.Navy;
            gMapControl1.GrayScaleMode = false;
            gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            gMapControl1.LevelsKeepInMemory = 5;
            gMapControl1.Location = new Point(200, 47);
            gMapControl1.MarkersEnabled = true;
            gMapControl1.MaxZoom = 15;
            gMapControl1.MinZoom = 2;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gMapControl1.Name = "gMapControl1";
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.RetryLoadTile = 0;
            gMapControl1.RoutesEnabled = true;
            gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            gMapControl1.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Size = new Size(704, 348);
            gMapControl1.TabIndex = 3;
            gMapControl1.Zoom = 0D;
            gMapControl1.MouseClick += gMapControl1_MouseClick_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 442);
            Controls.Add(gMapControl1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Great Map Viewer";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private HScrollBar hScrollBar1;
        private Button button_route;
        private Button button_polygon;
        private Panel panel5;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button_search_position;
        private Button button_marker;
        private TextBox textBox3;
        private ToolTip toolTip1;
        private TextBox ToTal_Distance;
    }
}
