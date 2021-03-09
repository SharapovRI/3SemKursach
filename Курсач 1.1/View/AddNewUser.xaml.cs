using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : UserControl
    {
        Model1 db = new Model1();
        private IMainWindowsCodeBehind _MainCodeBehind;
        private List<string> roleList;

        public AddNewUser(IMainWindowsCodeBehind codeBehind, users user)
        {
            InitializeComponent();
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            db.roles.Load();
            if (user.roles.Role != "Admin")
            {
                roleList = db.roles.Local.Where(i => i.priority < user.roles.priority).Select(i => i.Role).ToList();
            }
            else
            {
                roleList = db.roles.Local.Select(i => i.Role).ToList();
            }
            _MainCodeBehind = codeBehind;

            cbRole.ItemsSource = roleList;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                logIm.ToolTip = "Enter login";
                logIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                logIm.Visibility = Visibility.Hidden;
            }

            if (db.users.Local.Where(i => i.login == tbLogin.Text).ToList().Count != 0)
            {
                logIm.ToolTip = "This login already est'";
                logIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                logIm.Visibility = Visibility.Hidden;
            }

            if (cbRole == null)
            {
                roleIm.ToolTip = "Enter role";
                roleIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                roleIm.Visibility = Visibility.Hidden;
            }

            if (firstPass.Password != secondPass.Password)
            {
                passIm.ToolTip = "Passwords not equal";
                passIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                passIm.Visibility = Visibility.Hidden;
            }

            if (firstPass.Password.Length == 0 || secondPass.Password.Length == 0)
            {
                passIm.ToolTip = "Enter passwords";
                passIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                passIm.Visibility = Visibility.Hidden;
            }

            if (!string.IsNullOrWhiteSpace(tbLogin.Text) && cbRole != null && firstPass.Password == secondPass.Password)
            {
                db.roles.Load();
                var a = db.roles.Local.FirstOrDefault(q => q.name == cbRole.SelectedItem.ToString());
                users user = new users(tbLogin.Text, Hashing.GetHash(firstPass.Password), a.idrole);

                try
                {
                    db.users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }
                users user1 = db.users.FirstOrDefault(i => i.login == user.login);

                try
                {
                    db.changedtasks.Add(new changedtasks(user1));
                    db.SaveChanges();
                    _MainCodeBehind.LoadView(ViewType.TaskToLower);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }
            }
        }
    }
}
