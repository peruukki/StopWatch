using System.Drawing;
using System.Windows.Forms;
namespace StopWatch
{
  partial class MainWindow
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
      this.mStopTimesPanel = new System.Windows.Forms.TableLayoutPanel();
      this.mBusPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.mTimeNowLabel = new System.Windows.Forms.Label();
      this.mStopDelayChooser = new System.Windows.Forms.NumericUpDown();
      this.mStopDelayLabel = new System.Windows.Forms.Label();
      this.mTimeNowPanel = new System.Windows.Forms.Panel();
      this.mStopDelayPanel = new System.Windows.Forms.Panel();
      this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.mButtonOpenFile = new System.Windows.Forms.Button();
      this.mStopNameLabel = new System.Windows.Forms.Label();
      this.mStopNamePanel = new System.Windows.Forms.Panel();
      this.mButtonOpenWeb = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.mStopDelayChooser)).BeginInit();
      this.mTimeNowPanel.SuspendLayout();
      this.mStopDelayPanel.SuspendLayout();
      this.mStopNamePanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // mStopTimesPanel
      // 
      this.mStopTimesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
      this.mStopTimesPanel.ColumnCount = 2;
      this.mStopTimesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.mStopTimesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.mStopTimesPanel.Location = new System.Drawing.Point(0, 197);
      this.mStopTimesPanel.Name = "mStopTimesPanel";
      this.mStopTimesPanel.RowCount = 1;
      this.mStopTimesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.mStopTimesPanel.Size = new System.Drawing.Size(172, 150);
      this.mStopTimesPanel.TabIndex = 0;
      // 
      // mBusPanel
      // 
      this.mBusPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mBusPanel.Location = new System.Drawing.Point(0, 74);
      this.mBusPanel.Name = "mBusPanel";
      this.mBusPanel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.mBusPanel.Size = new System.Drawing.Size(172, 50);
      this.mBusPanel.TabIndex = 2;
      this.mBusPanel.TabStop = true;
      // 
      // mTimeNowLabel
      // 
      this.mTimeNowLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mTimeNowLabel.AutoSize = true;
      this.mTimeNowLabel.Location = new System.Drawing.Point(3, 6);
      this.mTimeNowLabel.Name = "mTimeNowLabel";
      this.mTimeNowLabel.Size = new System.Drawing.Size(49, 17);
      this.mTimeNowLabel.TabIndex = 0;
      this.mTimeNowLabel.Text = "Time is";
      // 
      // mStopDelayChooser
      // 
      this.mStopDelayChooser.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mStopDelayChooser.Location = new System.Drawing.Point(124, 4);
      this.mStopDelayChooser.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.mStopDelayChooser.Name = "mStopDelayChooser";
      this.mStopDelayChooser.Size = new System.Drawing.Size(43, 24);
      this.mStopDelayChooser.TabIndex = 0;
      this.mStopDelayChooser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.mStopDelayChooser.ValueChanged += new System.EventHandler(this.mStopDelayChooser_ValueChanged);
      // 
      // mStopDelayLabel
      // 
      this.mStopDelayLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mStopDelayLabel.AutoSize = true;
      this.mStopDelayLabel.Location = new System.Drawing.Point(3, 6);
      this.mStopDelayLabel.Name = "mStopDelayLabel";
      this.mStopDelayLabel.Size = new System.Drawing.Size(109, 17);
      this.mStopDelayLabel.TabIndex = 2;
      this.mStopDelayLabel.Text = "Stop delay (min)";
      // 
      // mTimeNowPanel
      // 
      this.mTimeNowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mTimeNowPanel.BackColor = System.Drawing.SystemColors.ControlDark;
      this.mTimeNowPanel.Controls.Add(this.mTimeNowLabel);
      this.mTimeNowPanel.Location = new System.Drawing.Point(0, 161);
      this.mTimeNowPanel.Name = "mTimeNowPanel";
      this.mTimeNowPanel.Size = new System.Drawing.Size(172, 30);
      this.mTimeNowPanel.TabIndex = 3;
      // 
      // mStopDelayPanel
      // 
      this.mStopDelayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mStopDelayPanel.Controls.Add(this.mStopDelayLabel);
      this.mStopDelayPanel.Controls.Add(this.mStopDelayChooser);
      this.mStopDelayPanel.Location = new System.Drawing.Point(0, 126);
      this.mStopDelayPanel.Name = "mStopDelayPanel";
      this.mStopDelayPanel.Size = new System.Drawing.Size(172, 32);
      this.mStopDelayPanel.TabIndex = 0;
      // 
      // mOpenFileDialog
      // 
      this.mOpenFileDialog.Filter = "HTML files (*.htm, *.html)|*.htm;*.html";
      this.mOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.mOpenFileDialog_FileOk);
      // 
      // mButtonOpenFile
      // 
      this.mButtonOpenFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mButtonOpenFile.Location = new System.Drawing.Point(4, 6);
      this.mButtonOpenFile.Name = "mButtonOpenFile";
      this.mButtonOpenFile.Size = new System.Drawing.Size(49, 25);
      this.mButtonOpenFile.TabIndex = 9;
      this.mButtonOpenFile.Text = "File...";
      this.mButtonOpenFile.UseVisualStyleBackColor = true;
      this.mButtonOpenFile.Click += new System.EventHandler(this.mButtonOpenFile_Click);
      // 
      // mStopNameLabel
      // 
      this.mStopNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mStopNameLabel.AutoSize = true;
      this.mStopNameLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
      this.mStopNameLabel.Location = new System.Drawing.Point(3, 6);
      this.mStopNameLabel.Margin = new System.Windows.Forms.Padding(0);
      this.mStopNameLabel.Name = "mStopNameLabel";
      this.mStopNameLabel.Size = new System.Drawing.Size(85, 17);
      this.mStopNameLabel.TabIndex = 0;
      this.mStopNameLabel.Text = "Stop Name";
      this.mStopNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // mStopNamePanel
      // 
      this.mStopNamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mStopNamePanel.BackColor = System.Drawing.SystemColors.ControlDark;
      this.mStopNamePanel.Controls.Add(this.mStopNameLabel);
      this.mStopNamePanel.Location = new System.Drawing.Point(0, 39);
      this.mStopNamePanel.Name = "mStopNamePanel";
      this.mStopNamePanel.Size = new System.Drawing.Size(172, 30);
      this.mStopNamePanel.TabIndex = 10;
      // 
      // mButtonOpenWeb
      // 
      this.mButtonOpenWeb.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.mButtonOpenWeb.Location = new System.Drawing.Point(111, 6);
      this.mButtonOpenWeb.Name = "mButtonOpenWeb";
      this.mButtonOpenWeb.Size = new System.Drawing.Size(57, 25);
      this.mButtonOpenWeb.TabIndex = 11;
      this.mButtonOpenWeb.Text = "Web...";
      this.mButtonOpenWeb.UseVisualStyleBackColor = true;
      this.mButtonOpenWeb.Click += new System.EventHandler(this.mButtonOpenWeb_Click);
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(172, 347);
      this.Controls.Add(this.mButtonOpenWeb);
      this.Controls.Add(this.mStopNamePanel);
      this.Controls.Add(this.mButtonOpenFile);
      this.Controls.Add(this.mStopDelayPanel);
      this.Controls.Add(this.mTimeNowPanel);
      this.Controls.Add(this.mBusPanel);
      this.Controls.Add(this.mStopTimesPanel);
      this.Font = new System.Drawing.Font("Tahoma", 10F);
      this.MaximizeBox = false;
      this.Name = "MainWindow";
      this.ShowIcon = false;
      this.Text = "Stop Watch";
      this.Load += new System.EventHandler(this.MainWindow_Load);
      ((System.ComponentModel.ISupportInitialize)(this.mStopDelayChooser)).EndInit();
      this.mTimeNowPanel.ResumeLayout(false);
      this.mTimeNowPanel.PerformLayout();
      this.mStopDelayPanel.ResumeLayout(false);
      this.mStopDelayPanel.PerformLayout();
      this.mStopNamePanel.ResumeLayout(false);
      this.mStopNamePanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel mStopTimesPanel;
    private FlowLayoutPanel mBusPanel;
    private Label mTimeNowLabel;
    private NumericUpDown mStopDelayChooser;
    private Label mStopDelayLabel;
    private Panel mTimeNowPanel;
    private Panel mStopDelayPanel;
    private OpenFileDialog mOpenFileDialog;
    private Button mButtonOpenFile;
    private Label mStopNameLabel;
    private Panel mStopNamePanel;
    private Button mButtonOpenWeb;
  }
}