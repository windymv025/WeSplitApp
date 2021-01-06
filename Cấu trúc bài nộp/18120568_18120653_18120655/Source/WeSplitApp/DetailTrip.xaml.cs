using System;
using System.Collections.Generic;
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
using WeSplitApp.ViewModels;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for DetailTrip.xaml
    /// </summary>
    public partial class DetailTrip : Window
    {
        DetailTripViewModel detailTrip;
        public DetailTrip(ChuyenDi chuyenDi)
        {
            InitializeComponent();
            detailTrip = new DetailTripViewModel(chuyenDi);
            this.DataContext = detailTrip.ChuyenDi;
            listViewKhoanChi.ItemsSource = detailTrip.KhoanChiTieus;
            listViewMembers.ItemsSource = detailTrip.ThanhVienKhoanThus;
            listViewTripImages.ItemsSource = detailTrip.HinhAnhChuyenDis;
        }

        private void _update_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateTrip updateTrip = new UpdateTrip(detailTrip.ChuyenDi);
            updateTrip.Show();
            this.Close();
        }

        private void _split_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SplitMoney screen = new SplitMoney(detailTrip.ChuyenDi);
            screen.Show();
            this.Close();
        }

        private void _piechart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chartsMoney screen = new chartsMoney(detailTrip.ChuyenDi);
            screen.Show();
            this.Close();
        }

        private void listViewKhoanChi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
