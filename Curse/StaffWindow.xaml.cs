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
    /// Логика взаимодействия для StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
            StaffSerach();
            StaffList.ItemsSource = CurseEntities.GetContext().Staff.ToList();
        }
        private void StaffSerach()
        {
            var CurrentStaff = CurseEntities.GetContext().Staff.ToList();
            CurrentStaff = CurrentStaff.Where(p => p.Id.ToString().Contains(TBStaffSearch.Text.ToString())).ToList();
            StaffList.ItemsSource = CurrentStaff.ToList();
        }
           
        private void EditStaffButton_Click(object sender, RoutedEventArgs e)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow((sender as Button).DataContext as Staff);
            addStaffWindow.Show();
            this.Hide();
        }

        
        private void TBStaffSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            StaffSerach();
        }

        private void ExitStaffButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow(null);
            addStaffWindow.Show();
            this.Hide();
        }

        private void DeleteStaffButton_Click(object sender, RoutedEventArgs e)
        {
            var StaffForRemoving = StaffList.SelectedItems.Cast<Staff>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить следующие {StaffForRemoving.Count()} элементов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CurseEntities.GetContext().Staff.RemoveRange(StaffForRemoving);
                    CurseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    StaffList.ItemsSource = CurseEntities.GetContext().Staff.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                CurseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                StaffList.ItemsSource = CurseEntities.GetContext().Staff.ToList();
            }
        }
    }
}
