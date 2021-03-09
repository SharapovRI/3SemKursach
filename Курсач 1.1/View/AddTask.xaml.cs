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
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class AddTask : UserControl
    {
        Model1 db = new Model1();
        private List<string> hierList;
        private IMainWindowsCodeBehind _MainCodeBehind;
        users user;
        List<users> Users;

        public AddTask(IMainWindowsCodeBehind codeBehind, users user)
        {
            this.user = user;
            InitializeComponent();
            PostInitialize(codeBehind);
        }

        public AddTask(IMainWindowsCodeBehind codeBehind, List<users> users)
        {
            this.Users = users;
            InitializeComponent();
            PostInitialize(codeBehind);
        }

        public void PostInitialize(IMainWindowsCodeBehind codeBehind)
        {
            db.importance.Load();
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            hierList = (from i in db.importance.Local
                        select i.importancetext).ToList();

            _MainCodeBehind = codeBehind;

            cbHier.ItemsSource = hierList;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHeader.Text))
            {
                headerIm.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                headerIm.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrWhiteSpace(AddedTask.Text))
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

            if (!string.IsNullOrWhiteSpace(AddedTask.Text) && dp.SelectedDate != null && !string.IsNullOrWhiteSpace(tbHeader.Text))
            {
                if (Users is null)
                {
                    AddNewTask(user);
                }
                else
                {
                    foreach (var item in Users)
                    {
                        AddNewTask(item);
                        ++item.changedtasks.addedtasks;
                        db.changedtasks.AddOrUpdate(item.changedtasks);
                        db.SaveChanges();
                    }
                }

                _MainCodeBehind.LoadView(ViewType.Main);
            }
        }

        private void AddNewTask(users user)
        {
            tasks task = new tasks(tbHeader.Text, AddedTask.Text, dp.SelectedDate, cbHier.SelectedIndex);

            try
            {
                db.tasks.Add(task);
                db.users_tasks.Add(new users_tasks(user.iduser));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter correct values");
            }
        }
    }
}
