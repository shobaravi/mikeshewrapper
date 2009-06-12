namespace MikeSheWrapper.Viewer
{
  partial class GraphView
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.timeSeriesEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.timeSeriesEntryBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // chart1
      // 
      chartArea6.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea6);
      legend6.Name = "Legend1";
      this.chart1.Legends.Add(legend6);
      this.chart1.Location = new System.Drawing.Point(35, 31);
      this.chart1.Name = "chart1";
      series6.ChartArea = "ChartArea1";
      series6.Legend = "Legend1";
      series6.Name = "Series1";
      this.chart1.Series.Add(series6);
      this.chart1.Size = new System.Drawing.Size(563, 312);
      this.chart1.TabIndex = 0;
      this.chart1.Text = "chart1";
      // 
      // dataGridView1
      // 
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new System.Drawing.Point(35, 368);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(563, 74);
      this.dataGridView1.TabIndex = 1;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Location = new System.Drawing.Point(35, 499);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(563, 104);
      this.richTextBox1.TabIndex = 2;
      this.richTextBox1.Text = "";
      // 
      // textBox1
      // 
      this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timeSeriesEntryBindingSource, "Value", true));
      this.textBox1.Location = new System.Drawing.Point(35, 473);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(100, 20);
      this.textBox1.TabIndex = 3;
      // 
      // timeSeriesEntryBindingSource
      // 
      this.timeSeriesEntryBindingSource.DataSource = typeof(MikeSheWrapper.Tools.TimeSeriesEntry);
      // 
      // GraphView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.chart1);
      this.Name = "GraphView";
      this.Size = new System.Drawing.Size(632, 638);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.timeSeriesEntryBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    public System.Windows.Forms.DataGridView dataGridView1;
    public System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.BindingSource timeSeriesEntryBindingSource;
  }
}
