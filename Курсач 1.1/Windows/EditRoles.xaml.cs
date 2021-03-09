using System.Windows;
using Курсач_1._1.RoleView;
using static Курсач_1._1.Windows.EditRoles;

namespace Курсач_1._1.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditRoles.xaml
    /// </summary>
    public partial class EditRoles : Window, IRoleWindowsCodeBehind
    {
        public interface IRoleWindowsCodeBehind
        {
            void LoadRoleWindowView(ViewTypeRole viewTypeRole, roles role = null);
            void Back();
        }

        public enum ViewTypeRole
        {
            RoleMain,
            RoleEdit,
            RoleAdd
        }

        public users User;

        public EditRoles(users user)
        {
            User = user;
            Style = (Style)FindResource(typeof(Window));
            this.Title = "Editing roles";
            InitializeComponent();
            roleChageView.Content = new Roles(this);
        }

        public void LoadRoleWindowView(ViewTypeRole viewTypeRole, roles role = null)
        {
            switch (viewTypeRole)
            {
                case ViewTypeRole.RoleMain:
                    Roles roles = new Roles(this);
                    this.Title = "Editing roles";
                    this.roleChageView.Content = roles;
                    break;
                case ViewTypeRole.RoleEdit:
                    RoleEdit roleEdit = new RoleEdit(this, role);
                    this.Title = "Editing role";
                    this.roleChageView.Content = roleEdit;
                    break;
                case ViewTypeRole.RoleAdd:
                    RoleAdd roleAdd = new RoleAdd(this, User.roles);
                    this.Title = "Adding new role";
                    this.roleChageView.Content = roleAdd;
                    break;
                default:
                    break;
            }
        }

        public void Back()
        {
            Owner.Visibility = Visibility.Visible;
            this.DialogResult = false;
        }
    }
}
