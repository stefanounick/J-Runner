using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace JRunner
{
    static class Program
    {
        static bool createdNew = true;
        static Mutex mutex = new Mutex(true, "J-Runner", out createdNew);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomain_UnhandledException;

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                Thread.Sleep(1000);
                Process current = Process.GetCurrentProcess();
                bool found = false;
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        found = true;
                        SetForegroundWindow(process.MainWindowHandle);
                        break;
                    }
                }
                if (!found) Process.Start(Application.ExecutablePath);
            }
        }
        private static void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                mutex.ReleaseMutex();
            }
            catch (Exception) { }
            Application.ExitThread();
            Application.Exit();
            Environment.Exit(0);
        }

        private static void OnCurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //MessageBox.Show(e.ExceptionObject.ToString());
            File.AppendAllText(Path.Combine(variables.pathforit, "Error.txt"), e.ExceptionObject.ToString() + Environment.NewLine);
        }
    }
}
