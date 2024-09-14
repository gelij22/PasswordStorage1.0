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
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PasswordStorage.data;
using System.Data.SQLite;


namespace PasswordStorage
{
    public partial class SingInPage : Window
    {
        User user = null;
        data.AppContext db = null;

        private void DataLoad()
        {
            using (db = new data.AppContext())
            {
                user = db.Users.FirstOrDefault();
                if (user == null)
                {
                    Hide();
                    LoginPage loginPage = new LoginPage();
                    loginPage.ShowDialog();
                  
                    //TBoxUserName.Text = "";
                    //TBoxPassword.Password = "";

                }
                

            }
        }

        private void deleteUser()
        {
            string queryString = @"delete  from Users";
            string queryStringEntries = @"delete from Entries";
            using (SQLiteConnection conn = SqlHelper.OpenDbConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        using (SQLiteCommand comand = new SQLiteCommand(queryStringEntries, conn))
                        {
                            comand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Пользователь удален!");
                        Hide();
                        SingInPage page = new SingInPage();
                        page.Show();
                    }
                    else
                    {
                        MessageBox.Show("Что-то неладное!");
                    }
                }

            }
        }

        public SingInPage()
        {
            InitializeComponent();
            DataLoad();
            
        }
        public bool Sucsess()
        {
            if (user != null)
            {
                return true;
            }
            return false;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxUserName.Text.Trim();
            string password = TBoxPassword.Password.Trim();
            if (login == string.Empty)
            {
                TBoxUserName.ToolTip = "Неверные данные!";
                TBoxUserName.Background = Brushes.OrangeRed;
            }
            else if (password == string.Empty)
            {
                TBoxPassword.ToolTip = "Неверный пароль!";
                TBoxPassword.Background = Brushes.OrangeRed;
            }

            else
            {
                TBoxUserName.ToolTip = "";
                TBoxUserName.Background = Brushes.Transparent;
                TBoxPassword.ToolTip = "";
                TBoxPassword.Background = Brushes.Transparent;
                //if (user != null)
                DataLoad();
                if(password == user.Password && login== user.Login) // Ошибка юрез нул последняя
                {
                    Hide();
                    MainWindow main = new MainWindow(user);
                    main.ShowDialog();
                }
                else
                {
                   MessageBoxResult result =   MessageBox.Show("Вы ввели что-то некорректно. Удалить пользователя", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                   if(result == MessageBoxResult.Yes)
                    {
                        deleteUser();
                    }     
                }
            }
            if (!IsVisible)
            {
                Show();
                Close();
            }
        }
        private void TBoxUserName_MouseEnter(object sender, MouseEventArgs e)
        {
            TBoxUserName.ToolTip = "";
            TBoxUserName.Background = Brushes.White;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (user != null)
            {
                e.Cancel = true;
            }
            e.Cancel = false;
        }

        private void Hyperlink_Navigate(object sender, RoutedEventArgs e)
        {
            Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.ShowDialog();
            if (!IsVisible)
            {
                DataLoad();
                Show();
            }
        }
    }
}
