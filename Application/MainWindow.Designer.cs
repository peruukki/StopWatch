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
      this.SuspendLayout();
      // 
      // MainWindow
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoSize = true;
      this.ClientSize = new Size(10, 10);
      this.Name = "MainWindow";
      this.ShowIcon = false;
      this.Font = new Font("Tahoma", 10);
      this.Text = "Stop Watch";
      this.Load += new System.EventHandler(this.MainWindow_Load);

      this.mLayout = new TableLayoutPanel();
      this.mLayout.AutoSize = true;
      this.Controls.Add(this.mLayout);

      this.ResumeLayout(false);
    }

    #endregion
  }
}