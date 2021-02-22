using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for AddTrip.xaml
    /// </summary>
    public partial class AddTrip : Window
    {
        AddTripViewModel addTripViewModel;
        bool hasLeader = false;

        public AddTrip()
        {
            InitializeComponent();
            addTripViewModel = new AddTripViewModel();
        }

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bool canSave = true;
            StringBuilder notifical = new StringBuilder();
            DateTime startDateTrip;
            DateTime finishDateTrip;
            if (tripName.Text.Trim().ToString() == "")
            {
                canSave = false;
                notifical.Append("The name");
            }
            if (imgTrip.ImageSource == null)
            { 
                canSave = false;
                notifical.Append(", The image");
            }
            if (Introduce.Text.Trim() == "") 
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
            if(listMembers.Items.Count==0)
            {
                canSave = false;
                notifical.Append(", any member");
            }

            if(startDate.SelectedDate!=null && finishDate.SelectedDate != null)
            {
                startDateTrip = startDate.SelectedDate.GetValueOrDefault();
                finishDateTrip = finishDate.SelectedDate.GetValueOrDefault();

                if (startDateTrip.CompareTo(finishDateTrip) == 1)
                {
                    MessageBox.Show("Start date must be less than finish date!!!", "Wrong Start or Finsh date", MessageBoxButton.OK, MessageBoxImage.Error);
                    canSave = false;
                }
                
            }

            //Check leader
            if(!hasLeader)
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

                    string imgtripName = System.IO.Path.GetFileName(imgTrip.ImageSource.ToString());
                    var imgAvatarPath = ((BitmapImage)imgTrip.ImageSource).UriSource.ToString().Remove(0, 8);
                    var newImageAvatarPath = String.Format(imagesFolder + "\\" + imgtripName);
                    File.Copy(imgAvatarPath, newImageAvatarPath, true);

                    addTripViewModel.ChuyenDi.HinhAnh = $"Images/{imgtripName}";
                    addTripViewModel.ChuyenDi.TenChuyenDi = tripName.Text.Trim();
                    addTripViewModel.ChuyenDi.LoTrinh = null;
                    addTripViewModel.ChuyenDi.GioiThieu = Introduce.Text.Trim();
                    addTripViewModel.ChuyenDi.NgayBatDau = startDate.SelectedDate.GetValueOrDefault();
                    addTripViewModel.ChuyenDi.NgayKetThuc = finishDate.SelectedDate.GetValueOrDefault();

                    addTripViewModel.InsertTrip();

                    MainWindow m = new MainWindow();
                    m.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show($"You did not enter {notifical} of the trip!!!", "Enter missing trip information.", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imgsource = new BitmapImage(new Uri(img[0].ToString()));
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

                addTripViewModel.KhoanChiTieus.Add(new KhoanChiTieu()
                {
                    TenKhoanChi = expenses,
                    SoTien = soTien
                });

                Expenditures.Text = "";
                Prices.Text = "";

                listExpenditures.ItemsSource = addTripViewModel.KhoanChiTieus;
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
                if(typeMember.SelectedIndex==0)
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
            }
            else
            {
                typeMember.SelectedIndex = 1;
                typeMember.IsEnabled = false;
                type = "Member";
            }

            if (name != "")
            {
                if (money=="")
                {
                    price = 0;
                }
                else
                {
                    price = decimal.Parse(money);
                }

                ThanhVienKhoanThu thanhVienKhoanThu = new ThanhVienKhoanThu(name, type, price);
                addTripViewModel.ThanhVienKhoanThus.Add(thanhVienKhoanThu);
                listMembers.ItemsSource = addTripViewModel.ThanhVienKhoanThus;
                
                memberName.Text = "";
                moneyPaid.Text = "";
            }
            else
                MessageBox.Show("You don't enter the name of member", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Prices_TextChanged(object sender, TextChangedEventArgs e)
        {
            Prices.Text = Regex.Replace(Prices.Text, "[^0-9]+", "");
        }

        private void moneyPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            moneyPaid.Text = Regex.Replace(moneyPaid.Text, "[^0-9]+", "");
        }

        private void listMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listExpenditures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
