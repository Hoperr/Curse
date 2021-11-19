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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            KlientSearch();
            KlientsList.ItemsSource = CurseEntities.GetContext().Klients.ToList();
            
        }
        private void KlientSearch()
        {
            var CurrentKlient = CurseEntities.GetContext().Klients.ToList();
            CurrentKlient = CurrentKlient.Where(p => p.Name.ToString().Contains(TBSearch.Text.ToString())).ToList();
            KlientsList.ItemsSource = CurrentKlient.ToList();
        }
        private void KlientExitButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KlientSearch();
        }

        private void AddKlientButton_Click(object sender, RoutedEventArgs e)
        {
            AddKlientWindow addKlientWindow = new  AddKlientWindow(null);
            addKlientWindow.Show();
            this.Hide();

        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                CurseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                KlientsList.ItemsSource = CurseEntities.GetContext().Klients.ToList();
            }
        }

        private void DeleteKlientButton_Click(object sender, RoutedEventArgs e)
        {
            var KlientsForRemoving = KlientsList.SelectedItems.Cast<Klients>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить следующие {KlientsForRemoving.Count()} элементов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CurseEntities.GetContext().Klients.RemoveRange(KlientsForRemoving);
                    CurseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    KlientsList.ItemsSource = CurseEntities.GetContext().Klients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            AddKlientWindow addKlientWindow = new AddKlientWindow((sender as Button).DataContext as Klients);
            addKlientWindow.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurseEntities.CurrentStaff.Role == "2")
            {
               AddKlientButton.Visibility = Visibility.Hidden;
              DeleteKlientButton.Visibility = Visibility.Hidden;

            }
        }
    }
}
    