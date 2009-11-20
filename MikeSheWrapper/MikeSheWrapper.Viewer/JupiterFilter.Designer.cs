namespace MikeSheWrapper.Viewer
{
  partial class JupiterFilter
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
        this.checkBoxPejlinger = new System.Windows.Forms.CheckBox();
        this.checkBoxIndvinding = new System.Windows.Forms.CheckBox();
        this.checkBoxLitologi = new System.Windows.Forms.CheckBox();
        this.checkBoxKemi = new System.Windows.Forms.CheckBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.checkBoxWell = new System.Windows.Forms.CheckBox();
        this.checkBox1Ro = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // checkBoxPejlinger
        // 
        this.checkBoxPejlinger.AutoSize = true;
        this.checkBoxPejlinger.Location = new System.Drawing.Point(12, 28);
        this.checkBoxPejlinger.Name = "checkBoxPejlinger";
        this.checkBoxPejlinger.Size = new System.Drawing.Size(115, 17);
        this.checkBoxPejlinger.TabIndex = 0;
        this.checkBoxPejlinger.Text = "Head observations";
        this.checkBoxPejlinger.UseVisualStyleBackColor = true;
        this.checkBoxPejlinger.CheckedChanged += new System.EventHandler(this.checkBoxPejlinger_CheckedChanged);
        // 
        // checkBoxIndvinding
        // 
        this.checkBoxIndvinding.AutoSize = true;
        this.checkBoxIndvinding.Location = new System.Drawing.Point(12, 86);
        this.checkBoxIndvinding.Name = "checkBoxIndvinding";
        this.checkBoxIndvinding.Size = new System.Drawing.Size(130, 17);
        this.checkBoxIndvinding.TabIndex = 1;
        this.checkBoxIndvinding.Text = "Extractions and plants";
        this.checkBoxIndvinding.UseVisualStyleBackColor = true;
        // 
        // checkBoxLitologi
        // 
        this.checkBoxLitologi.AutoSize = true;
        this.checkBoxLitologi.Location = new System.Drawing.Point(12, 119);
        this.checkBoxLitologi.Name = "checkBoxLitologi";
        this.checkBoxLitologi.Size = new System.Drawing.Size(68, 17);
        this.checkBoxLitologi.TabIndex = 2;
        this.checkBoxLitologi.Text = "Lithology";
        this.checkBoxLitologi.UseVisualStyleBackColor = true;
        // 
        // checkBoxKemi
        // 
        this.checkBoxKemi.AutoSize = true;
        this.checkBoxKemi.Location = new System.Drawing.Point(13, 152);
        this.checkBoxKemi.Name = "checkBoxKemi";
        this.checkBoxKemi.Size = new System.Drawing.Size(71, 17);
        this.checkBoxKemi.TabIndex = 3;
        this.checkBoxKemi.Text = "Chemistry";
        this.checkBoxKemi.UseVisualStyleBackColor = true;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(13, 224);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 4;
        this.button1.Text = "OK";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(140, 224);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 5;
        this.button2.Text = "Cancel";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // checkBoxWell
        // 
        this.checkBoxWell.AutoSize = true;
        this.checkBoxWell.Enabled = false;
        this.checkBoxWell.Location = new System.Drawing.Point(12, 185);
        this.checkBoxWell.Name = "checkBoxWell";
        this.checkBoxWell.Size = new System.Drawing.Size(52, 17);
        this.checkBoxWell.TabIndex = 6;
        this.checkBoxWell.Text = "Wells";
        this.checkBoxWell.UseVisualStyleBackColor = true;
        // 
        // checkBox1Ro
        // 
        this.checkBox1Ro.AutoSize = true;
        this.checkBox1Ro.Enabled = false;
        this.checkBox1Ro.Location = new System.Drawing.Point(31, 51);
        this.checkBox1Ro.Name = "checkBox1Ro";
        this.checkBox1Ro.Size = new System.Drawing.Size(83, 17);
        this.checkBox1Ro.TabIndex = 7;
        this.checkBox1Ro.Text = "Kun RO-pejl";
        this.checkBox1Ro.UseVisualStyleBackColor = true;
        // 
        // JupiterFilter
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(240, 269);
        this.Controls.Add(this.checkBox1Ro);
        this.Controls.Add(this.checkBoxWell);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.checkBoxKemi);
        this.Controls.Add(this.checkBoxLitologi);
        this.Controls.Add(this.checkBoxIndvinding);
        this.Controls.Add(this.checkBoxPejlinger);
        this.Name = "JupiterFilter";
        this.Text = "Select data to read.";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox checkBoxPejlinger;
    private System.Windows.Forms.CheckBox checkBoxIndvinding;
    private System.Windows.Forms.CheckBox checkBoxLitologi;
    private System.Windows.Forms.CheckBox checkBoxKemi;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.CheckBox checkBoxWell;
    private System.Windows.Forms.CheckBox checkBox1Ro;
  }
}