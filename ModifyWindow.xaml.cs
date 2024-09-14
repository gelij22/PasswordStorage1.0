using PasswordStorage.data;
using PasswordStorage.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using System.Xml.Linq;

namespace PasswordStorage
{
    public partial class ModifyWindow : Window
    {
        int id;

        public ModifyWindow(Model.Entry entry)
        {
            InitializeComponent();
            id = entry.id;
            NameTbox.Text = entry.Name;
            PassBox.Password = entry.Password;            
        }

        private void GenerateButton(object sender, RoutedEventArgs e)
        {
            string password = SecurePasswordGenerator.GenerateSecurePassword();
            PassBox.Password = password;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данные несохранены!");
            Close();
        }

        private void AddEntryClick(object sender, RoutedEventArgs e)
        {
            DbEntryService entryService = new DbEntryService();
            if (NameTbox.Text.Trim() == string.Empty || PassBox.Password == string.Empty)
            {
                MessageBox.Show("Невeрные данные!");
                return;
            }
            else
            {
                string name = NameTbox.Text.Trim();
                string pass = PassBox.Password;
                Model.Entry entry = new Model.Entry(id, name, pass);
                entryService.Modify(entry);
            }
            Close();           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox.IsChecked.Value)
            {
                pwdTextBox.Text = PassBox.Password; // скопируем в TextBox из PasswordBox
                pwdTextBox.Visibility = Visibility.Visible; // TextBox - отобразить
                PassBox.Visibility = Visibility.Hidden; // PasswordBox - скрыть               
            }
            else
            {
                PassBox.Password = pwdTextBox.Text; // скопируем в PasswordBox из TextBox
                pwdTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
                PassBox.Visibility = Visibility.Visible; // PasswordBox - отобразить                
            }
        }
    }
}
