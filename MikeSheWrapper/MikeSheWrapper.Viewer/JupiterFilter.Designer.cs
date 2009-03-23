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
      this.checkBoxNovana = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // checkBoxPejlinger
      // 
      this.checkBoxPejlinger.AutoSize = true;
      this.checkBoxPejlinger.Checked = true;
      this.checkBoxPejlinger.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxPejlinger.Location = new System.Drawing.Point(13, 41);
      this.checkBoxPejlinger.Name = "checkBoxPejlinger";
      this.checkBoxPejlinger.Size = new System.Drawing.Size(66, 17);
      this.checkBoxPejlinger.TabIndex = 0;
      this.checkBoxPejlinger.Text = "Pejlinger";
      this.checkBoxPejlinger.UseVisualStyleBackColor = true;
      // 
      // checkBoxIndvinding
      // 
      this.checkBoxIndvinding.AutoSize = true;
      this.checkBoxIndvinding.Checked = true;
      this.checkBoxIndvinding.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxIndvinding.Location = new System.Drawing.Point(12, 74);
      this.checkBoxIndvinding.Name = "checkBoxIndvinding";
      this.checkBoxIndvinding.Size = new System.Drawing.Size(117, 17);
      this.checkBoxIndvinding.TabIndex = 1;
      this.checkBoxIndvinding.Text = "Indvinding + anlæg";
      this.checkBoxIndvinding.UseVisualStyleBackColor = true;
      // 
      // checkBoxLitologi
      // 
      this.checkBoxLitologi.AutoSize = true;
      this.checkBoxLitologi.Checked = true;
      this.checkBoxLitologi.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxLitologi.Location = new System.Drawing.Point(12, 107);
      this.checkBoxLitologi.Name = "checkBoxLitologi";
      this.checkBoxLitologi.Size = new System.Drawing.Size(59, 17);
      this.checkBoxLitologi.TabIndex = 2;
      this.checkBoxLitologi.Text = "Litologi";
      this.checkBoxLitologi.UseVisualStyleBackColor = true;
      // 
      // checkBoxKemi
      // 
      this.checkBoxKemi.AutoSize = true;
      this.checkBoxKemi.Checked = true;
      this.checkBoxKemi.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxKemi.Location = new System.Drawing.Point(13, 140);
      this.checkBoxKemi.Name = "checkBoxKemi";
      this.checkBoxKemi.Size = new System.Drawing.Size(73, 17);
      this.checkBoxKemi.TabIndex = 3;
      this.checkBoxKemi.Text = "Kemi data";
      this.checkBoxKemi.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(13, 238);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "OK";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(146, 238);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "Cancel";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // checkBoxNovana
      // 
      this.checkBoxNovana.AutoSize = true;
      this.checkBoxNovana.Checked = true;
      this.checkBoxNovana.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBoxNovana.Location = new System.Drawing.Point(12, 173);
      this.checkBoxNovana.Name = "checkBoxNovana";
      this.checkBoxNovana.Size = new System.Drawing.Size(178, 17);
      this.checkBoxNovana.TabIndex = 6;
      this.checkBoxNovana.Text = "Ekstra data til NOAVANA output";
      this.checkBoxNovana.UseVisualStyleBackColor = true;
      // 
      // JupiterFilter
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(233, 273);
      this.Controls.Add(this.checkBoxNovana);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.checkBoxKemi);
      this.Controls.Add(this.checkBoxLitologi);
      this.Controls.Add(this.checkBoxIndvinding);
      this.Controls.Add(this.checkBoxPejlinger);
      this.Name = "JupiterFilter";
      this.Text = "Hvilke data skal indlæses?";
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
    private System.Windows.Forms.CheckBox checkBoxNovana;
  }
}