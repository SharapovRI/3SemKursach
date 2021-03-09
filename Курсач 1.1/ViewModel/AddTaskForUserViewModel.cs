using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Курсач_1._1.Relay;
using static Курсач_1._1.MainWindow;

namespace Курсач_1._1.ViewModel
{
    class AddTaskForUserViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public AddTaskForUserViewModel(IMainWindowsCodeBehind codeBehind)
        {            
            _MainCodeBehind = codeBehind;
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
            _MainCodeBehind.LoadView(ViewType.TaskToLower);
        }
    }
}
