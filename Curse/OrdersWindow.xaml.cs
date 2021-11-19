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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Curse
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : System.Windows.Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
            OrderSearch();
            OrderList.ItemsSource = CurseEntities.GetContext().Orders.ToList();
            OrderList2.ItemsSource = CurseEntities.GetContext().Orders.ToList();
        }
        private void OrderSearch()
        {
            var CurrentOrder = CurseEntities.GetContext().Orders.ToList();
            CurrentOrder = CurrentOrder.Where(p => p.Id.ToString().Contains(TBSearchOrder.Text.ToString())).ToList();
            OrderList.ItemsSource = CurrentOrder.ToList();
        }


        private void OrdersExitButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void TBSearchOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            OrderSearch();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow(null);
            addOrderWindow.Show();
            this.Hide();
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var OrdersForRemoving = OrderList.SelectedItems.Cast<Orders>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить следующие {OrdersForRemoving.Count()} элементов?", "Внимание",
                  MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CurseEntities.GetContext().Orders.RemoveRange(OrdersForRemoving);
                    CurseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    OrderList.ItemsSource = CurseEntities.GetContext().Klients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow((sender as System.Windows.Controls.Button).DataContext as Orders);
            addOrderWindow.Show();
            this.Hide();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                CurseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                OrderList.ItemsSource = CurseEntities.GetContext().Orders.ToList();
            }
        }

        private void ReportOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < OrderList2.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 20;
                myRange.Value2 = OrderList2.Columns[j].Header;
            }
            for (int i = 0; i < OrderList2.Columns.Count; i++)
            {
                for (int j = 0; j < OrderList2.Items.Count; j++)
                {
                    TextBlock b = OrderList2.Columns[i].GetCellContent(OrderList2.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrderList2.Visibility = Visibility.Hidden;
            if (CurseEntities.CurrentStaff.Role == "2")
            {
                AddOrderButton.Visibility = Visibility.Hidden;
                DeleteOrderButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
