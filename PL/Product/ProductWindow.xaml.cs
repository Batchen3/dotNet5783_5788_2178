using BO;
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
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BO.Product product = new BO.Product();
        BlApi.IBl blp;
        string status;
        public ProductWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            blp = bl;
            status = "add";
            cbCategory.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
        }
        public ProductWindow(BlApi.IBl bl, BO.Product selectedProduct)
        {
            InitializeComponent();
            blp = bl;
            status = "update";
            cbCategory.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
            txtId.Text = selectedProduct.ID.ToString();
            txtId.IsEnabled = false;
            txtName.Text = selectedProduct.Name;
            txtPrice.Text = selectedProduct.Price.ToString();
            txtInstock.Text = selectedProduct.InStock.ToString();
            cbCategory.Text = selectedProduct.Category.ToString();
            if (selectedProduct.Parve == 0)
            {
                cbParve.IsChecked = true;
                cbDairy.IsChecked = false;
            }
            else
            {
                cbParve.IsChecked = false;
                cbDairy.IsChecked = true;
            }
            product = selectedProduct;
        }

        private void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveAdding_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (status == "add")
                {
                    blp.Product.Add(product);
                }
                else
                {
                    blp.Product.Update(product);
                }
                Close();
            }

            catch (DalException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? id = txtId.Text;
            int.TryParse(id, out int idInt);
            product.ID = idInt;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = txtName.Text;
            product.Name = name;
        }

        private void txtPrice_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string? price = txtPrice.Text;
            double.TryParse(price, out double priceInt);
            product.Price = priceInt;
        }

        private void txtInstock_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? instock = txtInstock.Text;
            int.TryParse(instock, out int instockInt);
            product.InStock = instockInt;
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product.Category = (BO.ECategory)cbCategory.SelectedItem;
        }

        private void cbParve_Checked(object sender, RoutedEventArgs e)
        {
            product.Parve = 1;
        }

        private void cbDairy_Checked(object sender, RoutedEventArgs e)
        {
            product.Parve = 0;
        }
    }
}
