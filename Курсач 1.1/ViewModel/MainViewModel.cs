using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Курсач_1._1.Relay;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged1 = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        private tasks selectedtask;
        public static ObservableCollection<tasks> Tasks { get; set; }
        Model1 db = new Model1();

        public tasks SelectedTask
        {
            get { return selectedtask; }
            set
            {
                selectedtask = value;
                OnPropertyChanged("SelectedTask");
                db.SaveChanges();
            }
        }

        public ApplicationViewModel(IMainWindowsCodeBehind codeBehind, users user)
        {
            db.tasks.Load();
            //var a = db.users_tasks.Where(u => u.idusers == user.iduser);
            var tasksofuser = from i in db.tasks.Local
                    from j in db.users_tasks.Where(u => u.idusers == user.iduser)
                    where j.idtasks == i.idtask
                    select i;
            var orderedList = tasksofuser.OrderBy(i => i.deadline);
            Tasks = new ObservableCollection<tasks>(orderedList.ToList());
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand1 loadFirstUCCommand;
        public RelayCommand1 LoadFirstUCCommand
        {
            get
            {
                return loadFirstUCCommand = loadFirstUCCommand ??
                  new RelayCommand1(OnShowMessage, CanShowMessage);
            }
        }
        private bool CanShowMessage()
        {
            return true;
        }
        private void OnShowMessage()
        {
            _MainCodeBehind.LoadView(ViewType.First);
        }

        private RelayCommand1 loadEditTaskCommand;
        public RelayCommand1 LoadEditTaskCommand
        {
            get
            {
                return loadEditTaskCommand = loadEditTaskCommand ??
                  new RelayCommand1(OnLoadEditTask, CanLoadEditTask);
            }
        }
        private bool CanLoadEditTask()
        {
            return true;
        }
        private void OnLoadEditTask()
        {
            _MainCodeBehind.LoadView(ViewType.Second, selectedtask);
        }

        private RelayCommand1 _LoadTaskToLowerCommand;
        public RelayCommand1 LoadTaskToLowerCommand
        {
            get
            {
                return _LoadTaskToLowerCommand = _LoadTaskToLowerCommand ??
                  new RelayCommand1(OnLoadTaskToLower, CanLoadTaskToLower);
            }
        }
        private bool CanLoadTaskToLower()
        {
            return true;
        }
        private void OnLoadTaskToLower()
        {
            _MainCodeBehind.LoadView(ViewType.TaskToLower);
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      tasks phone = obj as tasks;
                      if (phone != null)
                      {
                          Tasks.Remove(phone);
                          db.tasks.Remove(phone);
                          db.SaveChanges();
                      }
                  },
                 (obj) => Tasks.Count > 0));
            }
        }
    }
}
