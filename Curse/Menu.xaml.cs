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

namespace Curse
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KlientsButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Hide();
        }

        private void HotelsButton_Click(object sender, RoutedEventArgs e)
        {
            HotelWindow hotelWindow = new HotelWindow();
            hotelWindow.Show();
            this.Hide();
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            TourWindow tourWindow = new TourWindow();
            tourWindow.Show();
            this.Hide();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow();
            ordersWindow.Show();
            this.Hide();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }



        private void AdminStaffListButton_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Hide();
        }
       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurseEntities.CurrentStaff.Role == "2")
            {
                AdminStaffListButton.Visibility = Visibility.Hidden;
            }
        }
    }
}