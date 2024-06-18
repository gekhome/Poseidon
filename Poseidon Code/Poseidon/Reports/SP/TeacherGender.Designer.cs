namespace Poseidon.Reports.SP
{
    partial class TeacherGender
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphTitle graphTitle1 = new Telerik.Reporting.GraphTitle();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeacherGender));
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.Û◊œÀ_≈‘œ”GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.Û◊œÀ_≈‘œ”GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox = new Telerik.Reporting.TextBox();
            this.Û◊œÀ… œ_≈‘œ”DataTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox = new Telerik.Reporting.TextBox();
            this.ˆ’ÀœCaptionTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”CaptionTextBox = new Telerik.Reporting.TextBox();
            this.sqlSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.shape3 = new Telerik.Reporting.Shape();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.shape2 = new Telerik.Reporting.Shape();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox = new Telerik.Reporting.TextBox();
            this.ˆ’ÀœDataTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”DataTextBox = new Telerik.Reporting.TextBox();
            this.shape4 = new Telerik.Reporting.Shape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Û◊œÀ_≈‘œ”GroupFooterSection
            // 
            this.Û◊œÀ_≈‘œ”GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.Û◊œÀ_≈‘œ”GroupFooterSection.Name = "Û◊œÀ_≈‘œ”GroupFooterSection";
            // 
            // Û◊œÀ_≈‘œ”GroupHeaderSection
            // 
            this.Û◊œÀ_≈‘œ”GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(14.600001335144043D);
            this.Û◊œÀ_≈‘œ”GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1,
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox,
            this.Û◊œÀ… œ_≈‘œ”DataTextBox});
            this.Û◊œÀ_≈‘œ”GroupHeaderSection.Name = "Û◊œÀ_≈‘œ”GroupHeaderSection";
            // 
            // graph1
            // 
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields. À¡ƒœ”_À≈ ‘… œ"));
            graphGroup1.Name = "ÍÀ¡ƒœ”_À≈ ‘… œGroup";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields. À¡ƒœ”_À≈ ‘… œ", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.DataSource = this.sqlDataSource;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D), Telerik.Reporting.Drawing.Unit.Cm(1.6999999284744263D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Series.Add(this.barSeries1);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(12.241250038146973D));
            this.graph1.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            graphTitle1.Position = Telerik.Reporting.GraphItemPosition.TopCenter;
            graphTitle1.Style.LineColor = System.Drawing.Color.LightGray;
            graphTitle1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            graphTitle1.Text = " ¡‘¡ÕœÃ« ¡Õ¡ ÷’Àœ";
            this.graph1.Titles.Add(graphTitle1);
            // 
            // cartesianCoordinateSystem1
            // 
            this.cartesianCoordinateSystem1.Name = "cartesianCoordinateSystem1";
            this.cartesianCoordinateSystem1.XAxis = this.graphAxis2;
            this.cartesianCoordinateSystem1.YAxis = this.graphAxis1;
            // 
            // graphAxis2
            // 
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "graphAxis2";
            this.graphAxis2.Scale = categoryScale1;
            // 
            // graphAxis1
            // 
            this.graphAxis1.LabelFormat = "{0:P0}";
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "graphAxis1";
            this.graphAxis1.Scale = numericalScale1;
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Poseidon.Properties.Settings.PoseidonDBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = resources.GetString("sqlDataSource.SelectCommand");
            // 
            // barSeries1
            // 
            this.barSeries1.ArrangeMode = Telerik.Reporting.GraphSeriesArrangeMode.Stacked100;
            this.barSeries1.CategoryGroup = graphGroup1;
            this.barSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.barSeries1.DataPointLabel = "=Sum(Fields.–À«»œ”)";
            this.barSeries1.DataPointLabelStyle.Visible = false;
            this.barSeries1.DataPointStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries1.DataPointStyle.Visible = true;
            this.barSeries1.LegendItem.Style.LineColor = System.Drawing.Color.LightGray;
            this.barSeries1.LegendItem.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries1.LegendItem.Value = "=Fields.÷’Àœ";
            this.barSeries1.Name = "barSeries1";
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.÷’Àœ"));
            graphGroup2.Name = "ˆ’ÀœGroup";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.÷’Àœ", Telerik.Reporting.SortDirection.Asc));
            this.barSeries1.SeriesGroup = graphGroup2;
            this.barSeries1.Y = "=Sum(Fields.–À«»œ”)";
            // 
            // Û◊œÀ… œ_≈‘œ”CaptionTextBox
            // 
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.CanGrow = true;
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.036351140588521957D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Name = "Û◊œÀ… œ_≈‘œ”CaptionTextBox";
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.4470834732055664D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Style.Font.Bold = true;
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.StyleName = "Caption";
            this.Û◊œÀ… œ_≈‘œ”CaptionTextBox.Value = "ƒ…ƒ¡ ‘… œ ≈‘œ”:";
            // 
            // Û◊œÀ… œ_≈‘œ”DataTextBox
            // 
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.CanGrow = true;
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5078091621398926D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Name = "Û◊œÀ… œ_≈‘œ”DataTextBox";
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.208133697509766D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Style.Font.Bold = true;
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.StyleName = "Data";
            this.Û◊œÀ… œ_≈‘œ”DataTextBox.Value = "= Fields.”◊œÀ_≈‘œ”";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.79999923706054688D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox,
            this.ˆ’ÀœCaptionTextBox,
            this.À«»œ”CaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox
            // 
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.CanGrow = true;
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.Name = "ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox";
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2008333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.747082531452179D));
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.Style.Font.Bold = true;
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.StyleName = "Caption";
            this.ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox.Value = " À¡ƒœ”";
            // 
            // ˆ’ÀœCaptionTextBox
            // 
            this.ˆ’ÀœCaptionTextBox.CanGrow = true;
            this.ˆ’ÀœCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.3066668510437012D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.ˆ’ÀœCaptionTextBox.Name = "ˆ’ÀœCaptionTextBox";
            this.ˆ’ÀœCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2008333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.747082531452179D));
            this.ˆ’ÀœCaptionTextBox.Style.Font.Bold = true;
            this.ˆ’ÀœCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.ˆ’ÀœCaptionTextBox.StyleName = "Caption";
            this.ˆ’ÀœCaptionTextBox.Value = "÷’Àœ";
            // 
            // À«»œ”CaptionTextBox
            // 
            this.À«»œ”CaptionTextBox.CanGrow = true;
            this.À«»œ”CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.560416221618652D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.À«»œ”CaptionTextBox.Name = "À«»œ”CaptionTextBox";
            this.À«»œ”CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2008333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.747082531452179D));
            this.À«»œ”CaptionTextBox.Style.Font.Bold = true;
            this.À«»œ”CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”CaptionTextBox.StyleName = "Caption";
            this.À«»œ”CaptionTextBox.Value = "–À«»œ”";
            // 
            // sqlSchoolYears
            // 
            this.sqlSchoolYears.ConnectionString = "Poseidon.Properties.Settings.PoseidonDBConnectionString";
            this.sqlSchoolYears.Name = "sqlSchoolYears";
            this.sqlSchoolYears.SelectCommand = "SELECT        ”◊œÀ_≈‘œ”\r\nFROM            ”◊œÀ… œ_≈‘œ”\r\nORDER BY ”◊œÀ_≈‘œ” DESC";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0452070236206055D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox,
            this.shape3});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0076093543320894241D), Telerik.Reporting.Drawing.Unit.Cm(0.345206618309021D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.69228935241699219D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.8882341384887695D), Telerik.Reporting.Drawing.Unit.Cm(0.345206618309021D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.69228935241699219D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.06052602082490921D), Telerik.Reporting.Drawing.Unit.Cm(0.045005325227975845D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.681875228881836D), Telerik.Reporting.Drawing.Unit.Cm(0.30000105500221252D));
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.titleTextBox,
            this.textBox1,
            this.shape1,
            this.shape2});
            this.reportHeader.Name = "reportHeader";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0793749988079071D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3470833301544189D), Telerik.Reporting.Drawing.Unit.Cm(2.2999000549316406D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.4606249332427979D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.255316734313965D), Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D));
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.titleTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "= \"¡≈Õ \" + Fields.¡≈Õ_≈–ŸÕ’Ã…¡ + \" - ”◊œÀ« –Àœ…¡—◊ŸÕ\"";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.4606249332427979D), Telerik.Reporting.Drawing.Unit.Cm(1.1907248497009277D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.255316734313965D), Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "Title";
            this.textBox1.Value = " ·Ù·ÌÔÏﬁ ÂÍ·È‰ÂıÙÈÍ˛Ì ·Ì‹ ˆ˝ÎÔ";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0793749988079071D), Telerik.Reporting.Drawing.Unit.Cm(2.3813498020172119D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.655416488647461D), Telerik.Reporting.Drawing.Unit.Cm(0.19979965686798096D));
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0793749988079071D), Telerik.Reporting.Drawing.Unit.Cm(2.5930166244506836D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.655416488647461D), Telerik.Reporting.Drawing.Unit.Cm(0.19979965686798096D));
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.98894965648651123D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox,
            this.ˆ’ÀœDataTextBox,
            this.À«»œ”DataTextBox,
            this.shape4});
            this.detail.Name = "detail";
            // 
            // ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox
            // 
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.CanGrow = true;
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.Name = "ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox";
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2008333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.747082531452179D));
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.StyleName = "Data";
            this.ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox.Value = "=Fields. À¡ƒœ”_À≈ ‘… œ";
            // 
            // ˆ’ÀœDataTextBox
            // 
            this.ˆ’ÀœDataTextBox.CanGrow = true;
            this.ˆ’ÀœDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.3066668510437012D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.ˆ’ÀœDataTextBox.Name = "ˆ’ÀœDataTextBox";
            this.ˆ’ÀœDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2008333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.747082531452179D));
            this.ˆ’ÀœDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.ˆ’ÀœDataTextBox.StyleName = "Data";
            this.ˆ’ÀœDataTextBox.Value = "=Fields.÷’Àœ";
            // 
            // À«»œ”DataTextBox
            // 
            this.À«»œ”DataTextBox.CanGrow = true;
            this.À«»œ”DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.560416221618652D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.À«»œ”DataTextBox.Name = "À«»œ”DataTextBox";
            this.À«»œ”DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2008333206176758D), Telerik.Reporting.Drawing.Unit.Cm(0.747082531452179D));
            this.À«»œ”DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”DataTextBox.StyleName = "Data";
            this.À«»œ”DataTextBox.Value = "=Fields.–À«»œ”";
            // 
            // shape4
            // 
            this.shape4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.80019986629486084D));
            this.shape4.Name = "shape4";
            this.shape4.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.681875228881836D), Telerik.Reporting.Drawing.Unit.Cm(0.1887497752904892D));
            // 
            // TeacherGender
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.”◊œÀ_≈‘œ”", Telerik.Reporting.FilterOperator.Equal, "=Parameters.SchoolYear.Value"));
            group1.GroupFooter = this.Û◊œÀ_≈‘œ”GroupFooterSection;
            group1.GroupHeader = this.Û◊œÀ_≈‘œ”GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.”◊œÀ_≈‘œ”"));
            group1.Name = "Û◊œÀ_≈‘œ”Group";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Û◊œÀ_≈‘œ”GroupHeaderSection,
            this.Û◊œÀ_≈‘œ”GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "TeacherGender";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AvailableValues.DataSource = this.sqlSchoolYears;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.”◊œÀ_≈‘œ”";
            reportParameter1.AvailableValues.ValueMember = "= Fields.”◊œÀ_≈‘œ”";
            reportParameter1.Name = "SchoolYear";
            reportParameter1.Text = "ƒÈ‰·ÍÙÈÍ¸ ›ÙÔÚ";
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(15.814167022705078D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection Û◊œÀ_≈‘œ”GroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection Û◊œÀ_≈‘œ”GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox ÍÀ¡ƒœ”_À≈ ‘… œCaptionTextBox;
        private Telerik.Reporting.TextBox ˆ’ÀœCaptionTextBox;
        private Telerik.Reporting.TextBox À«»œ”CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ÍÀ¡ƒœ”_À≈ ‘… œDataTextBox;
        private Telerik.Reporting.TextBox ˆ’ÀœDataTextBox;
        private Telerik.Reporting.TextBox À«»œ”DataTextBox;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.BarSeries barSeries1;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.TextBox Û◊œÀ… œ_≈‘œ”CaptionTextBox;
        private Telerik.Reporting.TextBox Û◊œÀ… œ_≈‘œ”DataTextBox;
        private Telerik.Reporting.Shape shape3;
        private Telerik.Reporting.Shape shape4;
        private Telerik.Reporting.SqlDataSource sqlSchoolYears;

    }
}