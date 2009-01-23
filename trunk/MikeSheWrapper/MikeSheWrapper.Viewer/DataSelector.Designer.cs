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
      this.buttonUniqueValues = new System.Windows.Forms.Button();
      this.OkButton = new System.Windows.Forms.Button();
      this.ButtonCancel = new System.Windows.Forms.Button();
      this.buttonNE = new System.Windows.Forms.Button();
      this.buttonLike = new System.Windows.Forms.Button();
      this.buttonAnd = new System.Windows.Forms.Button();
      this.buttonGE = new System.Windows.Forms.Button();
      this.buttonGT = new System.Windows.Forms.Button();
      this.buttonOr = new System.Windows.Forms.Button();
      this.buttonLE = new System.Windows.Forms.Button();
      this.buttonLT = new System.Windows.Forms.Button();
      this.buttonNot = new System.Windows.Forms.Button();
      this.buttonParanteses = new System.Windows.Forms.Button();
      this.button_ = new System.Windows.Forms.Button();
      this.buttonLs = new System.Windows.Forms.Button();
      this.buttonPercent = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
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
      // buttonUniqueValues
      // 
      this.buttonUniqueValues.Location = new System.Drawing.Point(159, 302);
      this.buttonUniqueValues.Name = "buttonUniqueValues";
      this.buttonUniqueValues.Size = new System.Drawing.Size(107, 23);
      this.buttonUniqueValues.TabIndex = 5;
      this.buttonUniqueValues.Text = "Get unique values";
      this.buttonUniqueValues.UseVisualStyleBackColor = true;
      this.buttonUniqueValues.Click += new System.EventHandler(this.button1_Click);
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
      // ButtonCancel
      // 
      this.ButtonCancel.Location = new System.Drawing.Point(301, 451);
      this.ButtonCancel.Name = "ButtonCancel";
      this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
      this.ButtonCancel.TabIndex = 7;
      this.ButtonCancel.Text = "Cancel";
      this.ButtonCancel.UseVisualStyleBackColor = true;
      this.ButtonCancel.Click += new System.EventHandler(this.CancelButton_Click);
      // 
      // buttonNE
      // 
      this.buttonNE.Location = new System.Drawing.Point(60, 170);
      this.buttonNE.Name = "buttonNE";
      this.buttonNE.Size = new System.Drawing.Size(31, 23);
      this.buttonNE.TabIndex = 8;
      this.buttonNE.Text = "<>";
      this.buttonNE.UseVisualStyleBackColor = true;
      // 
      // buttonLike
      // 
      this.buttonLike.Location = new System.Drawing.Point(97, 170);
      this.buttonLike.Name = "buttonLike";
      this.buttonLike.Size = new System.Drawing.Size(42, 23);
      this.buttonLike.TabIndex = 9;
      this.buttonLike.Text = "Like";
      this.buttonLike.UseVisualStyleBackColor = true;
      // 
      // buttonAnd
      // 
      this.buttonAnd.Location = new System.Drawing.Point(97, 199);
      this.buttonAnd.Name = "buttonAnd";
      this.buttonAnd.Size = new System.Drawing.Size(42, 23);
      this.buttonAnd.TabIndex = 12;
      this.buttonAnd.Text = "And";
      this.buttonAnd.UseVisualStyleBackColor = true;
      // 
      // buttonGE
      // 
      this.buttonGE.Location = new System.Drawing.Point(60, 199);
      this.buttonGE.Name = "buttonGE";
      this.buttonGE.Size = new System.Drawing.Size(31, 23);
      this.buttonGE.TabIndex = 11;
      this.buttonGE.Text = ">=";
      this.buttonGE.UseVisualStyleBackColor = true;
      // 
      // buttonGT
      // 
      this.buttonGT.Location = new System.Drawing.Point(23, 199);
      this.buttonGT.Name = "buttonGT";
      this.buttonGT.Size = new System.Drawing.Size(31, 23);
      this.buttonGT.TabIndex = 10;
      this.buttonGT.Text = ">";
      this.buttonGT.UseVisualStyleBackColor = true;
      // 
      // buttonOr
      // 
      this.buttonOr.Location = new System.Drawing.Point(97, 228);
      this.buttonOr.Name = "buttonOr";
      this.buttonOr.Size = new System.Drawing.Size(42, 23);
      this.buttonOr.TabIndex = 15;
      this.buttonOr.Text = "Or";
      this.buttonOr.UseVisualStyleBackColor = true;
      // 
      // buttonLE
      // 
      this.buttonLE.Location = new System.Drawing.Point(60, 228);
      this.buttonLE.Name = "buttonLE";
      this.buttonLE.Size = new System.Drawing.Size(31, 23);
      this.buttonLE.TabIndex = 14;
      this.buttonLE.Text = "<=";
      this.buttonLE.UseVisualStyleBackColor = true;
      // 
      // buttonLT
      // 
      this.buttonLT.Location = new System.Drawing.Point(23, 228);
      this.buttonLT.Name = "buttonLT";
      this.buttonLT.Size = new System.Drawing.Size(31, 23);
      this.buttonLT.TabIndex = 13;
      this.buttonLT.Text = "<";
      this.buttonLT.UseVisualStyleBackColor = true;
      // 
      // buttonNot
      // 
      this.buttonNot.Location = new System.Drawing.Point(97, 257);
      this.buttonNot.Name = "buttonNot";
      this.buttonNot.Size = new System.Drawing.Size(42, 23);
      this.buttonNot.TabIndex = 18;
      this.buttonNot.Text = "Not";
      this.buttonNot.UseVisualStyleBackColor = true;
      // 
      // buttonParanteses
      // 
      this.buttonParanteses.Location = new System.Drawing.Point(60, 257);
      this.buttonParanteses.Name = "buttonParanteses";
      this.buttonParanteses.Size = new System.Drawing.Size(31, 23);
      this.buttonParanteses.TabIndex = 17;
      this.buttonParanteses.Text = "()";
      this.buttonParanteses.UseVisualStyleBackColor = true;
      // 
      // button_
      // 
      this.button_.Location = new System.Drawing.Point(23, 257);
      this.button_.Name = "button_";
      this.button_.Size = new System.Drawing.Size(31, 23);
      this.button_.TabIndex = 16;
      this.button_.Text = "_";
      this.button_.UseVisualStyleBackColor = true;
      // 
      // buttonLs
      // 
      this.buttonLs.Location = new System.Drawing.Point(97, 286);
      this.buttonLs.Name = "buttonLs";
      this.buttonLs.Size = new System.Drawing.Size(42, 23);
      this.buttonLs.TabIndex = 19;
      this.buttonLs.Text = "Is";
      this.buttonLs.UseVisualStyleBackColor = true;
      // 
      // buttonPercent
      // 
      this.buttonPercent.Location = new System.Drawing.Point(23, 286);
      this.buttonPercent.Name = "buttonPercent";
      this.buttonPercent.Size = new System.Drawing.Size(31, 23);
      this.buttonPercent.TabIndex = 20;
      this.buttonPercent.Text = "%";
      this.buttonPercent.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(20, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(103, 13);
      this.label1.TabIndex = 21;
      this.label1.Text = "Attributes in data set";
      // 
      // DataSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(399, 486);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonPercent);
      this.Controls.Add(this.buttonLs);
      this.Controls.Add(this.buttonNot);
      this.Controls.Add(this.buttonParanteses);
      this.Controls.Add(this.button_);
      this.Controls.Add(this.buttonOr);
      this.Controls.Add(this.buttonLE);
      this.Controls.Add(this.buttonLT);
      this.Controls.Add(this.buttonAnd);
      this.Controls.Add(this.buttonGE);
      this.Controls.Add(this.buttonGT);
      this.Controls.Add(this.buttonLike);
      this.Controls.Add(this.buttonNE);
      this.Controls.Add(this.ButtonCancel);
      this.Controls.Add(this.OkButton);
      this.Controls.Add(this.buttonUniqueValues);
      this.Controls.Add(this.richTextBoxSelectString);
      this.Controls.Add(this.listBoxUniqueValues);
      this.Controls.Add(this.EqualButton);
      this.Controls.Add(this.listBox1);
      this.Name = "DataSelector";
      this.Text = "Select by Attributes";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.Button EqualButton;
    private System.Windows.Forms.ListBox listBoxUniqueValues;
    private System.Windows.Forms.RichTextBox richTextBoxSelectString;
    private System.Windows.Forms.Button buttonUniqueValues;
    private System.Windows.Forms.Button OkButton;
    private System.Windows.Forms.Button ButtonCancel;
    private System.Windows.Forms.Button buttonNE;
    private System.Windows.Forms.Button buttonLike;
    private System.Windows.Forms.Button buttonAnd;
    private System.Windows.Forms.Button buttonGE;
    private System.Windows.Forms.Button buttonGT;
    private System.Windows.Forms.Button buttonOr;
    private System.Windows.Forms.Button buttonLE;
    private System.Windows.Forms.Button buttonLT;
    private System.Windows.Forms.Button buttonNot;
    private System.Windows.Forms.Button buttonParanteses;
    private System.Windows.Forms.Button button_;
    private System.Windows.Forms.Button buttonLs;
    private System.Windows.Forms.Button buttonPercent;
    private System.Windows.Forms.Label label1;
  }
}