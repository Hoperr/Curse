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
    /// Логика взаимодействия для AddTourWindow.xaml
    /// </summary>
    public partial class AddTourWindow : Window
    {
        private Tour NewTour = new Tour();
        public AddTourWindow(Tour SelectedTour)
        {
            InitializeComponent();
            if (SelectedTour != null)
               NewTour = SelectedTour;
            DataContext = NewTour;
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            if (NewTour.Id == 0)
                CurseEntities.GetContext().Tour.Add(NewTour);
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

        private void Tourback_Click(object sender, RoutedEventArgs e)
        {
            TourWindow tourWindow = new TourWindow();
            tourWindow.Show();
            this.Hide();
        }
    }
}
