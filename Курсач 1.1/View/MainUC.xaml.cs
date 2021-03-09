using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Курсач_1._1
{
    /// <summary>
    /// Логика взаимодействия для MainUC.xaml
    /// </summary>
    public partial class MainUC : UserControl
    {
        Model1 db = new Model1();
        users User;
        public MainUC(users user)
        {
            User = user;
            InitializeComponent();
            if (user.roles.priority < 1)
            {
                RedRe.Visibility = Visibility.Hidden;
            }
        }

        private void btnCompl_Click(object sender, RoutedEventArgs e)
        {
            db.tasks.Load();
            ((tasks)lb.SelectedItem).status = 2;
            db.tasks.AddOrUpdate((tasks)lb.SelectedItem);
            db.SaveChanges();
            var b = from i in db.tasks.Local
                    from j in db.users_tasks.Where(u => u.idusers == User.iduser)
                    where j.idtasks == i.idtask
                    select i;

            lb.ItemsSource = new ObservableCollection<tasks>(b.ToList());
            lb.UpdateLayout();
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb.SelectedItems.Count > 0)
            {
                btnCompl.IsEnabled = true;
            }
            else
            {
                btnCompl.IsEnabled = false;
            }
        }
    }
}
