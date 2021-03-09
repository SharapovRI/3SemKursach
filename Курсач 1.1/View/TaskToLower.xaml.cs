using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1.View
{
    /// <summary>
    /// Логика взаимодействия для TaskToLower.xaml
    /// </summary>
    public partial class TaskToLower : UserControl 
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public TaskToLower(IMainWindowsCodeBehind codeBehind, users user)
        {
            InitializeComponent();
            _MainCodeBehind = codeBehind;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbUsers.SelectedItems.Count != 1)
            {
                bnEdit.IsEnabled = false;
                btDel.IsEnabled = false;
            }
            else
            {
                bnEdit.IsEnabled = true;
                btDel.IsEnabled = true;
            }

            if (lbUsers.SelectedItems.Count == 0)
            {
                bnAddTask.IsEnabled = false;
            }
            else            {

                bnAddTask.IsEnabled = true;
            }
        }

        private void bnAddTask_Click(object sender, RoutedEventArgs e)
        {
            List<users> SelectedItemsList = lbUsers.SelectedItems.Cast<users>().ToList();
            _MainCodeBehind.LoadView(ViewType.First, SelectedItemsList);
        }
    }
}
