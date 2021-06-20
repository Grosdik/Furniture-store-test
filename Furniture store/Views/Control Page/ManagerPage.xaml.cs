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
using Word = Microsoft.Office.Interop.Word;

namespace Furniture_store.Views.Control_Page
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private Word.Document document = null;
        private List<Clients> Clients;
        public ManagerPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
                if (Visibility == Visibility.Visible)
                {
                    Furniture_storeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dataGridClients.ItemsSource = Furniture_storeEntities.GetContext().Clients.ToList();
                }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerAddEditClientPage((sender as Button).DataContext as Clients));
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerAddEditClientPage(null));
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var clientForRemoving = dataGridClients.SelectedItems.Cast<Clients>().ToList();

            if(MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Furniture_storeEntities.GetContext().Clients.RemoveRange(clientForRemoving);
                    Furniture_storeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dataGridClients.ItemsSource = Furniture_storeEntities.GetContext().Clients.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGridClients.ItemsSource = Furniture_storeEntities.GetContext().Clients.ToList().Where(p => p.Surname.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerProductPage());
        }
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var rows = dataGridClients.ItemsSource.Cast<Clients>().ToList();
            if (rows.Count == 0)
            {
                MessageBox.Show("Нет выбранных книг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var app = new Word.Application();
            document = app.Documents.Add();
            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table table = document.Tables.Add(tableRange, rows.Count + 1, 6);
            table.Borders.InsideLineStyle = table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;
            cellRange = table.Cell(1, 1).Range;
            cellRange.Text = "№";
            cellRange = table.Cell(1, 2).Range;
            cellRange.Text = "Имя клиента";
            cellRange = table.Cell(1, 3).Range;
            cellRange.Text = "Фамилия клиента";
            cellRange = table.Cell(1, 4).Range;
            cellRange.Text = "Показания холодной воды";
            cellRange = table.Cell(1, 5).Range;
            cellRange.Text = "Показания горячей воды";
            cellRange = table.Cell(1, 6).Range;
            cellRange.Text = "Дата";

            table.Rows[1].Range.Bold = 1;
            int currentRow = 1;
            foreach (var row in rows)
            {
                currentRow++;
                cellRange = table.Cell(currentRow, 1).Range;
                cellRange.Text = row.Id.ToString();

                cellRange = table.Cell(currentRow, 2).Range;
                cellRange.Text = row.Name;

                cellRange = table.Cell(currentRow, 3).Range;
                cellRange.Text = row.Surname;

                cellRange = table.Cell(currentRow, 4).Range;
                cellRange.Text = row.Patronymic.ToString();

                cellRange = table.Cell(currentRow, 5).Range;
                cellRange.Text = row.City.ToString();

                cellRange = table.Cell(currentRow, 6).Range;
                cellRange.Text = row.Address.ToString();

                cellRange = table.Cell(currentRow, 7).Range;
                cellRange.Text = row.Phone.ToString();
            }

            document.Paragraphs.Add();
            Word.Paragraph sum = document.Paragraphs.Add();
            sum.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            Word.Range sumRange = sum.Range;
            sumRange.Bold = 1;
        }
    }
}
