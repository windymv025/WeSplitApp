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
    /// Interaction logic for SplitMoney.xaml
    /// </summary>
    public partial class SplitMoney : Window
    {
        class KetQuaChiaTien
        {
            public string TenThanhVien { get; set; }
            public decimal SoTienThu { get; set; }
            public string Result { get; set; }
            public KetQuaChiaTien(string a,decimal b, string c)
            {
                TenThanhVien = a;
                SoTienThu = b;
                Result = c;
            }
        }
        DetailTripViewModel detailTrip;
        public SplitMoney(ChuyenDi chuyenDi)
        {
            InitializeComponent();
            detailTrip = new DetailTripViewModel(chuyenDi);
            this.DataContext = detailTrip.ChuyenDi;
            listViewKhoanChi.ItemsSource = detailTrip.KhoanChiTieus;

            decimal sum = 0;
            decimal numberMember = detailTrip.ThanhVienKhoanThus.Count;
            foreach (var s in detailTrip.KhoanChiTieus)
            {
                sum += s.SoTien;
            }
            decimal averagePrice = sum / numberMember;

            List<string> splitResult = new List<string>();

            string nameLeader = "";

            foreach (var s in detailTrip.ThanhVienKhoanThus)
            {
                if(s.ChucVu.Equals("Leader"))
                {
                    nameLeader = s.TenThanhVien;
                    break;
                }    
            }

            foreach (var s in detailTrip.ThanhVienKhoanThus)
            {
                string temp = "";
                if(s.ChucVu.Equals("Leader"))
                {
                    if (averagePrice - s.SoTienThu > 0)
                    {
                        temp = $"Đóng thêm số tiền là: {averagePrice - s.SoTienThu}.";
                    }
                    else if (averagePrice - s.SoTienThu == 0)
                    {
                        temp = "Đã đóng đủ tiền.";
                    }
                    else
                    {
                        temp = $"{nameLeader} (Leader) lấy lại số tiền là: {s.SoTienThu - averagePrice}.";
                    }
                }    
                else
                {
                    if (averagePrice - s.SoTienThu > 0)
                    {
                        temp = $"Đóng thêm cho {nameLeader} (Leader) số tiền là: {averagePrice - s.SoTienThu}.";
                    }
                    else if(averagePrice - s.SoTienThu == 0)
                    {
                        temp = "Đã đóng đủ tiền.";
                    }
                    else
                    {
                        temp = $"{nameLeader} (Leader) trả lại số tiền là: {s.SoTienThu - averagePrice}.";
                    }
                }
                splitResult.Add(temp);
            }

            

            List<KetQuaChiaTien> kq = new List<KetQuaChiaTien>();
            for(var i = 0; i < detailTrip.ThanhVienKhoanThus.Count; i++)
            {
                KetQuaChiaTien rs = new KetQuaChiaTien(detailTrip.ThanhVienKhoanThus[i].TenThanhVien, detailTrip.ThanhVienKhoanThus[i].SoTienThu, splitResult[i]);
                kq.Add(rs);
            }    
            listViewChiaTien.ItemsSource = kq;
        }

        private void listViewKhoanChi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DetailTrip screen = new DetailTrip(detailTrip.ChuyenDi);
            screen.Show();
            this.Close();
        }
    }
}
