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

namespace Furniture_store.Views.Control_Page
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dataGridHistory.ItemsSource = Furniture_storeEntities.GetContext().AuthHistory.ToList();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AdminUsersPage());       
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
