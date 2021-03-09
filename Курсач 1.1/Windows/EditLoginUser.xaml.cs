using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

namespace Курсач_1._1.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditInfoUser.xaml
    /// </summary>
    public partial class EditLoginUser : Window
    {
        users User;
        Model1 db = new Model1();
        public EditLoginUser(users user)
        {
            User = user;
            InitializeComponent();
            tbChangeLogin.Text = user.Login;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (tbChangeLogin.Text == User.login)
            {
                logIm.ToolTip = "Haven't changes";
                logIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                logIm.Visibility = Visibility.Hidden;
            }

            db.users.Load();
            var list = db.users.Local.Where(i => i.login == tbChangeLogin.Text).ToList();
            if (list.Count == 0)
            {
                User.login = tbChangeLogin.Text;

                try
                {
                    db.users.AddOrUpdate(User);
                    db.SaveChanges();
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }
            }
            else
            {
                logIm.ToolTip = "This login already est'";
                logIm.Visibility = Visibility.Visible;
                return;
            }
        }
    }
}
