using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UpdateTrip.xaml
    /// </summary>
    public partial class UpdateTrip : Window
    {
        UpdateTripViewModel updateTrip;
        bool hasLeader;
        public UpdateTrip(ChuyenDi chuyenDi)
        {
            InitializeComponent();
            updateTrip = new UpdateTripViewModel(chuyenDi);
            this.DataContext = updateTrip.ChuyenDi;
            listViewTripImages.ItemsSource = updateTrip.HinhAnhChuyenDis;
            listViewKhoanChi.ItemsSource = updateTrip.KhoanChiTieus;
            listMembers.ItemsSource = updateTrip.ThanhVienKhoanThus;
            hasLeader = true;
        }

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string tenChuyenDi = tripName.Text.Trim();
            string gioiThieu = Introduce.Text.Trim();
            string loTrinh = Routes.Text.Trim();
            DateTime ngayBatDau = startDate.SelectedDate.GetValueOrDefault();
            DateTime ngayKetThuc = startDate.SelectedDate.GetValueOrDefault();
            string pathHinhAnh = System.IO.Path.GetFullPath(imgTrip.ImageSource.ToString().Remove(0,8));

            bool canSave = true;
            StringBuilder notifical = new StringBuilder();
            
            if (tenChuyenDi == "")
            {
                canSave = false;
                notifical.Append("The name");
            }
            if (imgTrip.ImageSource == null)
            {
                canSave = false;
                notifical.Append(", The image");
            }
            if (gioiThieu == "")
            {
                canSave = false;
                notifical.Append(", The introduce");
            }
            if (startDate.SelectedDate == null)
            {
                canSave = false;
                notifical.Append(", The start date");
            }
            if (finishDate.SelectedDate == null)
            {
                canSave = false;
                notifical.Append(", The finish date");
            }
            if (listMembers.Items.Count == 0)
            {
                canSave = false;
                notifical.Append(", any member");
            }

            if (startDate.SelectedDate != null && finishDate.SelectedDate != null)
            {
                if (ngayBatDau.CompareTo(ngayKetThuc) == 1)
                {
                    MessageBox.Show("Start date must be less than finish date!!!", "Wrong Start or Finsh date", MessageBoxButton.OK, MessageBoxImage.Error);
                    canSave = false;
                }

            }

            //Check leader
            if (!hasLeader)
            {
                MessageBox.Show("The trip doesn't have a Leader. Please Enter the Trip Leader!!!", "Doesn't have a Trip Leader", MessageBoxButton.OK, MessageBoxImage.Error);
                canSave = false;
            }

            if (canSave)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                    var imagesFolder = $"{currentFolder}Images";
                    if (pathHinhAnh != System.IO.Path.GetFullPath(updateTrip.ChuyenDi.HinhAnh))
                    {
                        string imgtripName = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                        var imgAvatarPath = ((BitmapImage)imgTrip.ImageSource).UriSource.ToString().Remove(0, 8);
                        var info = new FileInfo(pathHinhAnh);
                        var newNameImage = $"{Guid.NewGuid()}{info.Extension}";
                        var newImageAvatarPath = String.Format(imagesFolder + "\\" + newNameImage);
                        
                        File.Copy(imgAvatarPath, newImageAvatarPath, true);

                        updateTrip.ChuyenDi.HinhAnh = $"Images/{newNameImage}";
                    }
                    else
                    {
                        string imgtripName = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                        updateTrip.ChuyenDi.HinhAnh = $"Images/{imgtripName}";
                    }
                    if (tenChuyenDi != updateTrip.ChuyenDi.TenChuyenDi)
                    {
                        updateTrip.ChuyenDi.TenChuyenDi = tripName.Text.Trim();
                    }
                    updateTrip.ChuyenDi.LoTrinh = loTrinh;
                    updateTrip.ChuyenDi.GioiThieu = gioiThieu;
                    updateTrip.ChuyenDi.NgayBatDau = ngayBatDau;
                    updateTrip.ChuyenDi.NgayKetThuc = ngayKetThuc;

                    updateTrip.updateTrip();

                    DetailTrip m = new DetailTrip(updateTrip.ChuyenDi);
                    m.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show($"You did not enter {notifical} of the trip!!!", "Enter missing trip information.", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DetailTrip detailTrip = new DetailTrip(updateTrip.ChuyenDi);
            detailTrip.Show();
            this.Close();
        }

        private void Prices_TextChanged(object sender, TextChangedEventArgs e)
        {
            Prices.Text = Regex.Replace(Prices.Text, "[^0-9]+", "");
        }

        private void moneyPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            moneyPaid.Text = Regex.Replace(moneyPaid.Text, "[^0-9]+", "");
        }


        private void ChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == true)
            {
                var img = open.FileNames;
                SaveImage.tempUriImage = img[0].ToString();
                ImageSource imgsource = new BitmapImage(new Uri(SaveImage.tempUriImage));
                imgTrip.ImageSource = imgsource;
            }
        }

        private void imgAddPays_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Expenditures.Text.Trim() == "")
            {
                MessageBox.Show("You don't enter the name of Expenditures", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string expenses = Expenditures.Text.Trim();
                string prices = Prices.Text.Trim();
                decimal soTien;
                if (prices == "")
                {
                    soTien = 0;
                }
                else
                {
                    soTien = decimal.Parse(prices);
                }

                KhoanChiTieu khoanChiTieu = new KhoanChiTieu()
                {
                    TenKhoanChi = expenses,
                    SoTien = soTien,
                    IDChuyenDi = updateTrip.ChuyenDi.IDChuyenDi
                };

                Expenditures.Text = "";
                Prices.Text = "";

                updateTrip.addKhoanChiTieu(khoanChiTieu);

                listViewKhoanChi.ItemsSource = updateTrip.KhoanChiTieus;
            }
        }


        private void buttonDeleteExpenses_Click(object sender, RoutedEventArgs e)
        {
            var index = listViewKhoanChi.SelectedIndex;
            var choose = MessageBox.Show($"Do you want to remove the Expense: '{updateTrip.KhoanChiTieus[index].TenKhoanChi}'?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(choose == MessageBoxResult.Yes)
            {
                updateTrip.xoaKhoanChi(index);
            }
        }

        private void imgAddMember_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string name = memberName.Text.Trim();
            string money = moneyPaid.Text.Trim();
            decimal price;
            string type;
            if (!hasLeader)
            {
                if (typeMember.SelectedIndex == 0)
                {
                    type = "Leader";
                    hasLeader = true;
                    typeMember.SelectedIndex = 1;
                    typeMember.IsEnabled = false;
                }
                else
                {
                    type = "Member";
                }
                typeMember.IsEnabled = true;
            }
            else
            {
                typeMember.SelectedIndex = 1;
                typeMember.IsEnabled = false;
                type = "Member";
            }

            if (name != "")
            {
                if (money == "")
                {
                    price = 0;
                }
                else
                {
                    price = decimal.Parse(money);
                }

                ThanhVienKhoanThu thanhVienKhoanThu = new ThanhVienKhoanThu(name, type, price);
                updateTrip.addThanhVienKhoanThu(thanhVienKhoanThu);
                listMembers.ItemsSource = updateTrip.ThanhVienKhoanThus;

                memberName.Text = "";
                moneyPaid.Text = "";
            }
            else
                MessageBox.Show("You don't enter the name of member", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void buttonDeleteMembers_Click(object sender, RoutedEventArgs e)
        {
            var index = listMembers.SelectedIndex;
            var choose = MessageBox.Show($"Do you want to remove the Members: '{updateTrip.ThanhVienKhoanThus[index].TenThanhVien}': {updateTrip.ThanhVienKhoanThus[index].SoTienThu} ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (choose == MessageBoxResult.Yes)
            {
                updateTrip.xoaThanhVienKhoanThu(index);
            }
        }

        private void buttonDeleteImages_Click(object sender, RoutedEventArgs e)
        {
            var index = listViewTripImages.SelectedIndex;
            updateTrip.HinhAnhChuyenDis.RemoveAt(index);
        }

        private void Imgsss_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (open.ShowDialog() == true)
            {
                var imgs = open.FileNames;
                foreach(var item in imgs)
                {
                    updateTrip.SaveImage.listFiles.Add(item.ToString());
                    updateTrip.HinhAnhChuyenDis.Add(new HinhAnhChuyenDi()
                    {
                        HinhAnh = System.IO.Path.GetFullPath(item),
                        IDChuyenDi = updateTrip.ChuyenDi.IDChuyenDi
                    });
                }
            }
        }

        private void listMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
