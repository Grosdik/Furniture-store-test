using Furniture_store.Database;
using Furniture_store.Utils;
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
using Furniture_store.Views;

namespace Furniture_store.Views.AddEditPage
{
    /// <summary>
    /// Логика взаимодействия для ClientEditPage.xaml
    /// </summary>
    public partial class ManagerAddEditClientPage : Page
    {
        private Clients _currentClient = new Clients();
        public ManagerAddEditClientPage(Clients selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null)
            {
                _currentClient = selectedClient;
            }
            DataContext = _currentClient;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentClient.Name))
            {
                errors.AppendLine("Укажите имя клиента!");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.Surname))
            {
                errors.AppendLine("Укажите фамилию клиента!");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.Patronymic))
            {
                errors.AppendLine("Укажите отчество клиента!");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.City))
            {
                errors.AppendLine("Укажите город клиента!");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.Address))
            {
                errors.AppendLine("Укажите адресс клиента!");
            }
            if (string.IsNullOrWhiteSpace(_currentClient.Phone))
            {
                errors.AppendLine("Укажите номер телефона клиента!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentClient.Id == 0)
            {
                Furniture_storeEntities.GetContext().Clients.Add(_currentClient);
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