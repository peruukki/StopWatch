using System;
using System.IO;
using System.Windows.Forms;

namespace StopWatch
{
	class StopWatch
	{
		[STAThread]
		static void Main(string[] args)
		{
      Application.Run(new MainWindow());
		}
	}
}
