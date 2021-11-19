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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Curse
{
 
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginBox.Focus();
        }
        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
           if (LoginBox.Text.Length>0)
            {
                if (Pass.Text.Length>0)
                {
                    try
                    {
                        var user = CurseEntities.GetContext().Staff.First(p => p.Login == LoginBox.Text & p.Password == Pass.Text);
                        if (user!=null)
                        {
                            CurseEntities.CurrentStaff = user;
                            if (user.Role == "1")
                            {
                               Menu menu = new Menu();
                               menu.Show();
                               this.Hide();
                            }
                            else if (user.Role=="2")
                            {
                                Menu menu = new Menu();
                                menu.Show();
                                this.Hide();
                               
                            }
                            else
                            {
                                MessageBox.Show("Введены некоректные данные!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                        }
                       else
                        {
                            MessageBox.Show("Проверьте правильность введенных данных!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                            Pass.Clear();
                        }
                      
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Введены некоректные данные!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Pass.Background = Brushes.Red;
                }
            }
           else
            {
                MessageBox.Show("Введите Логин!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoginBox.Background = Brushes.Red;
            }
        }

        private void LoginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pass.Focus();
            }
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginbutton_Click(loginbutton, null);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
        }
    }
}
