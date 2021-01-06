using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string FILE_NAME_LOG = "StartUpSplash.log";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool isStartWithSplash;
            if (File.Exists(FILE_NAME_LOG))
            {
                using (StreamReader f = new StreamReader(FILE_NAME_LOG))
                {
                    string str = f.ReadLine();
                    if (str == null)
                    {
                        isStartWithSplash = true;
                    }
                    else
                    {
                        isStartWithSplash = bool.Parse(str);
                    }
                    f.Close();
                }
            }
            else
            {
                if (File.Exists(FILE_NAME_LOG))
                    File.Delete(FILE_NAME_LOG);
                isStartWithSplash = true;
                using (StreamWriter f = new StreamWriter(FILE_NAME_LOG))
                {
                    f.WriteLine(isStartWithSplash);
                    f.Close();
                }
            }

            if (isStartWithSplash)
            {
                SplashScreen splash = new SplashScreen();
                splash.Show();
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
            }
        }
    }
}
