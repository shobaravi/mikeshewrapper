namespace MikeSheWrapper.Viewer
{
  partial class DataSelector
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
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.EqualButton = new System.Windows.Forms.Button();
      this.listBoxUniqueValues = new System.Windows.Forms.ListBox();
      this.richTextBoxSelectString = new System.Windows.Forms.RichTextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.OkButton = new System.Windows.Forms.Button();
      this.ButtonCancel = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button5 = new System.Windows.Forms.Button();
      this.button6 = new System.Windows.Forms.Button();
      this.button7 = new System.Windows.Forms.Button();
      this.button8 = new System.Windows.Forms.Button();
      this.button9 = new System.Windows.Forms.Button();
      this.button10 = new System.Windows.Forms.Button();
      this.button11 = new System.Windows.Forms.Button();
      this.button12 = new System.Windows.Forms.Button();
      this.button13 = new System.Windows.Forms.Button();
      this.button14 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // listBox1
      // 
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new System.Drawing.Point(23, 43);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(353, 121);
      this.listBox1.TabIndex = 0;
      this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
      // 
      // EqualButton
      // 
      this.EqualButton.Location = new System.Drawing.Point(23, 170);
      this.EqualButton.Name = "EqualButton";
      this.EqualButton.Size = new System.Drawing.Size(31, 23);
      this.EqualButton.TabIndex = 2;
      this.EqualButton.Text = "=";
      this.EqualButton.UseVisualStyleBackColor = true;
      this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
      // 
      // listBoxUniqueValues
      // 
      this.listBoxUniqueValues.FormattingEnabled = true;
      this.listBoxUniqueValues.Location = new System.Drawing.Point(159, 175);
      this.listBoxUniqueValues.Name = "listBoxUniqueValues";
      this.listBoxUniqueValues.Size = new System.Drawing.Size(216, 121);
      this.listBoxUniqueValues.TabIndex = 3;
      // 
      // richTextBoxSelectString
      // 
      this.richTextBoxSelectString.Location = new System.Drawing.Point(23, 343);
      this.richTextBoxSelectString.Name = "richTextBoxSelectString";
      this.richTextBoxSelectString.Size = new System.Drawing.Size(352, 96);
      this.richTextBoxSelectString.TabIndex = 4;
      this.richTextBoxSelectString.Text = "";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(159, 302);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(107, 23);
      this.button1.TabIndex = 5;
      this.button1.Text = "Get unique values";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // OkButton
      // 
      this.OkButton.Location = new System.Drawing.Point(204, 451);
      this.OkButton.Name = "OkButton";
      this.OkButton.Size = new System.Drawing.Size(75, 23);
      this.OkButton.TabIndex = 6;
      this.OkButton.Text = "OK";
      this.OkButton.UseVisualStyleBackColor = true;
      this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
      // 
      // CancelButton
      // 
      this.ButtonCancel.Location = new System.Drawing.Point(301, 451);
      this.ButtonCancel.Name = "CancelButton";
      this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
      this.ButtonCancel.TabIndex = 7;
      this.ButtonCancel.Text = "Cancel";
      this.ButtonCancel.UseVisualStyleBackColor = true;
      this.ButtonCancel.Click += new System.EventHandler(this.CancelButton_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(60, 170);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(31, 23);
      this.button2.TabIndex = 8;
      this.button2.Text = "<>";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(97, 170);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(42, 23);
      this.button3.TabIndex = 9;
      this.button3.Text = "Like";
      this.button3.UseVisualStyleBackColor = true;
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(97, 199);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(42, 23);
      this.button4.TabIndex = 12;
      this.button4.Text = "And";
      this.button4.UseVisualStyleBackColor = true;
      // 
      // button5
      // 
      this.button5.Location = new System.Drawing.Point(60, 199);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(31, 23);
      this.button5.TabIndex = 11;
      this.button5.Text = ">=";
      this.button5.UseVisualStyleBackColor = true;
      // 
      // button6
      // 
      this.button6.Location = new System.Drawing.Point(23, 199);
      this.button6.Name = "button6";
      this.button6.Size = new System.Drawing.Size(31, 23);
      this.button6.TabIndex = 10;
      this.button6.Text = ">";
      this.button6.UseVisualStyleBackColor = true;
      // 
      // button7
      // 
      this.button7.Location = new System.Drawing.Point(97, 228);
      this.button7.Name = "button7";
      this.button7.Size = new System.Drawing.Size(42, 23);
      this.button7.TabIndex = 15;
      this.button7.Text = "Or";
      this.button7.UseVisualStyleBackColor = true;
      // 
      // button8
      // 
      this.button8.Location = new System.Drawing.Point(60, 228);
      this.button8.Name = "button8";
      this.button8.Size = new System.Drawing.Size(31, 23);
      this.button8.TabIndex = 14;
      this.button8.Text = "<=";
      this.button8.UseVisualStyleBackColor = true;
      // 
      // button9
      // 
      this.button9.Location = new System.Drawing.Point(23, 228);
      this.button9.Name = "button9";
      this.button9.Size = new System.Drawing.Size(31, 23);
      this.button9.TabIndex = 13;
      this.button9.Text = "<";
      this.button9.UseVisualStyleBackColor = true;
      // 
      // button10
      // 
      this.button10.Location = new System.Drawing.Point(97, 257);
      this.button10.Name = "button10";
      this.button10.Size = new System.Drawing.Size(42, 23);
      this.button10.TabIndex = 18;
      this.button10.Text = "Not";
      this.button10.UseVisualStyleBackColor = true;
      // 
      // button11
      // 
      this.button11.Location = new System.Drawing.Point(60, 257);
      this.button11.Name = "button11";
      this.button11.Size = new System.Drawing.Size(31, 23);
      this.button11.TabIndex = 17;
      this.button11.Text = "()";
      this.button11.UseVisualStyleBackColor = true;
      // 
      // button12
      // 
      this.button12.Location = new System.Drawing.Point(23, 257);
      this.button12.Name = "button12";
      this.button12.Size = new System.Drawing.Size(31, 23);
      this.button12.TabIndex = 16;
      this.button12.Text = "_";
      this.button12.UseVisualStyleBackColor = true;
      // 
      // button13
      // 
      this.button13.Location = new System.Drawing.Point(97, 286);
      this.button13.Name = "button13";
      this.button13.Size = new System.Drawing.Size(42, 23);
      this.button13.TabIndex = 19;
      this.button13.Text = "Is";
      this.button13.UseVisualStyleBackColor = true;
      // 
      // button14
      // 
      this.button14.Location = new System.Drawing.Point(23, 286);
      this.button14.Name = "button14";
      this.button14.Size = new System.Drawing.Size(31, 23);
      this.button14.TabIndex = 20;
      this.button14.Text = "%";
      this.button14.UseVisualStyleBackColor = true;
      // 
      // DataSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(399, 486);
      this.Controls.Add(this.button14);
      this.Controls.Add(this.button13);
      this.Controls.Add(this.button10);
      this.Controls.Add(this.button11);
      this.Controls.Add(this.button12);
      this.Controls.Add(this.button7);
      this.Controls.Add(this.button8);
      this.Controls.Add(this.button9);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.button5);
      this.Controls.Add(this.button6);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.ButtonCancel);
      this.Controls.Add(this.OkButton);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.richTextBoxSelectString);
      this.Controls.Add(this.listBoxUniqueValues);
      this.Controls.Add(this.EqualButton);
      this.Controls.Add(this.listBox1);
      this.Name = "DataSelector";
      this.Text = "Select by Attributes";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.Button EqualButton;
    private System.Windows.Forms.ListBox listBoxUniqueValues;
    private System.Windows.Forms.RichTextBox richTextBoxSelectString;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button OkButton;
    private System.Windows.Forms.Button ButtonCancel;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Button button10;
    private System.Windows.Forms.Button button11;
    private System.Windows.Forms.Button button12;
    private System.Windows.Forms.Button button13;
    private System.Windows.Forms.Button button14;
  }
}