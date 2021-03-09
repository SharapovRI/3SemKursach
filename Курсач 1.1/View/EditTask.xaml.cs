using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1.View
{
    public partial class EditTask : UserControl
    {
        Model1 db = new Model1();
        private List<string> hierList;
        private List<string> statusList;
        private IMainWindowsCodeBehind _MainCodeBehind;
        tasks Task;

        public EditTask(tasks task, IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            db.importance.Load();
            db.status.Load();
            db.tasks.Load();
            hierList = (from i in db.importance.Local
                        select i.importancetext).ToList();
            statusList = (from i in db.status.Local
                          select i.statustext).ToList();

            InitializeComponent();
            dp.SelectedDate = task.deadline.Value;
            cbHeir.ItemsSource = hierList;
            cbStatus.ItemsSource = statusList;

            var listhier = db.importance.Local.Where(r => r.idimportance == task.importance).Select(i => i.importancetext);
            var liststatus = db.status.Local.Where(r => r.idstatus == task.status).Select(i => i.statustext);
            cbHeir.SelectedItem = listhier.FirstOrDefault();
            cbStatus.SelectedItem = liststatus.FirstOrDefault();

            _MainCodeBehind = codeBehind;
            EditedHeader.Text = task.header;
            EditedTask.Text = task.text;
            Task = task;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditedHeader.Text))
            {
                headerIm.Visibility = Visibility.Visible;
            }
            else
            {
                headerIm.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(EditedTask.Text))
            {
                textIm.Visibility = Visibility.Visible;
            }
            else
            {
                textIm.Visibility = Visibility.Hidden;
            }

            if (dp.SelectedDate == null)
            {
                dateIm.Visibility = Visibility.Visible;
            }
            else
            {
                dateIm.Visibility = Visibility.Hidden;
            }

            if (!string.IsNullOrWhiteSpace(EditedTask.Text) && cbHeir.SelectedItem != null && cbStatus.SelectedItem != null && dp.SelectedDate != null && !string.IsNullOrWhiteSpace(EditedHeader.Text))
            {
                this.Task.header = EditedHeader.Text;
                this.Task.text = EditedTask.Text;
                this.Task.importance = cbHeir.SelectedIndex;
                this.Task.status = cbStatus.SelectedIndex;
                this.Task.deadline = dp.SelectedDate;

                try
                {
                    db.tasks.AddOrUpdate(Task);
                    db.SaveChanges();
                    _MainCodeBehind.LoadView(ViewType.Main);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter correct values");
                }
            }
        }

        private void EditedTask_KeyUp(object sender, KeyEventArgs e)
        {
            if (EditedTask.Text.Length <= 0)
            {
                SaveBut.IsEnabled = false;
            }
            else
            {
                SaveBut.IsEnabled = true;
            }
        }
    }
}
