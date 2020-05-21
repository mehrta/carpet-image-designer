using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CrossPlanGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormMain mainForm = new FormMain();
            mainForm.ProgramArguments = args;

            Application.Run(mainForm);
        }
    }
}
