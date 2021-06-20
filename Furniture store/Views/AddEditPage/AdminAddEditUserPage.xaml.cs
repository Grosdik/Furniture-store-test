using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Furniture_store.Database;
using Furniture_store.Utils;

namespace Furniture_store.Views.AddEditPage
{
    /// <summary>
    /// Логика взаимодействия для AdminAddEditUserPage.xaml
    /// </summary>
    public partial class AdminAddEditUserPage : Page
    {
        private Users _currentUser = new Users();
        public AdminAddEditUserPage(Users selectedUsers)
        {
            InitializeComponent();
            ComboRoles.ItemsSource = Furniture_storeEntities.GetContext().Roles.ToList();
            if (selectedUsers != null)
            {
                _currentUser = selectedUsers;
            }
            DataContext = _currentUser;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.FirstName))
            {
                errors.AppendLine("Укажите имя пользователя!");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.LastName))
            {
                errors.AppendLine("Укажите фамилию пользователя!");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.Login))
            {
                errors.AppendLine("Укажите логин пользователя!");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
            {
                errors.AppendLine("Укажите пароль пользователя!");
            }
            if (_currentUser.Roles == null)
            {
                errors.AppendLine("Укажите роль пользователя!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUser.RoleId == 0)
            {
                Furniture_storeEntities.GetContext().Users.Add(_currentUser);
            }

            try
            {
                Furniture_storeEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Transfer.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
