using LiveCharts;
using LiveCharts.Wpf;
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
    /// Interaction logic for chartsMoney.xaml
    /// </summary>
    public partial class chartsMoney : Window
    {
        DetailTripViewModel detailTrip;
        public chartsMoney(ChuyenDi chuyenDi)
        {
            InitializeComponent();
            detailTrip = new DetailTripViewModel(chuyenDi);
        }
        public Func<ChartPoint, string> PointLabel => point => $"{point.Y} ({point.Participation:P1})";
        public SeriesCollection Data1 { get; set; }
        public SeriesCollection Data2 { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TripName.Text = detailTrip.ChuyenDi.TenChuyenDi;
            Data1 = new SeriesCollection();
            Data2 = new SeriesCollection();
            var list1 = detailTrip.ThanhVienKhoanThus.ToList();

            for (int i = 0; i < list1.Count(); i++)
            {
                string tenthanhvien = list1[i].TenThanhVien.ToString();
                float tienthu = float.Parse(list1[i].SoTienThu.ToString());
                var t = new PieSeries()
                {
                    Values = new ChartValues<float> { tienthu },
                    Title = tenthanhvien,
                    DataLabels = true
                };
                Data1.Add(t);
            }

            var list2 = detailTrip.KhoanChiTieus.ToList();

            for (int i = 0; i < list2.Count(); i++)
            {
                string tenkhoangchi = list2[i].TenKhoanChi.ToString();
                float tienthu = float.Parse(list2[i].SoTien.ToString());
                var t = new PieSeries()
                {
                    Values = new ChartValues<float> { tienthu },
                    Title = tenkhoangchi,
                    DataLabels = true
                };
                Data2.Add(t);
            }

            this.DataContext = this;
        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = chartPoint.ChartView as PieChart;

            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }

            var selectedSeries = chartPoint.SeriesView as PieSeries;
            selectedSeries.PushOut = 10;
        }

        private void imgBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DetailTrip screen = new DetailTrip(detailTrip.ChuyenDi);
            screen.Show();
            this.Close();
        }
    }
}
