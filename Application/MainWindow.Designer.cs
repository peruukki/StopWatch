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
      this.mStopTimesPanel.Location = new System.Drawing.Point(0, 80);
      this.mStopTimesPanel.Name = "mStopTimesPanel";
      this.mStopTimesPanel.RowCount = 1;
      this.mStopTimesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.mStopTimesPanel.Size = new System.Drawing.Size(150, 150);
      this.mStopTimesPanel.TabIndex = 0;
      // 
      // mBusPanel
      // 
      this.mBusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mBusPanel.Location = new System.Drawing.Point(0, 0);
      this.mBusPanel.Name = "mBusPanel";
      this.mBusPanel.Size = new System.Drawing.Size(150, 50);
      this.mBusPanel.TabIndex = 0;
      // 
      // mTimeNowLabel
      // 
      this.mTimeNowLabel.AutoSize = true;
      this.mTimeNowLabel.Location = new System.Drawing.Point(3, 56);
      this.mTimeNowLabel.Name = "mTimeNowLabel";
      this.mTimeNowLabel.Size = new System.Drawing.Size(49, 17);
      this.mTimeNowLabel.TabIndex = 0;
      this.mTimeNowLabel.Text = "Time is";
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(150, 227);
      this.Controls.Add(this.mTimeNowLabel);
      this.Controls.Add(this.mBusPanel);
      this.Controls.Add(this.mStopTimesPanel);
      this.Font = new System.Drawing.Font("Tahoma", 10F);
      this.Name = "MainWindow";
      this.ShowIcon = false;
      this.Text = "Stop Watch";
      this.Load += new System.EventHandler(this.MainWindow_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private TableLayoutPanel mStopTimesPanel;
    private FlowLayoutPanel mBusPanel;
    private Label mTimeNowLabel;
  }
}