namespace MikeSheWrapper.Viewer
{
  partial class GridView
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
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.TimeStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Head = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.CellTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.CellBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.CellThickness = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.K_hor = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.K_ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.DrainFlow = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.RiverFlow = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Extraction = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column,
            this.Row,
            this.Layer,
            this.TimeStep,
            this.Head,
            this.CellTop,
            this.CellBottom,
            this.CellThickness,
            this.K_hor,
            this.K_ver,
            this.DrainFlow,
            this.RiverFlow,
            this.Extraction});
      this.dataGridView1.Location = new System.Drawing.Point(12, 121);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(956, 150);
      this.dataGridView1.TabIndex = 11;
      // 
      // Column
      // 
      this.Column.HeaderText = "Column";
      this.Column.Name = "Column";
      this.Column.Width = 67;
      // 
      // Row
      // 
      this.Row.HeaderText = "Row";
      this.Row.Name = "Row";
      this.Row.Width = 54;
      // 
      // Layer
      // 
      this.Layer.HeaderText = "Layer";
      this.Layer.Name = "Layer";
      this.Layer.Width = 58;
      // 
      // TimeStep
      // 
      this.TimeStep.HeaderText = "TimeStep";
      this.TimeStep.Name = "TimeStep";
      this.TimeStep.Width = 77;
      // 
      // Head
      // 
      this.Head.HeaderText = "Head";
      this.Head.Name = "Head";
      this.Head.ReadOnly = true;
      this.Head.Width = 58;
      // 
      // CellTop
      // 
      this.CellTop.HeaderText = "CellTop";
      this.CellTop.Name = "CellTop";
      this.CellTop.ReadOnly = true;
      this.CellTop.Width = 68;
      // 
      // CellBottom
      // 
      this.CellBottom.HeaderText = "CellBottom";
      this.CellBottom.Name = "CellBottom";
      this.CellBottom.ReadOnly = true;
      this.CellBottom.Width = 82;
      // 
      // CellThickness
      // 
      this.CellThickness.HeaderText = "CellThickness";
      this.CellThickness.Name = "CellThickness";
      this.CellThickness.ReadOnly = true;
      this.CellThickness.Width = 98;
      // 
      // K_hor
      // 
      this.K_hor.HeaderText = "K_hor";
      this.K_hor.Name = "K_hor";
      this.K_hor.ReadOnly = true;
      this.K_hor.Width = 60;
      // 
      // K_ver
      // 
      this.K_ver.HeaderText = "K_ver";
      this.K_ver.Name = "K_ver";
      this.K_ver.ReadOnly = true;
      this.K_ver.Width = 60;
      // 
      // DrainFlow
      // 
      this.DrainFlow.HeaderText = "DrainFlow";
      this.DrainFlow.Name = "DrainFlow";
      this.DrainFlow.Width = 79;
      // 
      // RiverFlow
      // 
      this.RiverFlow.HeaderText = "RiverFlow";
      this.RiverFlow.Name = "RiverFlow";
      this.RiverFlow.Width = 79;
      // 
      // Extraction
      // 
      this.Extraction.HeaderText = "Extraction";
      this.Extraction.Name = "Extraction";
      this.Extraction.Width = 79;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 33);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(472, 20);
      this.textBox1.TabIndex = 12;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "she";
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(494, 34);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(36, 19);
      this.button1.TabIndex = 13;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(32, 81);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(106, 28);
      this.button2.TabIndex = 14;
      this.button2.Text = "Refresh";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new System.Drawing.Point(557, 37);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(119, 17);
      this.radioButton1.TabIndex = 15;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "MikeShe numbering";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new System.Drawing.Point(557, 60);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(98, 17);
      this.radioButton2.TabIndex = 16;
      this.radioButton2.Text = "DFS numbering";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // GridView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1001, 395);
      this.Controls.Add(this.radioButton2);
      this.Controls.Add(this.radioButton1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.dataGridView1);
      this.Name = "GridView";
      this.Text = "GridView";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column;
    private System.Windows.Forms.DataGridViewTextBoxColumn Row;
    private System.Windows.Forms.DataGridViewTextBoxColumn Layer;
    private System.Windows.Forms.DataGridViewTextBoxColumn TimeStep;
    private System.Windows.Forms.DataGridViewTextBoxColumn Head;
    private System.Windows.Forms.DataGridViewTextBoxColumn CellTop;
    private System.Windows.Forms.DataGridViewTextBoxColumn CellBottom;
    private System.Windows.Forms.DataGridViewTextBoxColumn CellThickness;
    private System.Windows.Forms.DataGridViewTextBoxColumn K_hor;
    private System.Windows.Forms.DataGridViewTextBoxColumn K_ver;
    private System.Windows.Forms.DataGridViewTextBoxColumn DrainFlow;
    private System.Windows.Forms.DataGridViewTextBoxColumn RiverFlow;
    private System.Windows.Forms.DataGridViewTextBoxColumn Extraction;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.RadioButton radioButton2;
  }
}