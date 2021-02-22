using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplitApp.Model;
using WeSplitApp.ViewModels;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int NUMBER_ITEMS_HOME = 2;
        const int NUMBER_ITEMS_TRIP = 4;

        HomeViewModel finishViewModel;
        HomeViewModel comingViewModel;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            finishViewModel = new HomeViewModel();
            comingViewModel = new HomeViewModel();

            finishViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiFinish();
            comingViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiComing();

            finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_HOME, finishViewModel.ChuyenDis.Count);
            comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_HOME, comingViewModel.ChuyenDis.Count);

            loadDataFinishHome(1);
            loadDataComingHome(1);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //Set tooltip
            if (Tg_btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_add.Visibility = Visibility.Collapsed;
                tt_finishedtrip.Visibility = Visibility.Collapsed;
                tt_contact.Visibility = Visibility.Collapsed;
                tt_comingtrip.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_add.Visibility = Visibility.Visible;
                tt_finishedtrip.Visibility = Visibility.Visible;
                tt_contact.Visibility = Visibility.Visible;
                tt_comingtrip.Visibility = Visibility.Visible;
            }
        }

        private void Tg_btn_Unchecked(object sender, RoutedEventArgs e)
        {
            bg.Opacity = 1;
            contact_screen.Opacity = 1;
        }

        private void Tg_btn_Checked(object sender, RoutedEventArgs e)
        {
            bg.Opacity = 0.3;
            contact_screen.Opacity = 0.3;
        }

        private void bg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_btn.IsChecked = false;
        }

        private void img_contact_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Collapsed;
            bg_coming.Visibility = Visibility.Collapsed;
            bg_finished.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Visible;
            Tg_btn.IsChecked = false;
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Visible;
            bg_coming.Visibility = Visibility.Collapsed;
            bg_finished.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;

            finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_HOME, finishViewModel.ChuyenDis.Count);
            comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_HOME, comingViewModel.ChuyenDis.Count);
            loadDataFinishHome(1);
            loadDataComingHome(1);
        }


        private void img_addtrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           AddTrip addTrip = new AddTrip();
            //UpdateTrip updateTrip = new UpdateTrip();
            //updateTrip.Show();
           addTrip.Show();
           this.Close();
        }

        private void icon_search_coming_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(coming_search.Visibility == Visibility.Visible)
            {
                coming_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                coming_search.Visibility = Visibility.Visible;
            }
        }

        private void icon_search_finished_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (finished_search.Visibility == Visibility.Visible)
            {
                finished_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                finished_search.Visibility = Visibility.Visible;
            }
        }

        private void Image_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            bg_coming.Visibility = Visibility.Visible;
            bg_finished.Visibility = Visibility.Collapsed;
            bg_home.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;

            comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, comingViewModel.ChuyenDis.Count);
            loadDataComingTrip(1);
        }

        private void Image_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            bg_coming.Visibility = Visibility.Collapsed;
            bg_finished.Visibility = Visibility.Visible;
            bg_home.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;

            finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, finishViewModel.ChuyenDis.Count);
            loadDataFinishedTrip(1);
        }

        

        private void loadDataFinishHome(int page)
        {
            var resultFinish = finishViewModel.loadPage(page, finishViewModel.PagingInfo.NumberOfTripInPerPage);
            if (finishViewModel.PagingInfo.TotalPage == 0)
            {
                finishViewModel.PagingInfo.CurrentPage = 0;
            }
            if (resultFinish.Count >= 1)
            {
                borderFinish1.Visibility = Visibility.Visible;
                img_bg1_finished.ImageSource = new BitmapImage(new Uri(resultFinish[0].HinhAnh, UriKind.RelativeOrAbsolute));
                nameFinishTrip_1.Text = resultFinish[0].TenChuyenDi;
                introduceFinishTrip_1.Text = resultFinish[0].GioiThieu;
            }
            else
            {
                borderFinish1.Visibility = Visibility.Collapsed;
            }

            if (resultFinish.Count >= 2)
            {
                borderFinish2.Visibility = Visibility.Visible;
                img_bg2_finished.ImageSource = new BitmapImage(new Uri(resultFinish[1].HinhAnh, UriKind.RelativeOrAbsolute));
                nameFinishTrip_2.Text = resultFinish[1].TenChuyenDi;
                introduceFinishTrip_2.Text = resultFinish[1].GioiThieu;
            }
            else
            {
                borderFinish2.Visibility = Visibility.Collapsed;
            }

        }
        private void loadDataComingHome(int page)
        {
            var resulf = comingViewModel.loadPage(page, comingViewModel.PagingInfo.NumberOfTripInPerPage);
            if (comingViewModel.PagingInfo.TotalPage == 0)
            {
                finishViewModel.PagingInfo.CurrentPage = 0;
            }
            if (resulf.Count >= 1)
            {
                borderComing1.Visibility = Visibility.Visible;
                img_bg1_coming.ImageSource = new BitmapImage(new Uri(resulf[0].HinhAnh, UriKind.RelativeOrAbsolute));
                nameComingTrip1.Text = resulf[0].TenChuyenDi;
                introduceComingTrip1.Text = resulf[0].GioiThieu;
            }
            else
            {
                borderComing1.Visibility = Visibility.Collapsed;
            }

            if (resulf.Count >= 2)
            {
                borderComing2.Visibility = Visibility.Visible;
                img_bg2_coming.ImageSource = new BitmapImage(new Uri(resulf[1].HinhAnh, UriKind.RelativeOrAbsolute));
                nameComingTrip2.Text = resulf[1].TenChuyenDi;
                introduceComingTrip2.Text = resulf[1].GioiThieu;
            }
            else
            {
                borderComing2.Visibility = Visibility.Collapsed;
            }
        }

        private void loadDataFinishedTrip(int page)
        {
            var result = finishViewModel.loadPage(page, finishViewModel.PagingInfo.NumberOfTripInPerPage);

            listViewFinishedTrip.ItemsSource = result;
            pageFinished.Content = $"{finishViewModel.PagingInfo.CurrentPage} / {finishViewModel.PagingInfo.TotalPage}";
        }


        private void loadDataComingTrip(int page)
        {
            var list = comingViewModel.loadPage(page, comingViewModel.PagingInfo.NumberOfTripInPerPage);

            listViewComingTrip.ItemsSource = list;
            pageComing.Content = $"{comingViewModel.PagingInfo.CurrentPage} / {comingViewModel.PagingInfo.TotalPage}";
        }

        
        private void btn_Prev_finishedpage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataFinishedTrip(finishViewModel.PagingInfo.CurrentPage - 1);

        }

        private void btn_Next_finishedpage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataFinishedTrip(finishViewModel.PagingInfo.CurrentPage + 1);
        }

        private void btn_Prev_Finished_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataFinishHome(finishViewModel.PagingInfo.CurrentPage - 1);
        }

        private void btn_Next_Finished_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataFinishHome(finishViewModel.PagingInfo.CurrentPage + 1);

        }

        private void btn_Next_Coming_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataComingHome(comingViewModel.PagingInfo.CurrentPage + 1);
        }

        private void btn_Prev_Coming_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataComingHome(comingViewModel.PagingInfo.CurrentPage - 1);
        }

        private void btn_Prev_comingpage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataComingTrip(comingViewModel.PagingInfo.CurrentPage - 1);
        }

        private void btn_Next_comingpage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            loadDataComingTrip(comingViewModel.PagingInfo.CurrentPage + 1);
        }

        private void textbox_search_coming_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(cbb_coming_search.SelectedIndex == 0)
            {
                string name = textbox_search_coming.Text.Trim();
                if (name != "")
                {
                    if (name.Contains("\n"))
                    {
                        name = name.Replace('\n', ' ');
                    }

                    comingViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiComingByName(name);
                    comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, comingViewModel.ChuyenDis.Count);
                    loadDataComingTrip(1);
                }
                else
                {
                    comingViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiComing();
                    comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, comingViewModel.ChuyenDis.Count);
                    loadDataComingTrip(1);
                }
            }    
            else
            {
                string name = textbox_search_coming.Text.Trim();
                if (name != "")
                {
                    if (name.Contains("\n"))
                    {
                        name = name.Replace('\n', ' ');
                    }

                    comingViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiComingByMemberName(name);
                    comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, comingViewModel.ChuyenDis.Count);
                    loadDataComingTrip(1);
                }
                else
                {
                    comingViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiComing();
                    comingViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, comingViewModel.ChuyenDis.Count);
                    loadDataComingTrip(1);
                }
            }
        }

        private void textbox_search_finished_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbb_finished_search.SelectedIndex == 0)
            {
                string name = textbox_search_finished.Text.Trim();
                if (name != "")
                {
                    if (name.Contains("\n"))
                    {
                        name = name.Replace('\n', ' ');
                    }

                    finishViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiFinishByName(name);
                    finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, finishViewModel.ChuyenDis.Count);
                    loadDataFinishedTrip(1);
                }
                else
                {
                    finishViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiFinish();
                    finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, finishViewModel.ChuyenDis.Count);
                    loadDataFinishedTrip(1);
                }
            }
            else
            {
                string name = textbox_search_finished.Text.Trim();
                if (name != "")
                {
                    if (name.Contains("\n"))
                    {
                        name = name.Replace('\n', ' ');
                    }

                    finishViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiFinishedByMemberName(name);
                    finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, finishViewModel.ChuyenDis.Count);
                    loadDataFinishedTrip(1);
                }
                else
                {
                    finishViewModel.ChuyenDis = ChuyenDiDAO.GetChuyenDiFinish();
                    finishViewModel.PagingInfo = new PagingInfo(NUMBER_ITEMS_TRIP, finishViewModel.ChuyenDis.Count);
                    loadDataFinishedTrip(1);
                }
            }
            
        }

        private void borderFinish1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = finishViewModel.loadPage(finishViewModel.PagingInfo.CurrentPage, finishViewModel.PagingInfo.NumberOfTripInPerPage);

            DetailTrip detailTrip = new DetailTrip(result[0]);
            detailTrip.Show();
            this.Close();
        }

        private void borderFinish2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = finishViewModel.loadPage(finishViewModel.PagingInfo.CurrentPage, finishViewModel.PagingInfo.NumberOfTripInPerPage);

            DetailTrip detailTrip = new DetailTrip(result[1]);
            detailTrip.Show();
            this.Close();
        }

        private void borderComing1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = comingViewModel.loadPage(comingViewModel.PagingInfo.CurrentPage, comingViewModel.PagingInfo.NumberOfTripInPerPage);

            DetailTrip detailTrip = new DetailTrip(result[0]);
            detailTrip.Show();
            this.Close();
        }

        private void borderComing2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = comingViewModel.loadPage(comingViewModel.PagingInfo.CurrentPage, comingViewModel.PagingInfo.NumberOfTripInPerPage);

            DetailTrip detailTrip = new DetailTrip(result[1]);
            detailTrip.Show();
            this.Close();
        }

        private void listViewComingTrip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = comingViewModel.loadPage(comingViewModel.PagingInfo.CurrentPage, comingViewModel.PagingInfo.NumberOfTripInPerPage);

            DetailTrip detailTrip = new DetailTrip(result[listViewComingTrip.SelectedIndex]);
            detailTrip.Show();
            this.Close();

        }

        private void listViewFinishedTrip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = finishViewModel.loadPage(finishViewModel.PagingInfo.CurrentPage, finishViewModel.PagingInfo.NumberOfTripInPerPage);

            DetailTrip detailTrip = new DetailTrip(result[listViewFinishedTrip.SelectedIndex]);
            detailTrip.Show();
            this.Close();
        }

        private void cbb_finished_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
