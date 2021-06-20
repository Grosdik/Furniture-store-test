using Furniture_store.Database;
using Furniture_store.Utils;
using Furniture_store.Views.AddEditPage;
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
    /// Логика взаимодействия для ManagerProductPage.xaml
    /// </summary>
    public partial class ManagerProductPage : Page
    {
        private List<Products> Products;
        public ManagerProductPage()
        {
            InitializeComponent();
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGridProducts.ItemsSource = Furniture_storeEntities.GetContext().Products.ToList().Where(p => p.Name.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerAddEditProductPage((sender as Button).DataContext as Products));
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerAddEditProductPage(null));
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var productForRemoving = dataGridProducts.SelectedItems.Cast<Products>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {productForRemoving.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Furniture_storeEntities.GetContext().Products.RemoveRange(productForRemoving);
                    Furniture_storeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dataGridProducts.ItemsSource = Furniture_storeEntities.GetContext().Products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Furniture_storeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dataGridProducts.ItemsSource = Furniture_storeEntities.GetContext().Products.ToList();
            }
        }
    }
}
