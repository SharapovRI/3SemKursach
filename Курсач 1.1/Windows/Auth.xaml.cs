using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Курсач_1._1
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        Model1 db = new Model1();
        public Auth()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.users.Load();
            var a = db.users.Local.FirstOrDefault(log => log.login == LoginBox.Text && log.password == Hashing.GetHash(PassBox.Password));
            if (a is null)
            {
                PassBox.Password = "";
                LoginBox.Text = "";
                alarmIm.Visibility = Visibility.Visible;
                //LoginBox.BorderBrush = Brushes.Red;
            }
            else
            {
                alarmIm.Visibility = Visibility.Hidden;
                MainWindow mainWindow = new MainWindow(a);
                this.Close();
                mainWindow.ShowDialog();
            }
            
        }
    }
}
