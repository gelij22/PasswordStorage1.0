using PasswordStorage.data;
using PasswordStorage.Generator;
using PasswordStorage.ModelUser;
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
using System.Data.SQLite;

namespace PasswordStorage
{
    public partial class LoginPage : Window
    {
        User user = new User();
        string password = "";
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox.IsChecked.Value)
            {
                pwdTextBox.Text = TBoxPassword.Password; // скопируем в TextBox из PasswordBox
                pwdTextBox.Visibility = Visibility.Visible; // TextBox - отобразить
                TBoxPassword.Visibility = Visibility.Hidden; // PasswordBox - скрыть               
            }
            else
            {
                TBoxPassword.Password = pwdTextBox.Text; // скопируем в PasswordBox из TextBox
                pwdTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
                TBoxPassword.Visibility = Visibility.Visible; // PasswordBox - отобразить                
            }
        }


        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();             
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxUserName.Text.Trim();
            password = TBoxPassword.Password.Trim();
            if(login.Length < 5)
            {
                TBoxUserName.ToolTip = "Поле введено некорректно";
                TBoxUserName.Background = Brushes.Red;
                return;
            }else if(password.Length < 5)
            {
                TBoxPassword.ToolTip = "Поле введено некорректно";
                TBoxPassword.Background = Brushes.Red;
                return;
            }
            else
            {
                TBoxUserName.ToolTip = "";
                TBoxUserName.Background = Brushes.Transparent;
                TBoxPassword.ToolTip = "";
                TBoxPassword.Background = Brushes.Transparent;
                user = new User(login, password);
                //Если пользователь уже создан предупредить и вернуть обратно
                User savedUser = null;         
                using (data.AppContext db = new data.AppContext())
                {
                    savedUser = db.Users.FirstOrDefault();
                    if (savedUser != null)
                    {
                        MessageBox.Show("Пользователь уже создан");
                        this.Close();
                    }
                    else
                    {
                        db.Users.Add(user);
                        string queryString = @"delete from sqlite_sequence where name='Entries'";
                        using (SQLiteConnection conn = SqlHelper.OpenDbConnection())
                        using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
                        {
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                            db.SaveChanges();
                        MessageBox.Show("Пользователь успешно сохранен");
                    }                
                }               
            }

           Close();
        }

        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            password = SecurePasswordGenerator.GenerateSecurePassword();
            TBoxPassword.Password = password;
        }
    }
}
