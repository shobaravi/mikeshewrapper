namespace MikeSheWrapper.Viewer
{
  partial class Form1
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
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.buttonBack = new System.Windows.Forms.Button();
      this.buttonForward = new System.Windows.Forms.Button();
      this.graphView1 = new MikeSheWrapper.Viewer.GraphView();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(803, 557);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Finish";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(306, 557);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 2;
      this.button2.Text = "Refresh";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(28, 73);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(101, 20);
      this.textBox1.TabIndex = 3;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Location = new System.Drawing.Point(28, 99);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(241, 361);
      this.propertyGrid1.TabIndex = 4;
      // 
      // buttonBack
      // 
      this.buttonBack.Location = new System.Drawing.Point(453, 557);
      this.buttonBack.Name = "buttonBack";
      this.buttonBack.Size = new System.Drawing.Size(67, 23);
      this.buttonBack.TabIndex = 5;
      this.buttonBack.Text = "Previous";
      this.buttonBack.UseVisualStyleBackColor = true;
      this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
      // 
      // buttonForward
      // 
      this.buttonForward.Location = new System.Drawing.Point(546, 557);
      this.buttonForward.Name = "buttonForward";
      this.buttonForward.Size = new System.Drawing.Size(62, 23);
      this.buttonForward.TabIndex = 6;
      this.buttonForward.Text = "Next";
      this.buttonForward.UseVisualStyleBackColor = true;
      this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
      // 
      // graphView1
      // 
      this.graphView1.Location = new System.Drawing.Point(306, 12);
      this.graphView1.Name = "graphView1";
      this.graphView1.Size = new System.Drawing.Size(572, 500);
      this.graphView1.TabIndex = 0;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Location = new System.Drawing.Point(306, 472);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(534, 58);
      this.richTextBox1.TabIndex = 7;
      this.richTextBox1.Text = "";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(903, 601);
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.buttonForward);
      this.Controls.Add(this.buttonBack);
      this.Controls.Add(this.propertyGrid1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.graphView1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    public GraphView graphView1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.Button buttonBack;
    private System.Windows.Forms.Button buttonForward;
    private System.Windows.Forms.RichTextBox richTextBox1;
  }
}