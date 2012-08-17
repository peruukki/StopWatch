namespace StopWatch
{
  partial class WebFileChooser
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
      this.mAddressText = new System.Windows.Forms.TextBox();
      this.mAddressLabel = new System.Windows.Forms.Label();
      this.mButtonOpen = new System.Windows.Forms.Button();
      this.mButtonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // mAddressText
      // 
      this.mAddressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mAddressText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.mAddressText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.mAddressText.Location = new System.Drawing.Point(12, 35);
      this.mAddressText.Name = "mAddressText";
      this.mAddressText.Size = new System.Drawing.Size(375, 24);
      this.mAddressText.TabIndex = 0;
      this.mAddressText.Text = "http://aikataulut.hsl.fi/pysakit/fi/";
      // 
      // mAddressLabel
      // 
      this.mAddressLabel.AutoSize = true;
      this.mAddressLabel.Location = new System.Drawing.Point(12, 9);
      this.mAddressLabel.Name = "mAddressLabel";
      this.mAddressLabel.Size = new System.Drawing.Size(212, 17);
      this.mAddressLabel.TabIndex = 1;
      this.mAddressLabel.Text = "Internet address of the timetable:";
      // 
      // mButtonOpen
      // 
      this.mButtonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.mButtonOpen.Location = new System.Drawing.Point(11, 71);
      this.mButtonOpen.Name = "mButtonOpen";
      this.mButtonOpen.Size = new System.Drawing.Size(75, 25);
      this.mButtonOpen.TabIndex = 2;
      this.mButtonOpen.Text = "&Open";
      this.mButtonOpen.UseVisualStyleBackColor = true;
      this.mButtonOpen.Click += new System.EventHandler(this.mButtonOpen_Click);
      // 
      // mButtonCancel
      // 
      this.mButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.mButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.mButtonCancel.Location = new System.Drawing.Point(312, 71);
      this.mButtonCancel.Name = "mButtonCancel";
      this.mButtonCancel.Size = new System.Drawing.Size(75, 25);
      this.mButtonCancel.TabIndex = 3;
      this.mButtonCancel.Text = "&Cancel";
      this.mButtonCancel.UseVisualStyleBackColor = true;
      this.mButtonCancel.Click += new System.EventHandler(this.mButtonCancel_Click);
      // 
      // WebFileChooser
      // 
      this.AcceptButton = this.mButtonOpen;
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoSize = true;
      this.CancelButton = this.mButtonCancel;
      this.ClientSize = new System.Drawing.Size(399, 106);
      this.Controls.Add(this.mButtonCancel);
      this.Controls.Add(this.mButtonOpen);
      this.Controls.Add(this.mAddressLabel);
      this.Controls.Add(this.mAddressText);
      this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "WebFileChooser";
      this.ShowIcon = false;
      this.Text = "Open Timetable on the Web";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox mAddressText;
    private System.Windows.Forms.Label mAddressLabel;
    private System.Windows.Forms.Button mButtonOpen;
    private System.Windows.Forms.Button mButtonCancel;

  }
}