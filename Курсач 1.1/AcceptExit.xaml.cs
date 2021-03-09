using System.Windows;

namespace Курсач_1._1
{
    public partial class AcceptExit : Window
    {
        public AcceptExit(string quest)
        {
            InitializeComponent();
            tb.Text = quest;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
