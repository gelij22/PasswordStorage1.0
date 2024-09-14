using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordStorage
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            var mainWindow = new MainWindow();
           // 
            var singInPage= new SingInPage();
            if(singInPage.ShowDialog() == true)
            {
                                 
                    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    Current.MainWindow = mainWindow;
                    singInPage.Hide();
                   // mainWindow.Show();
            }
               
            else
            {
                MessageBox.Show("Unable to load data.", "Error", MessageBoxButton.OK);
                Current.Shutdown(-1);
            }
        }


        bool init = false;
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (!init)
            {
                this.MainWindow.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
                init = true;
            }
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window toClose = new MainWindow();
            this.MainWindow = new MainWindow();
            this.MainWindow.Close();
            
        }
        */
    }
}
