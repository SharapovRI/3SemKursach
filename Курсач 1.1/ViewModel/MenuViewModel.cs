using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач_1._1.Relay;
using static Курсач_1._1.MainWindow;
using static Курсач_1._1.Windows.EditRoles;

namespace Курсач_1._1.ViewModel
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {

        }

        public IMainWindowsCodeBehind CodeBehind { get; set; }
        public IRoleWindowsCodeBehind roleWindowsCodeBehind { get; set; }

        private RelayCommand1 loadAddTaskCommand;
        public RelayCommand1 LoadAddTaskCommand
        {
            get
            {
                return loadAddTaskCommand = loadAddTaskCommand ??
                  new RelayCommand1(OnLoadFirstUC, CanLoadFirstUC);
            }
        }
        private bool CanLoadFirstUC()
        {
            return true;
        }
        private void OnLoadFirstUC()
        {
            CodeBehind.LoadView(ViewType.First);
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
            CodeBehind.LoadView(ViewType.Main);
        }

        private RelayCommand1 backCommand;
        public RelayCommand1 BackCommand
        {
            get
            {
                return backCommand = backCommand ??
                  new RelayCommand1(OnBackCommand, CanBackCommand);
            }
        }
        private bool CanBackCommand()
        {
            return true;
        }
        private void OnBackCommand()
        {
            roleWindowsCodeBehind.Back();
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
            CodeBehind.LoadView(ViewType.TaskToLower);
        }
    }
}