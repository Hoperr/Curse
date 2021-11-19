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
    /// Логика взаимодействия для AddKlientWindow.xaml
    /// </summary>
    public partial class AddKlientWindow : Window
    {
        private Klients NewKlient = new Klients();
        
        
        public AddKlientWindow(Klients SelectedKlient)
        {
            InitializeComponent();
            if (SelectedKlient != null)
                NewKlient = SelectedKlient;

            DataContext = NewKlient;
        }

       

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Hide();
        }

        private void Addkl_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(NewKlient.Name))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(NewKlient.Surname))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(NewKlient.Otch))
                errors.AppendLine("Укажите очетство");
            if (string.IsNullOrWhiteSpace(NewKlient.Gender))
                errors.AppendLine("Укажите пол");
            if (string.IsNullOrWhiteSpace(NewKlient.PhoneNumber))
                errors.AppendLine("Укажите номер телефона");
        
            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;

            }
            if (NewKlient.Id == 0)
                CurseEntities.GetContext().Klients.Add(NewKlient);

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
    }
}
