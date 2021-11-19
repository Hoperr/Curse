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
    /// Логика взаимодействия для AddStaffWindow.xaml
    /// </summary>
    public partial class AddStaffWindow : Window
    {
        private Staff NewStaff = new Staff();
        public AddStaffWindow(Staff SelectedStaff )
        {
            InitializeComponent();
            if (SelectedStaff != null)
                NewStaff = SelectedStaff;
            DataContext = NewStaff;
        }

        private void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(NewStaff.Name))
                errors.AppendLine("Введите имя");
            if (string.IsNullOrWhiteSpace(NewStaff.Surname))
                errors.AppendLine("Введите фамилию");
            if (string.IsNullOrWhiteSpace(NewStaff.Role))
                errors.AppendLine("Введите код роли");
            if (string.IsNullOrWhiteSpace(NewStaff.Login))
                errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(NewStaff.Password))
                errors.AppendLine("Введите пароль");
            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (NewStaff.Id == 0)
                CurseEntities.GetContext().Staff.Add(NewStaff);
            try
            {
                CurseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Staffback_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Hide();
        }

       
    }
}
