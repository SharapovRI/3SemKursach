using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EditPasswordUser.xaml
    /// </summary>
    public partial class EditPasswordUser : Window
    {
        users User;
        Model1 db = new Model1();
        public EditPasswordUser(users user)
        {
            User = user;
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (pbFirstPass.Password != pbSecondPass.Password)
            {
                passIm.ToolTip = "Passwords are not equal";
                passIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                passIm.Visibility = Visibility.Hidden;
            }

            if (Hashing.GetHash(pbFirstPass.Password) == Hashing.GetHash(User.password))
            {
                passIm.ToolTip = "Haven't changes";
                passIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                passIm.Visibility = Visibility.Hidden;
            }

            User.password = Hashing.GetHash(pbFirstPass.Password);

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
    }
}
