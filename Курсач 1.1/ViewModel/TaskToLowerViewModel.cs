using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Курсач_1._1.Relay;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1.ViewModel
{
    public class TaskToLowerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged1 = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        private users selectedUser;
        public static ObservableCollection<users> Userses { get; set; }
        Model1 db = new Model1();

        public TaskToLowerViewModel(IMainWindowsCodeBehind codeBehind, users user)
        {
            db.users.Load();
            var b = db.users.Local.Where(i => i.roles.priority < user.roles.priority);
            Userses = new ObservableCollection<users>(b.ToList());
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public users SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedPhone");
                db.SaveChanges();
            }
        }

        private RelayCommand1 _LoadAddNewUserCommand;
        public RelayCommand1 LoadAddNewUserCommand
        {
            get
            {
                return _LoadAddNewUserCommand = _LoadAddNewUserCommand ??
                  new RelayCommand1(OnLoadAddNewUserUC, CanLoadAddNewUserUC);
            }
        }
        private bool CanLoadAddNewUserUC()
        {
            return true;
        }
        private void OnLoadAddNewUserUC()
        {
            _MainCodeBehind.LoadView(ViewType.AddNewUser);
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      users user = obj as users;
                      if (user != null)
                      {
                          Userses.Remove(user);
                          db.users.Remove(user);
                          db.SaveChanges();
                      }
                  },
                 (obj) => Userses.Count > 0));
            }
        }

        private RelayCommand1 loadEditUserCommand;
        public RelayCommand1 LoadEditUserCommand
        {
            get
            {
                return loadEditUserCommand = loadEditUserCommand ??
                  new RelayCommand1(OnLoadEditTask, CanLoadEditTask);
            }
        }
        private bool CanLoadEditTask()
        {
            return true;
        }
        private void OnLoadEditTask()
        {
            _MainCodeBehind.LoadView(ViewType.EditUser, selectedUser);
        }

        private RelayCommand1 _LoadMainUCCommand;
        public RelayCommand1 LoadMainUCCommand
        {
            get
            {
                return _LoadMainUCCommand = _LoadMainUCCommand ??
                  new RelayCommand1(OnLoadMainUC, CanLoadMainUC);
            }
        }
        private bool CanLoadMainUC()
        {
            return true;
        }
        private void OnLoadMainUC()
        {
            _MainCodeBehind.LoadView(ViewType.Main);
        }

    }
}
