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
      this.button4 = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.button5 = new System.Windows.Forms.Button();
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
      this.label1 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.label6 = new System.Windows.Forms.Label();
      this.propertyWells = new System.Windows.Forms.PropertyGrid();
      this.panel4 = new System.Windows.Forms.Panel();
      this.buttonNovanaExtract = new System.Windows.Forms.Button();
      this.label14 = new System.Windows.Forms.Label();
      this.textBoxPlantCount = new System.Windows.Forms.TextBox();
      this.panel5 = new System.Windows.Forms.Panel();
      this.label7 = new System.Windows.Forms.Label();
      this.dateTimeEndExt = new System.Windows.Forms.DateTimePicker();
      this.textBoxMeanYearlyExt = new System.Windows.Forms.TextBox();
      this.dateTimeStartExt = new System.Windows.Forms.DateTimePicker();
      this.label10 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.propertyGridPlants = new System.Windows.Forms.PropertyGrid();
      this.observationWellBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel5.SuspendLayout();
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
      this.textBoxWellsNumber.Location = new System.Drawing.Point(153, 181);
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
      this.textBox4.Location = new System.Drawing.Point(144, 185);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(67, 20);
      this.textBox4.TabIndex = 9;
      // 
      // MinNumber
      // 
      this.MinNumber.Location = new System.Drawing.Point(127, 108);
      this.MinNumber.Name = "MinNumber";
      this.MinNumber.Size = new System.Drawing.Size(67, 20);
      this.MinNumber.TabIndex = 10;
      this.MinNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MinNumber_KeyUp);
      this.MinNumber.Validating += new System.ComponentModel.CancelEventHandler(this.MinNumber_Validating);
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Location = new System.Drawing.Point(65, 37);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(129, 20);
      this.dateTimePicker1.TabIndex = 12;
      this.dateTimePicker1.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
      this.dateTimePicker1.Validating += new System.ComponentModel.CancelEventHandler(this.MinNumber_Validating);
      // 
      // dateTimePicker2
      // 
      this.dateTimePicker2.Location = new System.Drawing.Point(65, 63);
      this.dateTimePicker2.Name = "dateTimePicker2";
      this.dateTimePicker2.Size = new System.Drawing.Size(129, 20);
      this.dateTimePicker2.TabIndex = 13;
      this.dateTimePicker2.Validating += new System.ComponentModel.CancelEventHandler(this.MinNumber_Validating);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 41);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(53, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "Start date";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 67);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(50, 13);
      this.label4.TabIndex = 15;
      this.label4.Text = "End date";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(62, 111);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(62, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "No. of obs.:";
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(16, 780);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(203, 23);
      this.button4.TabIndex = 18;
      this.button4.Text = "Create detailed time seriesfiles";
      this.button4.UseVisualStyleBackColor = true;
      // 
      // button5
      // 
      this.button5.Location = new System.Drawing.Point(17, 722);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(203, 23);
      this.button5.TabIndex = 28;
      this.button5.Text = "Create shape file";
      this.button5.UseVisualStyleBackColor = true;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Location = new System.Drawing.Point(16, 470);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(203, 241);
      this.propertyGrid1.TabIndex = 29;
      // 
      // listBoxIntakes
      // 
      this.listBoxIntakes.FormattingEnabled = true;
      this.listBoxIntakes.Location = new System.Drawing.Point(16, 211);
      this.listBoxIntakes.Name = "listBoxIntakes";
      this.listBoxIntakes.Size = new System.Drawing.Size(203, 251);
      this.listBoxIntakes.TabIndex = 30;
      this.listBoxIntakes.SelectedIndexChanged += new System.EventHandler(this.listBoxIntake_SelectedIndexChanged);
      // 
      // buttonLSFile
      // 
      this.buttonLSFile.Enabled = false;
      this.buttonLSFile.Location = new System.Drawing.Point(16, 751);
      this.buttonLSFile.Name = "buttonLSFile";
      this.buttonLSFile.Size = new System.Drawing.Size(203, 23);
      this.buttonLSFile.TabIndex = 31;
      this.buttonLSFile.Text = "Create LayerStatistics file";
      this.buttonLSFile.UseVisualStyleBackColor = true;
      // 
      // radioButtonMax
      // 
      this.radioButtonMax.AutoSize = true;
      this.radioButtonMax.Location = new System.Drawing.Point(9, 94);
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
      this.radioButtonMin.Location = new System.Drawing.Point(9, 117);
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
      this.buttonNovanaShape.Location = new System.Drawing.Point(16, 722);
      this.buttonNovanaShape.Name = "buttonNovanaShape";
      this.buttonNovanaShape.Size = new System.Drawing.Size(203, 23);
      this.buttonNovanaShape.TabIndex = 34;
      this.buttonNovanaShape.Text = "Create NOVANA shape file";
      this.buttonNovanaShape.UseVisualStyleBackColor = true;
      // 
      // listBoxAnlaeg
      // 
      this.listBoxAnlaeg.FormattingEnabled = true;
      this.listBoxAnlaeg.Location = new System.Drawing.Point(14, 209);
      this.listBoxAnlaeg.Name = "listBoxAnlaeg";
      this.listBoxAnlaeg.Size = new System.Drawing.Size(203, 251);
      this.listBoxAnlaeg.TabIndex = 35;
      this.listBoxAnlaeg.SelectedIndexChanged += new System.EventHandler(this.listBoxAnlaeg_SelectedIndexChanged_1);
      // 
      // listBoxWells
      // 
      this.listBoxWells.FormattingEnabled = true;
      this.listBoxWells.Location = new System.Drawing.Point(17, 208);
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
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(170, 13);
      this.label1.TabIndex = 42;
      this.label1.Text = "Select on numbers of observations";
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.listBoxIntakes);
      this.panel1.Controls.Add(this.label11);
      this.panel1.Controls.Add(this.propertyGrid1);
      this.panel1.Controls.Add(this.buttonLSFile);
      this.panel1.Controls.Add(this.button4);
      this.panel1.Controls.Add(this.buttonNovanaShape);
      this.panel1.Controls.Add(this.textBox4);
      this.panel1.Location = new System.Drawing.Point(687, 21);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(236, 817);
      this.panel1.TabIndex = 43;
      // 
      // panel2
      // 
      this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel2.Controls.Add(this.label1);
      this.panel2.Controls.Add(this.dateTimePicker2);
      this.panel2.Controls.Add(this.MinNumber);
      this.panel2.Controls.Add(this.dateTimePicker1);
      this.panel2.Controls.Add(this.label3);
      this.panel2.Controls.Add(this.label4);
      this.panel2.Controls.Add(this.radioButtonMin);
      this.panel2.Controls.Add(this.label5);
      this.panel2.Controls.Add(this.radioButtonMax);
      this.panel2.Location = new System.Drawing.Point(16, 32);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(203, 142);
      this.panel2.TabIndex = 44;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 188);
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
      this.panel3.Controls.Add(this.button5);
      this.panel3.Location = new System.Drawing.Point(430, 21);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(236, 817);
      this.panel3.TabIndex = 44;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Enabled = false;
      this.radioButton2.Location = new System.Drawing.Point(17, 150);
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
      this.radioButton1.Location = new System.Drawing.Point(17, 127);
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
      this.label6.Location = new System.Drawing.Point(14, 188);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 45;
      this.label6.Text = "Count:";
      // 
      // propertyWells
      // 
      this.propertyWells.Location = new System.Drawing.Point(17, 470);
      this.propertyWells.Name = "propertyWells";
      this.propertyWells.Size = new System.Drawing.Size(203, 241);
      this.propertyWells.TabIndex = 45;
      // 
      // panel4
      // 
      this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel4.Controls.Add(this.buttonNovanaExtract);
      this.panel4.Controls.Add(this.label14);
      this.panel4.Controls.Add(this.textBoxPlantCount);
      this.panel4.Controls.Add(this.panel5);
      this.panel4.Controls.Add(this.propertyGridPlants);
      this.panel4.Controls.Add(this.label8);
      this.panel4.Controls.Add(this.listBoxAnlaeg);
      this.panel4.Location = new System.Drawing.Point(170, 21);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(236, 817);
      this.panel4.TabIndex = 45;
      // 
      // buttonNovanaExtract
      // 
      this.buttonNovanaExtract.Enabled = false;
      this.buttonNovanaExtract.Location = new System.Drawing.Point(16, 722);
      this.buttonNovanaExtract.Name = "buttonNovanaExtract";
      this.buttonNovanaExtract.Size = new System.Drawing.Size(203, 23);
      this.buttonNovanaExtract.TabIndex = 45;
      this.buttonNovanaExtract.Text = "Create NOVANA shape file";
      this.buttonNovanaExtract.UseVisualStyleBackColor = true;
      this.buttonNovanaExtract.Click += new System.EventHandler(this.button3_Click);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(11, 188);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(38, 13);
      this.label14.TabIndex = 47;
      this.label14.Text = "Count:";
      // 
      // textBoxPlantCount
      // 
      this.textBoxPlantCount.Enabled = false;
      this.textBoxPlantCount.Location = new System.Drawing.Point(150, 181);
      this.textBoxPlantCount.Name = "textBoxPlantCount";
      this.textBoxPlantCount.Size = new System.Drawing.Size(67, 20);
      this.textBoxPlantCount.TabIndex = 46;
      // 
      // panel5
      // 
      this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel5.Controls.Add(this.label7);
      this.panel5.Controls.Add(this.dateTimeEndExt);
      this.panel5.Controls.Add(this.textBoxMeanYearlyExt);
      this.panel5.Controls.Add(this.dateTimeStartExt);
      this.panel5.Controls.Add(this.label10);
      this.panel5.Controls.Add(this.label12);
      this.panel5.Controls.Add(this.label13);
      this.panel5.Location = new System.Drawing.Point(14, 32);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(203, 142);
      this.panel5.TabIndex = 45;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 13);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(160, 13);
      this.label7.TabIndex = 42;
      this.label7.Text = "Select on mean yearly extraction";
      // 
      // dateTimeEndExt
      // 
      this.dateTimeEndExt.Location = new System.Drawing.Point(65, 63);
      this.dateTimeEndExt.Name = "dateTimeEndExt";
      this.dateTimeEndExt.Size = new System.Drawing.Size(129, 20);
      this.dateTimeEndExt.TabIndex = 13;
      this.dateTimeEndExt.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
      // 
      // textBoxMeanYearlyExt
      // 
      this.textBoxMeanYearlyExt.Location = new System.Drawing.Point(140, 100);
      this.textBoxMeanYearlyExt.Name = "textBoxMeanYearlyExt";
      this.textBoxMeanYearlyExt.Size = new System.Drawing.Size(54, 20);
      this.textBoxMeanYearlyExt.TabIndex = 14;
      this.textBoxMeanYearlyExt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxMeanYearlyExt_KeyUp);
      this.textBoxMeanYearlyExt.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
      // 
      // dateTimeStartExt
      // 
      this.dateTimeStartExt.Location = new System.Drawing.Point(65, 37);
      this.dateTimeStartExt.Name = "dateTimeStartExt";
      this.dateTimeStartExt.Size = new System.Drawing.Size(129, 20);
      this.dateTimeStartExt.TabIndex = 12;
      this.dateTimeStartExt.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
      this.dateTimeStartExt.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 41);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(53, 13);
      this.label10.TabIndex = 14;
      this.label10.Text = "Start date";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(6, 67);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(50, 13);
      this.label12.TabIndex = 15;
      this.label12.Text = "End date";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(6, 103);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(128, 13);
      this.label13.TabIndex = 16;
      this.label13.Text = "Mean yearly extraction >=";
      // 
      // propertyGridPlants
      // 
      this.propertyGridPlants.Location = new System.Drawing.Point(14, 470);
      this.propertyGridPlants.Name = "propertyGridPlants";
      this.propertyGridPlants.Size = new System.Drawing.Size(203, 241);
      this.propertyGridPlants.TabIndex = 46;
      // 
      // observationWellBindingSource
      // 
      this.observationWellBindingSource.DataSource = typeof(MikeSheWrapper.Tools.ObservationWell);
      // 
      // HeadObservationsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(942, 861);
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
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.observationWellBindingSource)).EndInit();
      this.ResumeLayout(false);

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
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button button5;
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
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.PropertyGrid propertyWells;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.PropertyGrid propertyGridPlants;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.DateTimePicker dateTimeEndExt;
    private System.Windows.Forms.TextBox textBoxMeanYearlyExt;
    private System.Windows.Forms.DateTimePicker dateTimeStartExt;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox textBoxPlantCount;
    private System.Windows.Forms.Button buttonNovanaExtract;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
  }
}

