namespace Poseidon.Reports.SM
{
    partial class StudentGradesReportSM
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentGradesReportSM));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.�������������GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.�������������GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.�������������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�������������DataTextBox = new Telerik.Reporting.TextBox();
            this.shape5 = new Telerik.Reporting.Shape();
            this.�������GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.�������GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.�������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�������DataTextBox = new Telerik.Reporting.TextBox();
            this.shape6 = new Telerik.Reporting.Shape();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.������_�����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������_��CaptionTextBox = new Telerik.Reporting.TextBox();
            this.sqlStudents = new Telerik.Reporting.SqlDataSource();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.���_��������DataTextBox = new Telerik.Reporting.TextBox();
            this.���������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.���������DataTextBox = new Telerik.Reporting.TextBox();
            this.��������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.��������DataTextBox = new Telerik.Reporting.TextBox();
            this.�����������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�����������DataTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.shape3 = new Telerik.Reporting.Shape();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.������_�����DataTextBox = new Telerik.Reporting.TextBox();
            this.������_��DataTextBox = new Telerik.Reporting.TextBox();
            this.sqlTerms = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // �������������GroupFooterSection
            // 
            this.�������������GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.4149070978164673D);
            this.�������������GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox9});
            this.�������������GroupFooterSection.Name = "�������������GroupFooterSection";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.4149068295955658D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.646883010864258D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox8.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.StyleName = "Caption";
            this.textBox8.Value = "=\"������� �.� ���������: \"+ Fields.�������������";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Format = "{0:N1}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.702708244323731D), Telerik.Reporting.Drawing.Unit.Cm(0.4149068295955658D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0612499713897705D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox9.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "= Avg(Fields.������_��)";
            // 
            // �������������GroupHeaderSection
            // 
            this.�������������GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.000200629234314D);
            this.�������������GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�������������CaptionTextBox,
            this.�������������DataTextBox,
            this.shape5});
            this.�������������GroupHeaderSection.Name = "�������������GroupHeaderSection";
            this.�������������GroupHeaderSection.PageBreak = Telerik.Reporting.PageBreak.After;
            this.�������������GroupHeaderSection.PrintOnEveryPage = true;
            // 
            // �������������CaptionTextBox
            // 
            this.�������������CaptionTextBox.CanGrow = true;
            this.�������������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�������������CaptionTextBox.Name = "�������������CaptionTextBox";
            this.�������������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9468827247619629D), Telerik.Reporting.Drawing.Unit.Cm(0.747083306312561D));
            this.�������������CaptionTextBox.Style.Font.Bold = true;
            this.�������������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.�������������CaptionTextBox.StyleName = "Caption";
            this.�������������CaptionTextBox.Value = "�������������:";
            // 
            // �������������DataTextBox
            // 
            this.�������������DataTextBox.CanGrow = true;
            this.�������������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.9999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�������������DataTextBox.Name = "�������������DataTextBox";
            this.�������������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.708333969116211D), Telerik.Reporting.Drawing.Unit.Cm(0.747083306312561D));
            this.�������������DataTextBox.Style.Font.Bold = true;
            this.�������������DataTextBox.StyleName = "Data";
            this.�������������DataTextBox.Value = "=Fields.�������������";
            // 
            // shape5
            // 
            this.shape5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.80019986629486084D));
            this.shape5.Name = "shape5";
            this.shape5.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.76145076751709D), Telerik.Reporting.Drawing.Unit.Cm(0.20000070333480835D));
            // 
            // �������GroupFooterSection
            // 
            this.�������GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0001000165939331D);
            this.�������GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox7});
            this.�������GroupFooterSection.Name = "�������GroupFooterSection";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Format = "{0:N1}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.700200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0612499713897705D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "= Avg(Fields.������_��)";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.646883010864258D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.StyleName = "Caption";
            this.textBox7.Value = "=\"����� ���� ������ : \" + Fields.�������";
            // 
            // �������GroupHeaderSection
            // 
            this.�������GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D);
            this.�������GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�������CaptionTextBox,
            this.�������DataTextBox,
            this.shape6});
            this.�������GroupHeaderSection.Name = "�������GroupHeaderSection";
            this.�������GroupHeaderSection.PrintOnEveryPage = true;
            // 
            // �������CaptionTextBox
            // 
            this.�������CaptionTextBox.CanGrow = true;
            this.�������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�������CaptionTextBox.Name = "�������CaptionTextBox";
            this.�������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9468824863433838D), Telerik.Reporting.Drawing.Unit.Cm(0.74688225984573364D));
            this.�������CaptionTextBox.Style.Font.Bold = true;
            this.�������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.�������CaptionTextBox.StyleName = "Caption";
            this.�������CaptionTextBox.Value = "�������:";
            // 
            // �������DataTextBox
            // 
            this.�������DataTextBox.CanGrow = true;
            this.�������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.9999995231628418D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�������DataTextBox.Name = "�������DataTextBox";
            this.�������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.708333969116211D), Telerik.Reporting.Drawing.Unit.Cm(0.74688225984573364D));
            this.�������DataTextBox.Style.Font.Bold = true;
            this.�������DataTextBox.StyleName = "Data";
            this.�������DataTextBox.Value = "=Fields.�������";
            // 
            // shape6
            // 
            this.shape6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.79999959468841553D));
            this.shape6.Name = "shape6";
            this.shape6.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.76145076751709D), Telerik.Reporting.Drawing.Unit.Cm(0.20000070333480835D));
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.44104862213134766D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0529166460037231D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������_�����CaptionTextBox,
            this.������_��CaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ������_�����CaptionTextBox
            // 
            this.������_�����CaptionTextBox.CanGrow = true;
            this.������_�����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������_�����CaptionTextBox.Name = "������_�����CaptionTextBox";
            this.������_�����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.646883010864258D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.������_�����CaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������_�����CaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������_�����CaptionTextBox.Style.Font.Bold = true;
            this.������_�����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_�����CaptionTextBox.StyleName = "Caption";
            this.������_�����CaptionTextBox.Value = "������ (�=������, �=���������)";
            // 
            // ������_��CaptionTextBox
            // 
            this.������_��CaptionTextBox.CanGrow = true;
            this.������_��CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.699999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������_��CaptionTextBox.Name = "������_��CaptionTextBox";
            this.������_��CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0612499713897705D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.������_��CaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������_��CaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������_��CaptionTextBox.Style.Font.Bold = true;
            this.������_��CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_��CaptionTextBox.StyleName = "Caption";
            this.������_��CaptionTextBox.Value = "������";
            // 
            // sqlStudents
            // 
            this.sqlStudents.ConnectionString = "Poseidon.Properties.Settings.PoseidonDBConnectionString";
            this.sqlStudents.Name = "sqlStudents";
            this.sqlStudents.SelectCommand = "SELECT DISTINCT ����, �������������\r\nFROM            View����������_�����_��\r\nORD" +
    "ER BY �������������";
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Poseidon.Properties.Settings.PoseidonDBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = resources.GetString("sqlDataSource.SelectCommand");
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2001998424530029D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox,
            this.shape2});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.708333015441895D), Telerik.Reporting.Drawing.Unit.Cm(0.84708327054977417D));
            this.reportNameTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = " ��������� ���������� ���������� ������ ���������";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.9001997709274292D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.814167022705078D), Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D));
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2620849609375D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox,
            this.shape1});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.50750130414962769D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.75458365678787231D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.8806252479553223D), Telerik.Reporting.Drawing.Unit.Cm(0.50750130414962769D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.75458365678787231D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \"+PageNumber";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.20730000734329224D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.814167022705078D), Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D));
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(9.299799919128418D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.���_��������DataTextBox,
            this.���������CaptionTextBox,
            this.���������DataTextBox,
            this.��������CaptionTextBox,
            this.��������DataTextBox,
            this.�����������CaptionTextBox,
            this.�����������DataTextBox,
            this.pictureBox1,
            this.pictureBox2,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.shape3,
            this.textBox5,
            this.titleTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // ���_��������DataTextBox
            // 
            this.���_��������DataTextBox.CanGrow = true;
            this.���_��������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9335417747497559D), Telerik.Reporting.Drawing.Unit.Cm(3.3000006675720215D));
            this.���_��������DataTextBox.Name = "���_��������DataTextBox";
            this.���_��������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8279085159301758D), Telerik.Reporting.Drawing.Unit.Cm(0.89979970455169678D));
            this.���_��������DataTextBox.Style.Font.Bold = true;
            this.���_��������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.���_��������DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.���_��������DataTextBox.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.���_��������DataTextBox.StyleName = "Data";
            this.���_��������DataTextBox.Value = "=Fields.���_��������";
            // 
            // ���������CaptionTextBox
            // 
            this.���������CaptionTextBox.CanGrow = true;
            this.���������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(5.1998004913330078D));
            this.���������CaptionTextBox.Name = "���������CaptionTextBox";
            this.���������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7470831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.68954402208328247D));
            this.���������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.���������CaptionTextBox.StyleName = "Caption";
            this.���������CaptionTextBox.Value = "���������:";
            // 
            // ���������DataTextBox
            // 
            this.���������DataTextBox.CanGrow = true;
            this.���������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8001999855041504D), Telerik.Reporting.Drawing.Unit.Cm(5.2102560997009277D));
            this.���������DataTextBox.Name = "���������DataTextBox";
            this.���������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.68954402208328247D));
            this.���������DataTextBox.StyleName = "Data";
            this.���������DataTextBox.Value = "= Fields.���_���������";
            // 
            // ��������CaptionTextBox
            // 
            this.��������CaptionTextBox.CanGrow = true;
            this.��������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(5.8895444869995117D));
            this.��������CaptionTextBox.Name = "��������CaptionTextBox";
            this.��������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(0.689344584941864D));
            this.��������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.��������CaptionTextBox.StyleName = "Caption";
            this.��������CaptionTextBox.Value = "��������:";
            // 
            // ��������DataTextBox
            // 
            this.��������DataTextBox.CanGrow = true;
            this.��������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8002004623413086D), Telerik.Reporting.Drawing.Unit.Cm(5.8999996185302734D));
            this.��������DataTextBox.Name = "��������DataTextBox";
            this.��������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.689344584941864D));
            this.��������DataTextBox.StyleName = "Data";
            this.��������DataTextBox.Value = "=Fields.��������";
            // 
            // �����������CaptionTextBox
            // 
            this.�����������CaptionTextBox.CanGrow = true;
            this.�����������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(6.5895442962646484D));
            this.�����������CaptionTextBox.Name = "�����������CaptionTextBox";
            this.�����������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7998995780944824D), Telerik.Reporting.Drawing.Unit.Cm(0.81025534868240356D));
            this.�����������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.�����������CaptionTextBox.StyleName = "Caption";
            this.�����������CaptionTextBox.Value = "�����������:";
            // 
            // �����������DataTextBox
            // 
            this.�����������DataTextBox.CanGrow = true;
            this.�����������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8002004623413086D), Telerik.Reporting.Drawing.Unit.Cm(6.5999999046325684D));
            this.�����������DataTextBox.Name = "�����������DataTextBox";
            this.�����������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.81025534868240356D));
            this.�����������DataTextBox.StyleName = "Data";
            this.�����������DataTextBox.Value = "=Fields.�����������";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.899899959564209D), Telerik.Reporting.Drawing.Unit.Cm(1.5998001098632813D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.9002001285552979D), Telerik.Reporting.Drawing.Unit.Cm(0.00019984244136139751D));
            this.pictureBox2.MimeType = "image/png";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.899899959564209D), Telerik.Reporting.Drawing.Unit.Cm(1.5998001098632813D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.6000006198883057D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8806247711181641D), Telerik.Reporting.Drawing.Unit.Cm(0.79979974031448364D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "�������� ����������";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.40000057220459D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8806252479553223D), Telerik.Reporting.Drawing.Unit.Cm(0.89979970455169678D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "��������� ��������� ��� �������";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(4.2000002861022949D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8806252479553223D), Telerik.Reporting.Drawing.Unit.Cm(0.89979970455169678D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox3.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "����� ���������";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.0232105255126953D), Telerik.Reporting.Drawing.Unit.Cm(1.6000006198883057D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3999991416931152D), Telerik.Reporting.Drawing.Unit.Cm(0.79999995231628418D));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "��. ����.:";
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.5D), Telerik.Reporting.Drawing.Unit.Cm(2.40000057220459D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3140664100646973D), Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D));
            this.shape3.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Dotted;
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(3.3000006675720215D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8806252479553223D), Telerik.Reporting.Drawing.Unit.Cm(0.89979970455169678D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox5.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "�������� ��������� ��������";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(7.9998006820678711D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.81386661529541D), Telerik.Reporting.Drawing.Unit.Cm(0.9999992847442627D));
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.titleTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "������������� ���������� �����������";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.74688225984573364D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������_�����DataTextBox,
            this.������_��DataTextBox});
            this.detail.Name = "detail";
            // 
            // ������_�����DataTextBox
            // 
            this.������_�����DataTextBox.CanGrow = true;
            this.������_�����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������_�����DataTextBox.Name = "������_�����DataTextBox";
            this.������_�����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.646883010864258D), Telerik.Reporting.Drawing.Unit.Cm(0.6939656138420105D));
            this.������_�����DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������_�����DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������_�����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.������_�����DataTextBox.StyleName = "Data";
            this.������_�����DataTextBox.Value = "=Fields.������_�����";
            // 
            // ������_��DataTextBox
            // 
            this.������_��DataTextBox.CanGrow = true;
            this.������_��DataTextBox.Format = "{0:N1}";
            this.������_��DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.699999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������_��DataTextBox.Name = "������_��DataTextBox";
            this.������_��DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0612499713897705D), Telerik.Reporting.Drawing.Unit.Cm(0.6939656138420105D));
            this.������_��DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������_��DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������_��DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_��DataTextBox.StyleName = "Data";
            this.������_��DataTextBox.Value = "=Fields.������_��";
            // 
            // sqlTerms
            // 
            this.sqlTerms.ConnectionString = "Poseidon.Properties.Settings.PoseidonDBConnectionString";
            this.sqlTerms.Name = "sqlTerms";
            this.sqlTerms.SelectCommand = "SELECT        �������\r\nFROM            �������\r\nORDER BY �������";
            // 
            // StudentGradesReportSM
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.Student.Value"));
            group1.GroupFooter = this.�������������GroupFooterSection;
            group1.GroupHeader = this.�������������GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.�������������"));
            group1.Name = "�������������Group";
            group2.GroupFooter = this.�������GroupFooterSection;
            group2.GroupHeader = this.�������GroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.�������"));
            group2.Name = "�������Group";
            group3.GroupFooter = this.labelsGroupFooterSection;
            group3.GroupHeader = this.labelsGroupHeaderSection;
            group3.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�������������GroupHeaderSection,
            this.�������������GroupFooterSection,
            this.�������GroupHeaderSection,
            this.�������GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "StudentGradesCertificateSM";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AvailableValues.DataSource = this.sqlStudents;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.�������������";
            reportParameter1.AvailableValues.ValueMember = "= Fields.����";
            reportParameter1.Name = "Student";
            reportParameter1.Text = "����������";
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(15.814066886901856D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection �������������GroupHeaderSection;
        private Telerik.Reporting.TextBox �������������CaptionTextBox;
        private Telerik.Reporting.TextBox �������������DataTextBox;
        private Telerik.Reporting.GroupFooterSection �������������GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection �������GroupHeaderSection;
        private Telerik.Reporting.TextBox �������CaptionTextBox;
        private Telerik.Reporting.TextBox �������DataTextBox;
        private Telerik.Reporting.GroupFooterSection �������GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox ������_�����CaptionTextBox;
        private Telerik.Reporting.TextBox ������_��CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox ���_��������DataTextBox;
        private Telerik.Reporting.TextBox ���������CaptionTextBox;
        private Telerik.Reporting.TextBox ���������DataTextBox;
        private Telerik.Reporting.TextBox ��������CaptionTextBox;
        private Telerik.Reporting.TextBox ��������DataTextBox;
        private Telerik.Reporting.TextBox �����������CaptionTextBox;
        private Telerik.Reporting.TextBox �����������DataTextBox;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ������_�����DataTextBox;
        private Telerik.Reporting.TextBox ������_��DataTextBox;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.Shape shape3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.Shape shape5;
        private Telerik.Reporting.Shape shape6;
        private Telerik.Reporting.SqlDataSource sqlStudents;
        private Telerik.Reporting.SqlDataSource sqlTerms;

    }
}