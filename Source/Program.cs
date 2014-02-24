using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace woanware
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.OnUnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Program.OnThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        #region Unhandled Exception Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;

            Misc.WriteToEventLog(Application.ProductName, "An unhandled exception has occurred: " + exception.ToString(), EventLogEntryType.Error);
            UserInterface.DisplayErrorMessageBox("An unhandled exception has occurred, check the event log for details");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = (Exception)e.Exception;

            Misc.WriteToEventLog(Application.ProductName, "An unhandled exception has occurred: " + Environment.NewLine + Environment.NewLine + exception.ToString(), EventLogEntryType.Error);
            UserInterface.DisplayErrorMessageBox("An unhandled exception has occurred, check the event log for details");
        }
        #endregion
    }
}
