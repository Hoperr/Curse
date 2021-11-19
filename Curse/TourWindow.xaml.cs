using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Curse
{
    /// <summary>
    /// Логика взаимодействия для TourWindow.xaml
    /// </summary>
    public partial class TourWindow : Window
    {
        public TourWindow()
        {
            InitializeComponent();
            TourSearch();
            ToursList.ItemsSource = CurseEntities.GetContext().Tour.ToList();
        }
        private void TourSearch()
        {
            var CurrentTour = CurseEntities.GetContext().Tour.ToList();
            CurrentTour = CurrentTour.Where(p => p.Id.ToString().Contains(TBSearchTour.Text.ToString())).ToList();
            ToursList.ItemsSource = CurrentTour.ToList();
        }
        private void TourExitButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void TBSearchTour_TextChanged(object sender, TextChangedEventArgs e)
        {
            TourSearch();
        }

        private void TourAddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTourWindow addTourWindow = new AddTourWindow(null);
            addTourWindow.Show();
            this.Hide();
        }

        private void TourDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var ToursForRemoving = ToursList.SelectedItems.Cast<Tour>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить следующие {ToursForRemoving.Count()} элементов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CurseEntities.GetContext().Tour.RemoveRange(ToursForRemoving);
                    CurseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    ToursList.ItemsSource = CurseEntities.GetContext().Tour.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditTourButton_Click(object sender, RoutedEventArgs e)
        {
            AddTourWindow addTourWindow = new AddTourWindow((sender as Button).DataContext as Tour);
            addTourWindow.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurseEntities.CurrentStaff.Role == "2")
            {
                TourAddButton.Visibility = Visibility.Hidden;
                TourDeleteButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
