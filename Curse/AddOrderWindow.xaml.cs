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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private Orders NewOrder = new Orders();

        public AddOrderWindow(Orders SelectedOrder)
        {
            InitializeComponent();
            if (SelectedOrder != null)
                NewOrder = SelectedOrder;
            DataContext = NewOrder;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (NewOrder.Id == 0)
                CurseEntities.GetContext().Orders.Add(NewOrder);
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

        private void Orderback_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow();
            ordersWindow.Show();
            this.Hide();
        }
    }
}
