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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1.View
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : UserControl
    {
        Model1 db = new Model1();
        private IMainWindowsCodeBehind _MainCodeBehind;
        private List<string> roleList;
        users User;
        users MainUser;

        public EditUser(IMainWindowsCodeBehind codeBehind, users user)
        {
            User = user;
            InitializeComponent();

            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            db.roles.Load();

            _MainCodeBehind = codeBehind;
            MainUser = _MainCodeBehind.GetThisUser(); //получение юзера под которым мы зашли

            if (user.roles.Role != "Admin")
            {
                roleList = db.roles.Local.Where(i => i.priority < MainUser.roles.priority).Select(i => i.Role).ToList();
            }
            else
            {
                roleList = db.roles.Local.Select(i => i.Role).ToList();
            }


            tbLogin.Text = user.login;
            cbRole.ItemsSource = roleList;
            cbRole.SelectedItem = user.roles.name;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbLogin.Text) && cbRole != null && firstPass.Password == secondPass.Password)
            {
                User.login = tbLogin.Text;
                db.users.AddOrUpdate(User);
            }

            if (cbRole.SelectedItem.ToString() != User.roles.name)
            {
                db.roles.Load();
                var a = db.roles.Local.FirstOrDefault(q => q.name == cbRole.SelectedItem.ToString());
                User.role = a.idrole;
                db.users.AddOrUpdate(User);
            }

            if (firstPass.Password != string.Empty)
            {
                if (firstPass.Password == secondPass.Password && Hashing.GetHash(firstPass.Password) != User.password)
                {
                    User.password = Hashing.GetHash(firstPass.Password);
                    db.users.AddOrUpdate(User);
                }
                else
                {
                    tbLogin.Text = "ssssssss";
                }
            }

            try
            {
                db.SaveChanges();
                _MainCodeBehind.LoadView(ViewType.TaskToLower);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter correct values");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _MainCodeBehind.LoadView(ViewType.TaskToLower);
        }
    }
}
