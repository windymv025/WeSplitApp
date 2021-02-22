using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeSplitApp.Model;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        System.Timers.Timer timer;
        private int count = 0;
        private bool isClickSkip;
        private const int target = 10;
        private const string FILE_NAME_LOG = "StartUpSplash.log";

        private Random rgn = new Random();

        public SplashScreen()
        {
            InitializeComponent();
            isClickSkip = false;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;

            if (count == target)
            {
                timer.Stop();

                Dispatcher.Invoke(() =>
                {
                    if (!isClickSkip)
                    {
                        var screen = new MainWindow();
                        screen.Show();

                        this.Close();
                    }
                });
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChuyenDi f = new ChuyenDi();
            using (DBWeSplitEntities db = new DBWeSplitEntities())
            {
                var list = db.ChuyenDis.ToList();

                foreach (var item in list)
                {
                    item.HinhAnh = System.IO.Path.GetFullPath(item.HinhAnh);
                }

                int index = rgn.Next(list.Count);
                //foodImage.ImageSource = new BitmapImage(new Uri(list[index].FoodImage, UriKind.Relative));
                //Title.Text = list[index].NameFood;
                //Descripttion.Text = list[index].DishDescription;
                f = list[index];
            }
            this.DataContext = f;
        }
        private void CheckSplashScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            if (File.Exists(FILE_NAME_LOG))
                File.Delete(FILE_NAME_LOG);
            using (StreamWriter f = new StreamWriter(FILE_NAME_LOG))
            {
                f.WriteLine(true);
                f.Close();
            }
        }

        private void CheckSplashScreen_Checked(object sender, RoutedEventArgs e)
        {
            if (File.Exists(FILE_NAME_LOG))
                File.Delete(FILE_NAME_LOG);
            using (StreamWriter f = new StreamWriter(FILE_NAME_LOG))
            {
                f.WriteLine(false);
                f.Close();
            }
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            isClickSkip = true;
            timer.Stop();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }
    }
}
