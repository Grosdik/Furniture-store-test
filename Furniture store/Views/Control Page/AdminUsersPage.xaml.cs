using Furniture_store.Database;
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
using Furniture_store.Utils;
using Furniture_store.Views.AddEditPage;

namespace Furniture_store.Views.Control_Page
{
    /// <summary>
    /// Логика взаимодействия для AdminUsersPage.xaml
    /// </summary>
    public partial class AdminUsersPage : Page
    {
        public AdminUsersPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Furniture_storeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dataGridUsers.ItemsSource = Furniture_storeEntities.GetContext().Users.ToList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AdminAddEditUserPage((sender as Button).DataContext as Users));
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = dataGridUsers.SelectedItems.Cast<Users>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {usersForRemoving.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Furniture_storeEntities.GetContext().Users.RemoveRange(usersForRemoving);
                    Furniture_storeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dataGridUsers.ItemsSource = Furniture_storeEntities.GetContext().Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new AdminAddEditUserPage(null));
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGridUsers.ItemsSource = Furniture_storeEntities.GetContext().Users.ToList().Where(p => p.LastName.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }
    }
}
