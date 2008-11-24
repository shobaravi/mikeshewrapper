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
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.MinNumber = new System.Windows.Forms.TextBox();
      this.button3 = new System.Windows.Forms.Button();
      this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
      this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
      this.SuspendLayout();
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.Filter = "Known file types (*.she, *.mdb, *.shp)|.*she;*.mdb;*.shp";
      this.openFileDialog1.ShowReadOnly = true;
      this.openFileDialog1.Title = "Select file with data for wells";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(421, 28);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(26, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(497, 31);
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
      this.label2.Location = new System.Drawing.Point(12, 91);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Read in observations";
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(120, 91);
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(295, 20);
      this.textBox3.TabIndex = 6;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(421, 89);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(26, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "...";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // openFileDialog2
      // 
      this.openFileDialog2.Filter = "Jupiter Access database (*.mdb)|*.mdb";
      // 
      // textBox4
      // 
      this.textBox4.Location = new System.Drawing.Point(594, 180);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(79, 20);
      this.textBox4.TabIndex = 9;
      // 
      // MinNumber
      // 
      this.MinNumber.Location = new System.Drawing.Point(458, 178);
      this.MinNumber.Name = "MinNumber";
      this.MinNumber.Size = new System.Drawing.Size(79, 20);
      this.MinNumber.TabIndex = 10;
      this.MinNumber.TextChanged += new System.EventHandler(this.MinNumber_TextChanged);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(679, 178);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 23);
      this.button3.TabIndex = 11;
      this.button3.Text = "Refresh";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // dateTimePicker1
      // 
      this.dateTimePicker1.Location = new System.Drawing.Point(15, 179);
      this.dateTimePicker1.Name = "dateTimePicker1";
      this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
      this.dateTimePicker1.TabIndex = 12;
      this.dateTimePicker1.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
      // 
      // dateTimePicker2
      // 
      this.dateTimePicker2.Location = new System.Drawing.Point(221, 179);
      this.dateTimePicker2.Name = "dateTimePicker2";
      this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
      this.dateTimePicker2.TabIndex = 13;
      // 
      // HeadObservationsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(801, 399);
      this.Controls.Add(this.dateTimePicker2);
      this.Controls.Add(this.dateTimePicker1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.MinNumber);
      this.Controls.Add(this.textBox4);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button1);
      this.Name = "HeadObservationsView";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.TextBox MinNumber;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.DateTimePicker dateTimePicker2;
  }
}

