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
      this.mDelayLabelText = new System.Windows.Forms.Label();
      this.mTimeNowPanel = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.mStopDelayChooser)).BeginInit();
      this.mTimeNowPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // mStopTimesPanel
      // 
      this.mStopTimesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.mStopTimesPanel.AutoSize = true;
      this.mStopTimesPanel.ColumnCount = 2;
      this.mStopTimesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.mStopTimesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.mStopTimesPanel.Location = new System.Drawing.Point(0, 120);
      this.mStopTimesPanel.Name = "mStopTimesPanel";
      this.mStopTimesPanel.RowCount = 1;
      this.mStopTimesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.mStopTimesPanel.Size = new System.Drawing.Size(160, 150);
      this.mStopTimesPanel.TabIndex = 0;
      // 
      // mBusPanel
      // 
      this.mBusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mBusPanel.Location = new System.Drawing.Point(0, 0);
      this.mBusPanel.Name = "mBusPanel";
      this.mBusPanel.Size = new System.Drawing.Size(160, 50);
      this.mBusPanel.TabIndex = 0;
      // 
      // mTimeNowLabel
      // 
      this.mTimeNowLabel.AutoSize = true;
      this.mTimeNowLabel.Location = new System.Drawing.Point(3, 6);
      this.mTimeNowLabel.Name = "mTimeNowLabel";
      this.mTimeNowLabel.Size = new System.Drawing.Size(49, 17);
      this.mTimeNowLabel.TabIndex = 0;
      this.mTimeNowLabel.Text = "Time is";
      // 
      // mStopDelayChooser
      // 
      this.mStopDelayChooser.Location = new System.Drawing.Point(113, 54);
      this.mStopDelayChooser.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.mStopDelayChooser.Name = "mStopDelayChooser";
      this.mStopDelayChooser.Size = new System.Drawing.Size(43, 24);
      this.mStopDelayChooser.TabIndex = 1;
      this.mStopDelayChooser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.mStopDelayChooser.ValueChanged += new System.EventHandler(this.mStopDelayChooser_ValueChanged);
      // 
      // mDelayLabelText
      // 
      this.mDelayLabelText.AutoSize = true;
      this.mDelayLabelText.Location = new System.Drawing.Point(3, 56);
      this.mDelayLabelText.Name = "mDelayLabelText";
      this.mDelayLabelText.Size = new System.Drawing.Size(109, 17);
      this.mDelayLabelText.TabIndex = 2;
      this.mDelayLabelText.Text = "Stop delay (min)";
      // 
      // mTimeNowPanel
      // 
      this.mTimeNowPanel.BackColor = System.Drawing.SystemColors.ControlDark;
      this.mTimeNowPanel.Controls.Add(this.mTimeNowLabel);
      this.mTimeNowPanel.Location = new System.Drawing.Point(0, 83);
      this.mTimeNowPanel.Name = "mTimeNowPanel";
      this.mTimeNowPanel.Size = new System.Drawing.Size(160, 30);
      this.mTimeNowPanel.TabIndex = 3;
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(160, 267);
      this.Controls.Add(this.mTimeNowPanel);
      this.Controls.Add(this.mDelayLabelText);
      this.Controls.Add(this.mStopDelayChooser);
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
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private TableLayoutPanel mStopTimesPanel;
    private FlowLayoutPanel mBusPanel;
    private Label mTimeNowLabel;
    private NumericUpDown mStopDelayChooser;
    private Label mDelayLabelText;
    private Panel mTimeNowPanel;
  }
}