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
using Курсач_1._1.Relay;
using static Курсач_1._1.Windows.EditRoles;

namespace Курсач_1._1.RoleView
{
    public partial class Roles : UserControl
    {
        Model1 db = new Model1();
        private IRoleWindowsCodeBehind _RoleCodeBehind;

        public Roles(IRoleWindowsCodeBehind codeBehind)
        {
            db.roles.Load();
            _RoleCodeBehind = codeBehind;
            InitializeComponent();
            dgroles.ItemsSource = db.roles.Local;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _RoleCodeBehind.Back();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (((roles)dgroles.SelectedItem).name != "Admin")
            {
                AcceptExit acceptExit = new AcceptExit("Are you sure you want to delete the role?");
                acceptExit.Owner = (Window)_RoleCodeBehind;
                if (acceptExit.ShowDialog() == true)
                {
                    db.roles.Load();
                    db.roles.Local.Remove((roles)dgroles.SelectedItem);
                    db.SaveChanges();
                    dgroles.ItemsSource = db.roles.Local;
                }
            }
            else
            {
                AcceptExit acceptExit = new AcceptExit("You can't delete admin.");
                acceptExit.Owner = (Window)_RoleCodeBehind;
                acceptExit.ShowDialog();
            }
        }

        private void dgroles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgroles.SelectedItems.Count != 0)
            {
                btnDel.IsEnabled = true;
                btnEdit.IsEnabled = true;
            }
            else
            {
                btnDel.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleEdit, (roles)dgroles.SelectedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _RoleCodeBehind.LoadRoleWindowView(ViewTypeRole.RoleAdd);
        }
    }
}
