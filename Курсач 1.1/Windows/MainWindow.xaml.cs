using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Курсач_1._1.ViewModel;
using System.Windows;
using System.Windows.Input;
using static Курсач_1._1.MainWindow;
using Курсач_1._1.View;
using Курсач_1._1.Windows;
using System.Data.Entity.Migrations;

namespace Курсач_1._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindowsCodeBehind
    {
        Model1 db = new Model1();
        private users user;
        public interface IMainWindowsCodeBehind
        {
            void LoadView(ViewType typeView, tasks task = null);
            void LoadView(ViewType editUser, users selectedPhone);
            void LoadView(ViewType first, IEnumerable<users> selectedItems);
            users GetThisUser();
        }

        public enum ViewType
        {
            Main,
            First,
            Second,
            TaskToLower,
            AddNewUser,
            EditUser
        }

        public MainWindow(users user)
        {
            db.roles.Load();
            db.importance.Load();
            db.tasks.Load();
            db.users_tasks.Load();
            db.status.Load();
            db.changedtasks.Load();
            this.user = user;
            Style = (Style)FindResource(typeof(Window));
            InitializeComponent();
            CheckTasksStatus(user);
            if (user.roles.name == "Admin")
            {
                admTab.Visibility = Visibility.Visible;
            }

            lablog.Content = user.login;
            labrole.Content = user.roles.name;

            this.Loaded += MainWindow_Loaded;
            ShowNotification(user);
        }

        public void ShowNotification(users user)
        {
            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                Notification alarm = new Notification(user.changedtasks);
                alarm.Show();
                await Task.Delay(15000);
                alarm.Close();
                CleanChangeTasks(user);
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка вьюмодел для кнопок меню
            MenuViewModel vm = new MenuViewModel();
            //даем доступ к этому кодбихайнд
            vm.CodeBehind = this;
            //делаем эту вьюмодел контекстом данных
            this.DataContext = vm;
            //загрузка стартовой View
            LoadView(ViewType.Main);
        }


        public void LoadView(ViewType typeView, tasks task = null)
        {
            switch (typeView)
            {
                case ViewType.Main:
                    //загружаем вьюшку, ее вьюмодель
                    MainUC view = new MainUC(user);
                    ApplicationViewModel vm = new ApplicationViewModel(this, user);
                    //связываем их м/собой
                    view.DataContext = vm;
                    //отображаем
                    this.OutputView.Content = view;
                    break;
                case ViewType.First:
                    AddTask viewF = new AddTask(this, user);
                    this.OutputView.Content = viewF;
                    break;
                case ViewType.Second:
                    if (task != null)
                    {
                        EditTask viewS = new EditTask(task, this);
                        this.OutputView.Content = viewS;
                    }
                    break;
                case ViewType.TaskToLower:
                    TaskToLower taskToLower = new TaskToLower(this, user);
                    TaskToLowerViewModel taskToLowerViewModel = new TaskToLowerViewModel(this, user);
                    taskToLower.DataContext = taskToLowerViewModel;
                    this.OutputView.Content = taskToLower;
                    break;
                case ViewType.AddNewUser:
                    {
                        AddNewUser addNewUser = new AddNewUser(this, user);
                        this.OutputView.Content = addNewUser;
                        break;
                    }

            }
        }

        public void LoadView(ViewType typeView, users user)
        {
            switch (typeView)
            {
                case ViewType.EditUser:
                    {
                        EditUser editUser = new EditUser(this, user);
                        this.OutputView.Content = editUser;
                        break;
                    }
            }
        }

        public void LoadView(ViewType typeView, IEnumerable<users> users)
        {
            switch (typeView)
            {
                case ViewType.First:
                    {
                        AddTask addTask = new AddTask(this, (List<users>)users);
                        AddTaskForUserViewModel addTaskForUserViewModel = new AddTaskForUserViewModel(this);
                        addTask.DataContext = addTaskForUserViewModel;
                        this.OutputView.Content = addTask;
                        break;
                    }
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void ExitItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AcceptExit acceptExit = new AcceptExit("Are you sure you want to get out??");
            acceptExit.Owner = this;
            if (acceptExit.ShowDialog() == true)
            {
                Auth auth = new Auth();
                this.Close();
                auth.ShowDialog();
            }
            else
            {
                tabcontrol.SelectedItem = tabtasks;
            }
        }

        private void EditLogin_Click(object sender, RoutedEventArgs e)
        {
            EditLoginUser acceptExit = new EditLoginUser(user);
            acceptExit.Owner = this;
            if (acceptExit.ShowDialog() == true)
            {
                this.ShowMessage("Успешно изменено");
            }
             lablog.Content = user.login;
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            EditPasswordUser editPasswordUser = new EditPasswordUser(user);
            editPasswordUser.Owner = this;
            if (editPasswordUser.ShowDialog() == true)
            {
                this.ShowMessage("Successfully modified");
            }
        }

        private void ChangeRoleMain_Click(object sender, RoutedEventArgs e)
        {
            EditRoles editRoles = new EditRoles(user);
            editRoles.Owner = this;
            if (this.WindowState == WindowState.Maximized)
            {
                editRoles.WindowState = WindowState.Maximized;
            }
            this.Hide();
            editRoles.ShowDialog();
            if (editRoles.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.Left = editRoles.Left;
                this.Top = editRoles.Top;
                this.Height = editRoles.Height;
                this.Width = editRoles.Width;
            }
        }

        public void CheckTasksStatus(users user)
        {
            user.changedtasks.misseddeadline = 0;
            var overdueTasks = db.users_tasks.Local.Where(i => i.tasks.status != 2 && i.users.iduser == user.iduser).Select(r=>r.tasks);
            foreach (var item in overdueTasks)
            {
                if (item.deadline < DateTime.Now)
                {
                    item.status = 3;
                    ++user.changedtasks.misseddeadline;
                }
            }
            db.SaveChanges();
        }

        public void CleanChangeTasks(users user)
        {
            user.changedtasks.addedtasks = 0;
            db.changedtasks.AddOrUpdate(user.changedtasks);
            db.SaveChanges();
        }

        public users GetThisUser()
        {
            return user;
        }
    }
}

