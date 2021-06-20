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
    /// Логика взаимодействия для ManagerAddEditProductPage.xaml
    /// </summary>
    public partial class ManagerAddEditProductPage : Page
    {
        private Products _currentProduct = new Products();
        public ManagerAddEditProductPage(Products selectedProduct)
        {
            InitializeComponent();
            ComboSupplier.ItemsSource = Furniture_storeEntities.GetContext().Suppliers.ToList();
            if (selectedProduct != null)
            {
                _currentProduct = selectedProduct;
            }
            DataContext = _currentProduct;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentProduct.Name))
            {
                errors.AppendLine("Укажите название товара!");
            }
            if (_currentProduct.Cost < 1)
            {
                errors.AppendLine("Укажите стоимость товара!");
            }
            if (_currentProduct.Cost < 1)
            {
                errors.AppendLine("Укажите количество товара!");
            }
            if (_currentProduct.Suppliers == null)
            {
                errors.AppendLine("Укажите поставщика!");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.TypeOfFurniture))
            {
                errors.AppendLine("Укажите тип фурнитуры!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProduct.Id == 0)
            {
                Furniture_storeEntities.GetContext().Products.Add(_currentProduct);
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
