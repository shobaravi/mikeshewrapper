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
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.ButtonReadWells = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxObsFile = new System.Windows.Forms.TextBox();
      this.buttonReadObs = new System.Windows.Forms.Button();
      this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.MinNumber = new System.Windows.Forms.TextBox();
      this.RefreshButton = new System.Windows.Forms.Button();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.button4 = new System.Windows.Forms.Button();
      this.textBox5 = new System.Windows.Forms.TextBox();
      this.buttonSelectMShe = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.button5 = new System.Windows.Forms.Button();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.OpenSheFileForSelection = new System.Windows.Forms.OpenFileDialog();
      this.observationWellBindingSource = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.observationWellBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.Filter = "Known file types (*.she, *.mdb, *.shp)|*.she;*.mdb;*.shp";
      this.openFileDialog1.ShowReadOnly = true;
      this.openFileDialog1.Title = "Select file with data for wells";
      // 
      // ButtonReadWells
      // 
      this.ButtonReadWells.Location = new System.Drawing.Point(421, 28);
      this.ButtonReadWells.Name = "ButtonReadWells";
      this.ButtonReadWells.Size = new System.Drawing.Size(26, 23);
      this.ButtonReadWells.TabIndex = 0;
      this.ButtonReadWells.Text = "...";
      this.ButtonReadWells.UseVisualStyleBackColor = true;
      this.ButtonReadWells.Click += new System.EventHandler(this.button1_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(540, 31);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(79, 20);
      this.textBox1.TabIndex = 1;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(120, 30);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(295, 20);
      this.textBox2.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 34);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(70, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Read in wells";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 83);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Read in observations";
      // 
      // textBoxObsFile
      // 
      this.textBoxObsFile.Location = new System.Drawing.Point(120, 83);
      this.textBoxObsFile.Name = "textBoxObsFile";
      this.textBoxObsFile.Size = new System.Drawing.Size(295, 20);
      this.textBoxObsFile.TabIndex = 6;
      // 
      // buttonReadObs
      // 
      this.buttonReadObs.Location = new System.Drawing.Point(421, 81);
      this.buttonReadObs.Name = "buttonReadObs";
      this.buttonReadObs.Size = new System.Drawing.Size(26, 23);
      this.buttonReadObs.TabIndex = 5;
      this.buttonReadObs.Text = "...";
      this.buttonReadObs.UseVisualStyleBackColor = true;
      this.buttonReadObs.Click += new System.EventHandler(this.button2_Click);
      // 
      // openFileDialog2
      // 
      this.openFileDialog2.Filter = "Jupiter Access database (*.mdb)|*.mdb";
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(368, 294);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(79, 20);
      this.textBox4.TabIndex = 9;
      // 
      // MinNumber
      // 
      this.MinNumber.Location = new System.Drawing.Point(368, 218);
      this.MinNumber.Name = "MinNumber";
      this.MinNumber.Size = new System.Drawing.Size(79, 20);
      this.MinNumber.TabIndex = 10;
      // 
      // RefreshButton
      // 
      this.RefreshButton.Location = new System.Drawing.Point(544, 216);
      this.RefreshButton.Name = "RefreshButton";
      this.RefreshButton.Size = new System.Drawing.Size(75, 23);
      this.RefreshButton.TabIndex = 11;
      this.RefreshButton.Text = "Refresh";
      this.RefreshButton.UseVisualStyleBackColor = true;
      this.RefreshButton.Click += new System.EventHandler(this.button3_Click);
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Location = new System.Drawing.Point(15, 218);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(141, 20);
      this.dateTimePicker1.TabIndex = 12;
      this.dateTimePicker1.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
      // 
      // dateTimePicker2
      // 
      this.dateTimePicker2.Location = new System.Drawing.Point(190, 218);
      this.dateTimePicker2.Name = "dateTimePicker2";
      this.dateTimePicker2.Size = new System.Drawing.Size(133, 20);
      this.dateTimePicker2.TabIndex = 13;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(15, 199);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(53, 13);
      this.label3.TabIndex = 14;
      this.label3.Text = "Start date";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(187, 199);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(50, 13);
      this.label4.TabIndex = 15;
      this.label4.Text = "End date";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(365, 199);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(82, 13);
      this.label5.TabIndex = 16;
      this.label5.Text = "Min. No. of obs.";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(537, 9);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(62, 13);
      this.label6.TabIndex = 17;
      this.label6.Text = "No. of wells";
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(15, 347);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(104, 23);
      this.button4.TabIndex = 18;
      this.button4.Text = "Create dfs0-files";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // textBox5
      // 
      this.textBox5.Location = new System.Drawing.Point(120, 140);
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new System.Drawing.Size(295, 20);
      this.textBox5.TabIndex = 21;
      // 
      // buttonSelectMShe
      // 
      this.buttonSelectMShe.Location = new System.Drawing.Point(421, 138);
      this.buttonSelectMShe.Name = "buttonSelectMShe";
      this.buttonSelectMShe.Size = new System.Drawing.Size(26, 23);
      this.buttonSelectMShe.TabIndex = 20;
      this.buttonSelectMShe.Text = "...";
      this.buttonSelectMShe.UseVisualStyleBackColor = true;
      this.buttonSelectMShe.Click += new System.EventHandler(this.buttonSelectMShe_Click);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(15, 143);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(96, 13);
      this.label7.TabIndex = 22;
      this.label7.Text = "Select by MikeShe";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(264, 297);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(62, 13);
      this.label10.TabIndex = 27;
      this.label10.Text = "No. of wells";
      // 
      // button5
      // 
      this.button5.Location = new System.Drawing.Point(143, 347);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(104, 23);
      this.button5.TabIndex = 28;
      this.button5.Text = "Create shape file";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new System.EventHandler(this.button5_Click);
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Location = new System.Drawing.Point(465, 288);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(217, 296);
      this.propertyGrid1.TabIndex = 29;
      // 
      // listBox1
      // 
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new System.Drawing.Point(267, 320);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(180, 264);
      this.listBox1.TabIndex = 30;
      this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
      // 
      // OpenSheFileForSelection
      // 
      this.OpenSheFileForSelection.Filter = "MikeShe imput file(*.she)|*.she";
      // 
      // observationWellBindingSource
      // 
      this.observationWellBindingSource.DataSource = typeof(MikeSheWrapper.InputDataPreparation.ObservationWell);
      // 
      // HeadObservationsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(806, 617);
      this.Controls.Add(this.listBox1);
      this.Controls.Add(this.propertyGrid1);
      this.Controls.Add(this.button5);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.textBox5);
      this.Controls.Add(this.buttonSelectMShe);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.dateTimePicker2);
      this.Controls.Add(this.dateTimePicker1);
      this.Controls.Add(this.RefreshButton);
      this.Controls.Add(this.MinNumber);
      this.Controls.Add(this.textBox4);
      this.Controls.Add(this.textBoxObsFile);
      this.Controls.Add(this.buttonReadObs);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.ButtonReadWells);
      this.Name = "HeadObservationsView";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.observationWellBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Button ButtonReadWells;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxObsFile;
    private System.Windows.Forms.Button buttonReadObs;
    private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.TextBox MinNumber;
    private System.Windows.Forms.Button RefreshButton;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.DateTimePicker dateTimePicker2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.Button buttonSelectMShe;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.BindingSource observationWellBindingSource;
    private System.Windows.Forms.OpenFileDialog OpenSheFileForSelection;
  }
}

