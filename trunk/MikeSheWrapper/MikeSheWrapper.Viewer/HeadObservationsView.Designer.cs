namespace MikeSheWrapper.Viewer
{
  partial class HeadObservationsView
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.ButtonReadWells = new System.Windows.Forms.Button();
      this.textBoxWellsNumber = new System.Windows.Forms.TextBox();
      this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.MinNumber = new System.Windows.Forms.TextBox();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.buttonMSheObs = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.listBoxIntakes = new System.Windows.Forms.ListBox();
      this.buttonLSFile = new System.Windows.Forms.Button();
      this.radioButtonMax = new System.Windows.Forms.RadioButton();
      this.radioButtonMin = new System.Windows.Forms.RadioButton();
      this.buttonNovanaShape = new System.Windows.Forms.Button();
      this.listBoxAnlaeg = new System.Windows.Forms.ListBox();
      this.listBoxWells = new System.Windows.Forms.ListBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.buttonReadMshe = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.ToolTipMSHEbutton = new System.Windows.Forms.ToolTip(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.label6 = new System.Windows.Forms.Label();
      this.propertyWells = new System.Windows.Forms.PropertyGrid();
      this.panel4 = new System.Windows.Forms.Panel();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.textBoxMeanYearlyExt = new System.Windows.Forms.TextBox();
      this.dateTimeEndExt = new System.Windows.Forms.DateTimePicker();
      this.label13 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.dateTimeStartExt = new System.Windows.Forms.DateTimePicker();
      this.label10 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.buttonMsheExt = new System.Windows.Forms.Button();
      this.buttonNovanaExtract = new System.Windows.Forms.Button();
      this.label14 = new System.Windows.Forms.Label();
      this.textBoxPlantCount = new System.Windows.Forms.TextBox();
      this.propertyGridPlants = new System.Windows.Forms.PropertyGrid();
      this.observationWellBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.labelProgBar = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.observationWellBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // ButtonReadWells
      // 
      this.ButtonReadWells.Location = new System.Drawing.Point(12, 21);
      this.ButtonReadWells.Name = "ButtonReadWells";
      this.ButtonReadWells.Size = new System.Drawing.Size(142, 42);
      this.ButtonReadWells.TabIndex = 0;
      this.ButtonReadWells.Text = "Read in Jupiter database";
      this.ButtonReadWells.UseVisualStyleBackColor = true;
      this.ButtonReadWells.Click += new System.EventHandler(this.ReadButton_Click);
      // 
      // textBoxWellsNumber
      // 
      this.textBoxWellsNumber.Enabled = false;
      this.textBoxWellsNumber.Location = new System.Drawing.Point(153, 170);
      this.textBoxWellsNumber.Name = "textBoxWellsNumber";
      this.textBoxWellsNumber.Size = new System.Drawing.Size(67, 20);
      this.textBoxWellsNumber.TabIndex = 1;
      // 
      // openFileDialog2
      // 
      this.openFileDialog2.Filter = "Jupiter Access database (*.mdb)|*.mdb";
      // 
      // textBox4
      // 
      this.textBox4.Enabled = false;
      this.textBox4.Location = new System.Drawing.Point(152, 169);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(67, 20);
      this.textBox4.TabIndex = 9;
      // 
      // MinNumber
      // 
      this.MinNumber.Location = new System.Drawing.Point(136, 95);
      this.MinNumber.Name = "MinNumber";
      this.MinNumber.Size = new System.Drawing.Size(61, 20);
      this.MinNumber.TabIndex = 10;
      this.MinNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MinNumber_KeyUp);
      this.MinNumber.Validating += new System.ComponentModel.CancelEventHandler(this.MinNumber_Validating);
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Location = new System.Drawing.Point(68, 24);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(129, 20);
      this.dateTimePicker1.TabIndex = 12;
      this.dateTimePicker1.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
      this.dateTimePicker1.Validating += new System.ComponentModel.CancelEventHandler(this.MinNumber_Validating);
      // 
      // dateTimePicker2
      // 
      this.dateTimePicker2.Location = new System.Drawing.Point(68, 50);
      this.dateTimePicker2.Name = "dateTimePicker2";
      this.dateTimePicker2.Size = new System.Drawing.Size(129, 20);
      this.dateTimePicker2.TabIndex = 13;
      this.dateTimePicker2.Validating += new System.ComponentModel.CancelEventHandler(this.MinNumber_Validating);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 28);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(53, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "Start date";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(9, 54);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(50, 13);
      this.label4.TabIndex = 15;
      this.label4.Text = "End date";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(65, 98);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(62, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "No. of obs.:";
      // 
      // buttonMSheObs
      // 
      this.buttonMSheObs.Enabled = false;
      this.buttonMSheObs.Location = new System.Drawing.Point(17, 77);
      this.buttonMSheObs.Name = "buttonMSheObs";
      this.buttonMSheObs.Size = new System.Drawing.Size(169, 23);
      this.buttonMSheObs.TabIndex = 18;
      this.buttonMSheObs.Text = "Time series file for MikeShe";
      this.ToolTipMSHEbutton.SetToolTip(this.buttonMSheObs, "Text and .dfs0 files to create detailed time series output in MikeShe.");
      this.buttonMSheObs.UseVisualStyleBackColor = true;
      this.buttonMSheObs.Click += new System.EventHandler(this.buttonMSheObs_Click);
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Location = new System.Drawing.Point(16, 456);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(203, 241);
      this.propertyGrid1.TabIndex = 29;
      // 
      // listBoxIntakes
      // 
      this.listBoxIntakes.FormattingEnabled = true;
      this.listBoxIntakes.Location = new System.Drawing.Point(16, 197);
      this.listBoxIntakes.Name = "listBoxIntakes";
      this.listBoxIntakes.Size = new System.Drawing.Size(203, 251);
      this.listBoxIntakes.TabIndex = 30;
      this.listBoxIntakes.SelectedIndexChanged += new System.EventHandler(this.listBoxIntake_SelectedIndexChanged);
      // 
      // buttonLSFile
      // 
      this.buttonLSFile.Enabled = false;
      this.buttonLSFile.Location = new System.Drawing.Point(17, 48);
      this.buttonLSFile.Name = "buttonLSFile";
      this.buttonLSFile.Size = new System.Drawing.Size(169, 23);
      this.buttonLSFile.TabIndex = 31;
      this.buttonLSFile.Text = "LayerStatistics input file";
      this.buttonLSFile.UseVisualStyleBackColor = true;
      this.buttonLSFile.Click += new System.EventHandler(this.buttonLSFile_Click_1);
      // 
      // radioButtonMax
      // 
      this.radioButtonMax.AutoSize = true;
      this.radioButtonMax.Location = new System.Drawing.Point(12, 81);
      this.radioButtonMax.Name = "radioButtonMax";
      this.radioButtonMax.Size = new System.Drawing.Size(45, 17);
      this.radioButtonMax.TabIndex = 32;
      this.radioButtonMax.Text = "Max";
      this.radioButtonMax.UseVisualStyleBackColor = true;
      this.radioButtonMax.CheckedChanged += new System.EventHandler(this.radioButtonMax_CheckedChanged);
      // 
      // radioButtonMin
      // 
      this.radioButtonMin.AutoSize = true;
      this.radioButtonMin.Checked = true;
      this.radioButtonMin.Location = new System.Drawing.Point(12, 104);
      this.radioButtonMin.Name = "radioButtonMin";
      this.radioButtonMin.Size = new System.Drawing.Size(42, 17);
      this.radioButtonMin.TabIndex = 33;
      this.radioButtonMin.TabStop = true;
      this.radioButtonMin.Text = "Min";
      this.radioButtonMin.UseVisualStyleBackColor = true;
      // 
      // buttonNovanaShape
      // 
      this.buttonNovanaShape.Enabled = false;
      this.buttonNovanaShape.Location = new System.Drawing.Point(17, 19);
      this.buttonNovanaShape.Name = "buttonNovanaShape";
      this.buttonNovanaShape.Size = new System.Drawing.Size(169, 23);
      this.buttonNovanaShape.TabIndex = 34;
      this.buttonNovanaShape.Text = "NOVANA shape file for ArcMap";
      this.buttonNovanaShape.UseVisualStyleBackColor = true;
      this.buttonNovanaShape.Click += new System.EventHandler(this.WriteNovanaShape);
      // 
      // listBoxAnlaeg
      // 
      this.listBoxAnlaeg.FormattingEnabled = true;
      this.listBoxAnlaeg.Location = new System.Drawing.Point(14, 197);
      this.listBoxAnlaeg.Name = "listBoxAnlaeg";
      this.listBoxAnlaeg.Size = new System.Drawing.Size(203, 251);
      this.listBoxAnlaeg.TabIndex = 35;
      this.listBoxAnlaeg.SelectedIndexChanged += new System.EventHandler(this.listBoxAnlaeg_SelectedIndexChanged_1);
      // 
      // listBoxWells
      // 
      this.listBoxWells.FormattingEnabled = true;
      this.listBoxWells.Location = new System.Drawing.Point(17, 197);
      this.listBoxWells.Name = "listBoxWells";
      this.listBoxWells.Size = new System.Drawing.Size(203, 251);
      this.listBoxWells.TabIndex = 36;
      this.listBoxWells.SelectedIndexChanged += new System.EventHandler(this.listBoxWells_SelectedIndexChanged);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(13, 2);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(59, 20);
      this.label8.TabIndex = 37;
      this.label8.Text = "Plants";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(3, 0);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(52, 20);
      this.label9.TabIndex = 38;
      this.label9.Text = "Wells";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label11.Location = new System.Drawing.Point(3, 0);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(69, 20);
      this.label11.TabIndex = 39;
      this.label11.Text = "Intakes";
      // 
      // buttonReadMshe
      // 
      this.buttonReadMshe.Location = new System.Drawing.Point(12, 71);
      this.buttonReadMshe.Name = "buttonReadMshe";
      this.buttonReadMshe.Size = new System.Drawing.Size(142, 42);
      this.buttonReadMshe.TabIndex = 40;
      this.buttonReadMshe.Text = "Read in MikeShe setup";
      this.ToolTipMSHEbutton.SetToolTip(this.buttonReadMshe, "Read wells, observation, results or model area from MikeShe setup.");
      this.buttonReadMshe.UseVisualStyleBackColor = true;
      this.buttonReadMshe.Click += new System.EventHandler(this.buttonReadMshe_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(13, 125);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(141, 42);
      this.button2.TabIndex = 41;
      this.button2.Text = "Read in wells from shape";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.groupBox4);
      this.panel1.Controls.Add(this.groupBox3);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.listBoxIntakes);
      this.panel1.Controls.Add(this.label11);
      this.panel1.Controls.Add(this.propertyGrid1);
      this.panel1.Controls.Add(this.textBox4);
      this.panel1.Location = new System.Drawing.Point(687, 21);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(236, 817);
      this.panel1.TabIndex = 43;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.buttonNovanaShape);
      this.groupBox4.Controls.Add(this.buttonLSFile);
      this.groupBox4.Controls.Add(this.buttonMSheObs);
      this.groupBox4.Location = new System.Drawing.Point(16, 703);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(203, 107);
      this.groupBox4.TabIndex = 49;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Create files from observation data";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.MinNumber);
      this.groupBox3.Controls.Add(this.dateTimePicker2);
      this.groupBox3.Controls.Add(this.radioButtonMax);
      this.groupBox3.Controls.Add(this.label5);
      this.groupBox3.Controls.Add(this.dateTimePicker1);
      this.groupBox3.Controls.Add(this.radioButtonMin);
      this.groupBox3.Controls.Add(this.label3);
      this.groupBox3.Controls.Add(this.label4);
      this.groupBox3.Location = new System.Drawing.Point(16, 28);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(203, 135);
      this.groupBox3.TabIndex = 48;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Select on numbers of observations";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 174);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 43;
      this.label2.Text = "Count:";
      // 
      // panel3
      // 
      this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel3.Controls.Add(this.radioButton2);
      this.panel3.Controls.Add(this.radioButton1);
      this.panel3.Controls.Add(this.label6);
      this.panel3.Controls.Add(this.propertyWells);
      this.panel3.Controls.Add(this.label9);
      this.panel3.Controls.Add(this.listBoxWells);
      this.panel3.Controls.Add(this.textBoxWellsNumber);
      this.panel3.Location = new System.Drawing.Point(430, 21);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(236, 817);
      this.panel3.TabIndex = 44;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Enabled = false;
      this.radioButton2.Location = new System.Drawing.Point(17, 136);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(204, 17);
      this.radioButton2.TabIndex = 47;
      this.radioButton2.Text = "Show wells attached to selected plant";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new System.Drawing.Point(17, 113);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(91, 17);
      this.radioButton1.TabIndex = 46;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "Show all wells";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(14, 177);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 45;
      this.label6.Text = "Count:";
      // 
      // propertyWells
      // 
      this.propertyWells.Location = new System.Drawing.Point(17, 459);
      this.propertyWells.Name = "propertyWells";
      this.propertyWells.Size = new System.Drawing.Size(203, 241);
      this.propertyWells.TabIndex = 45;
      // 
      // panel4
      // 
      this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel4.Controls.Add(this.groupBox2);
      this.panel4.Controls.Add(this.groupBox1);
      this.panel4.Controls.Add(this.label14);
      this.panel4.Controls.Add(this.textBoxPlantCount);
      this.panel4.Controls.Add(this.propertyGridPlants);
      this.panel4.Controls.Add(this.label8);
      this.panel4.Controls.Add(this.listBoxAnlaeg);
      this.panel4.Location = new System.Drawing.Point(170, 21);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(236, 817);
      this.panel4.TabIndex = 45;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.textBoxMeanYearlyExt);
      this.groupBox2.Controls.Add(this.dateTimeEndExt);
      this.groupBox2.Controls.Add(this.label13);
      this.groupBox2.Controls.Add(this.label12);
      this.groupBox2.Controls.Add(this.dateTimeStartExt);
      this.groupBox2.Controls.Add(this.label10);
      this.groupBox2.Location = new System.Drawing.Point(14, 28);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(203, 135);
      this.groupBox2.TabIndex = 48;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Select on mean yearly extraction";
      // 
      // textBoxMeanYearlyExt
      // 
      this.textBoxMeanYearlyExt.Location = new System.Drawing.Point(140, 86);
      this.textBoxMeanYearlyExt.Name = "textBoxMeanYearlyExt";
      this.textBoxMeanYearlyExt.Size = new System.Drawing.Size(54, 20);
      this.textBoxMeanYearlyExt.TabIndex = 14;
      this.textBoxMeanYearlyExt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxMeanYearlyExt_KeyUp);
      this.textBoxMeanYearlyExt.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
      // 
      // dateTimeEndExt
      // 
      this.dateTimeEndExt.Location = new System.Drawing.Point(65, 49);
      this.dateTimeEndExt.Name = "dateTimeEndExt";
      this.dateTimeEndExt.Size = new System.Drawing.Size(129, 20);
      this.dateTimeEndExt.TabIndex = 13;
      this.dateTimeEndExt.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(6, 89);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(128, 13);
      this.label13.TabIndex = 16;
      this.label13.Text = "Mean yearly extraction >=";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(6, 53);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(50, 13);
      this.label12.TabIndex = 15;
      this.label12.Text = "End date";
      // 
      // dateTimeStartExt
      // 
      this.dateTimeStartExt.Location = new System.Drawing.Point(65, 23);
      this.dateTimeStartExt.Name = "dateTimeStartExt";
      this.dateTimeStartExt.Size = new System.Drawing.Size(129, 20);
      this.dateTimeStartExt.TabIndex = 12;
      this.dateTimeStartExt.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
      this.dateTimeStartExt.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 27);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(53, 13);
      this.label10.TabIndex = 14;
      this.label10.Text = "Start date";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.buttonMsheExt);
      this.groupBox1.Controls.Add(this.buttonNovanaExtract);
      this.groupBox1.Location = new System.Drawing.Point(14, 705);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(203, 98);
      this.groupBox1.TabIndex = 47;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Create files from extraction data";
      // 
      // buttonMsheExt
      // 
      this.buttonMsheExt.Enabled = false;
      this.buttonMsheExt.Location = new System.Drawing.Point(16, 46);
      this.buttonMsheExt.Name = "buttonMsheExt";
      this.buttonMsheExt.Size = new System.Drawing.Size(169, 23);
      this.buttonMsheExt.TabIndex = 46;
      this.buttonMsheExt.Text = "Extraction files for MikeShe";
      this.buttonMsheExt.UseVisualStyleBackColor = true;
      this.buttonMsheExt.Click += new System.EventHandler(this.buttonMsheExt_Click);
      // 
      // buttonNovanaExtract
      // 
      this.buttonNovanaExtract.Enabled = false;
      this.buttonNovanaExtract.Location = new System.Drawing.Point(16, 19);
      this.buttonNovanaExtract.Name = "buttonNovanaExtract";
      this.buttonNovanaExtract.Size = new System.Drawing.Size(169, 23);
      this.buttonNovanaExtract.TabIndex = 45;
      this.buttonNovanaExtract.Text = "NOVANA shape file for ArcMap";
      this.buttonNovanaExtract.UseVisualStyleBackColor = true;
      this.buttonNovanaExtract.Click += new System.EventHandler(this.button3_Click);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(11, 176);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(38, 13);
      this.label14.TabIndex = 47;
      this.label14.Text = "Count:";
      // 
      // textBoxPlantCount
      // 
      this.textBoxPlantCount.Enabled = false;
      this.textBoxPlantCount.Location = new System.Drawing.Point(150, 169);
      this.textBoxPlantCount.Name = "textBoxPlantCount";
      this.textBoxPlantCount.Size = new System.Drawing.Size(67, 20);
      this.textBoxPlantCount.TabIndex = 46;
      // 
      // propertyGridPlants
      // 
      this.propertyGridPlants.Location = new System.Drawing.Point(14, 458);
      this.propertyGridPlants.Name = "propertyGridPlants";
      this.propertyGridPlants.Size = new System.Drawing.Size(203, 241);
      this.propertyGridPlants.TabIndex = 46;
      // 
      // observationWellBindingSource
      // 
      this.observationWellBindingSource.DataSource = typeof(MikeSheWrapper.Tools.ObservationWell);
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(99, 841);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(100, 17);
      this.progressBar1.TabIndex = 46;
      this.progressBar1.Visible = false;
      // 
      // labelProgBar
      // 
      this.labelProgBar.AutoSize = true;
      this.labelProgBar.Location = new System.Drawing.Point(9, 845);
      this.labelProgBar.Name = "labelProgBar";
      this.labelProgBar.Size = new System.Drawing.Size(87, 13);
      this.labelProgBar.TabIndex = 47;
      this.labelProgBar.Text = "Writing dfs0-files:";
      this.labelProgBar.Visible = false;
      // 
      // HeadObservationsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(942, 861);
      this.Controls.Add(this.labelProgBar);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.panel4);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.buttonReadMshe);
      this.Controls.Add(this.ButtonReadWells);
      this.Name = "HeadObservationsView";
      this.Text = "Wells and observations";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.observationWellBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button ButtonReadWells;
    private System.Windows.Forms.TextBox textBoxWellsNumber;
    private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.TextBox MinNumber;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.DateTimePicker dateTimePicker2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button buttonMSheObs;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.ListBox listBoxIntakes;
    private System.Windows.Forms.BindingSource observationWellBindingSource;
    private System.Windows.Forms.Button buttonLSFile;
    private System.Windows.Forms.RadioButton radioButtonMax;
    private System.Windows.Forms.RadioButton radioButtonMin;
    private System.Windows.Forms.Button buttonNovanaShape;
    private System.Windows.Forms.ListBox listBoxAnlaeg;
    private System.Windows.Forms.ListBox listBoxWells;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Button buttonReadMshe;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.ToolTip ToolTipMSHEbutton;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.PropertyGrid propertyWells;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.PropertyGrid propertyGridPlants;
    private System.Windows.Forms.DateTimePicker dateTimeEndExt;
    private System.Windows.Forms.TextBox textBoxMeanYearlyExt;
    private System.Windows.Forms.DateTimePicker dateTimeStartExt;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox textBoxPlantCount;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button buttonNovanaExtract;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button buttonMsheExt;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label labelProgBar;
  }
}

