using Microsoft.Data.Sqlite;
using PasswordStorage.Components;
using PasswordStorage.data;
using PasswordStorage.ModelUser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;
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

namespace PasswordStorage
{
    public partial class MainWindow : Window
    {
        User CurrentUser = null;
        private readonly DbEntryService _dbSevice = new DbEntryService();
        public MainWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            NameUser.Content = user.Login;
            _dbSevice = new DbEntryService();
            viesEntries();
        }
        private void viesEntries()
        {
            try
            {
                List<Model.Entry> entries = _dbSevice.GetAll();
                listsofEntries.Items.Clear();
                foreach(var entry in entries)
                {
                   listsofEntries.Items.Add(entry);
                }
               
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
  

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            viesEntries();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            
            // Считать  данные
            if(listsofEntries.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            else
            {                                              
                string[] arr = listsofEntries.SelectedItem.ToString().Trim().Split(' ');            
                int id = int.Parse(arr[0]); 
                string name = arr[2];
                string pass = arr[5];
                Model.Entry entry = new Model.Entry(id, name, pass);
                _dbSevice.Delete(entry);    
                viesEntries();
            }             
        }

        private void CopyBtnClick(object sender, RoutedEventArgs e)
        {
            if(listsofEntries.SelectedIndex == -1)
            {
                MessageBox.Show("Выбериет элемент");
                return;
            }
            else
            {
                string[] arr = listsofEntries.SelectedItem.ToString().Trim().Split(' ');                
                Clipboard.SetText(arr[arr.Length - 1]);
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEntryWindow entryWindow= new AddEntryWindow();
            entryWindow.ShowDialog();
            viesEntries();
        }

       

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _dbSevice.Dispose();
        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listsofEntries.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            else
            {
                string[] arr = listsofEntries.SelectedItem.ToString().Trim().Split(' ');
                int id = int.Parse(arr[0]);
                string name = arr[2];
                string pass = arr[5];
                Model.Entry entry = new Model.Entry(id, name, pass);
                ModifyWindow modifyWindow = new ModifyWindow(entry);
                modifyWindow.ShowDialog();
                viesEntries();
            }
        }

      
    }
}
