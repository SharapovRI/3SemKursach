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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Курсач_1._1.Windows.EditRoles;

namespace Курсач_1._1.RoleView
{
    /// <summary>
    /// Логика взаимодействия для RoleAdd.xaml
    /// </summary>
    public partial class RoleAdd : UserControl
    {
        private IRoleWindowsCodeBehind _RoleCodeBehind;
        Model1 db = new Model1();
        List<string> Equal;
        List<string> BiggerThan;
        roles Role;

        public RoleAdd(IRoleWindowsCodeBehind roleWindowsCodeBehind, roles role)
        {
            db.roles.Load();
            this.Role = role;
            _RoleCodeBehind = roleWindowsCodeBehind;

            InitializeComponent();
            Equal = db.roles.Local.Select(i => i.name).ToList();
            BiggerThan = db.roles.Local.Select(i => i.name).ToList();

            Equal.Insert(0, "-");
            BiggerThan.Insert(0, "-");
            BiggerThan.Add("BiggerThanAll");
            BiggerThan.Add("LessThanAll");

            cbEqual.ItemsSource = Equal;
            cbBiggerThan.ItemsSource = BiggerThan;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleMain);
        }

        private void cbEqual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cbEqual.SelectedItem.ToString();
            cbBiggerThan.SelectedItem = "-";
            cbEqual.SelectedItem = selected;
        }

        private void cbBiggerThan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = cbBiggerThan.SelectedItem.ToString();
            cbEqual.SelectedItem = "-";
            cbBiggerThan.SelectedItem = selected;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbRoleName.Text))
            {
                nameroleIm.ToolTip = "Enter correct name";
                nameroleIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                nameroleIm.Visibility = Visibility.Hidden;
            }

            var selectedItemToRole = db.roles.Local.Where(i => i.name == cbBiggerThan.SelectedItem.ToString()).FirstOrDefault();
            var a = db.roles.Local.Where(i => i.name == tbRoleName.Text).ToList();

            if (a.Count != 0 && string.IsNullOrWhiteSpace(tbRoleName.Text))
            {
                nameroleIm.ToolTip = "This name already exists";
                nameroleIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                nameroleIm.Visibility = Visibility.Hidden;
            }

            if (cbBiggerThan.SelectedIndex == 0 && cbEqual.SelectedIndex == 0)
            {
                priorIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                priorIm.Visibility = Visibility.Hidden;
            }

            if (cbEqual.SelectedIndex != 0)
            {
                var b = db.roles.Local.Where(i => i.name == cbEqual.SelectedItem.ToString());
                roles role = new roles(tbRoleName.Text, b.FirstOrDefault().priority);

                try
                {
                db.roles.Add(role);
                db.SaveChanges();
                _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleMain);

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }
                return;
            }

            if (cbBiggerThan.SelectedItem.ToString() == "BiggerThanAll")
            {
                var roleslist = db.roles.Local.Where(i => i.priority < Role.priority).ToList();
                roles role;
                if (roleslist.Count != 0)
                {
                    var roleslist2 = roleslist.OrderByDescending(i => i.priority).FirstOrDefault();
                    role = new roles(tbRoleName.Text, roleslist2.priority + 1);
                }
                else
                {
                    role = new roles(tbRoleName.Text, 0);
                }

                try
                {
                    db.roles.Add(role);
                    db.SaveChanges();
                    _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleMain);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }

                return;
            }
            else if (cbBiggerThan.SelectedItem.ToString() == "LessThanAll")
            {
                var roleslist = db.roles.Local.Where(i => i.priority < Role.priority).ToList();
                roles role;
                role = new roles(tbRoleName.Text, 0);
                foreach (var item in roleslist)
                {
                    ++item.priority;
                }

                try
                {
                    db.roles.Add(role);
                    db.SaveChanges();
                    _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleMain);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }

                return;
            }
            else
            {
                var roleslist0 = db.roles.Local.Where(i => i.priority < Role.priority && i.priority > selectedItemToRole.priority).ToList();
                roles role;
                if (roleslist0.Count == 0)
                {
                    role = new roles(tbRoleName.Text, selectedItemToRole.priority + 1);
                }
                else
                {
                    role = new roles(tbRoleName.Text, selectedItemToRole.priority + 1);
                    foreach (var item in roleslist0)
                    {
                        ++item.priority;
                    }
                }

                try
                {
                    db.roles.Add(role);
                    db.SaveChanges();
                    _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleMain);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }

                return;
            }
        }
    }
}
