using System;
using System.Windows.Forms;

namespace InwersjaTomograficzna.Core.Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry PointF for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
