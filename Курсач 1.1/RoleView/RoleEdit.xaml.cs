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
using static Курсач_1._1.Windows.EditRoles;

namespace Курсач_1._1.RoleView
{
    /// <summary>
    /// Логика взаимодействия для RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : UserControl
    {
        private IRoleWindowsCodeBehind _RoleCodeBehind;
        Model1 db = new Model1();
        List<string> listTo;
        roles role1;

        public RoleEdit(IRoleWindowsCodeBehind roleWindowsCodeBehind, roles role)
        {
            _RoleCodeBehind = roleWindowsCodeBehind;
            db.roles.Load();
            role1 = role;
            InitializeComponent();

            tbRoleName.Text = role.name;
            listTo = db.roles.Local.Select(i => i.name).ToList();

            cbTo.ItemsSource = listTo;
            cbTo.SelectedItem = role.Role;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleMain);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbRoleName.Text))
            {
                nameroleIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                nameroleIm.Visibility = Visibility.Hidden;
            }


            if (cbTo.SelectedItem.ToString() == role1.Role && tbRoleName.Text == role1.name)
            {
                priorIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                var list_roles = db.roles.Local.Where(i => i.name == ((roles)(cbTo.SelectedItem)).name);
                role1.name = list_roles.FirstOrDefault().name;
                role1.priority = list_roles.FirstOrDefault().priority;

                try
                {
                    db.roles.AddOrUpdate(role1);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }
            }
        }
    }
}
