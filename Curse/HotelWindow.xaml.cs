using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Curse
{
    /// <summary>
    /// Логика взаимодействия для HotelWindow.xaml
    /// </summary>
    public partial class HotelWindow : Window
    {
        public HotelWindow()
        {
            InitializeComponent();
            HotelSearch();
            LVHotels.ItemsSource = CurseEntities.GetContext().Hotel.ToList();
        }
        private void HotelSearch()
        {
            var CurrentHotel = CurseEntities.GetContext().Hotel.ToList();
            CurrentHotel = CurrentHotel.Where(p => p.Id.ToString().Contains(TBhotelSearch.Text.ToString())).ToList();
            LVHotels.ItemsSource = CurrentHotel.ToList();
        }

        private void TBhotelSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            HotelSearch();
        }

        private void HotelExitButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }
    }
}
