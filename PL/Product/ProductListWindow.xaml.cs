using BlApi;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl bl = new BlImplementation.Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductsListview.ItemsSource = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
        }

        //private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ProductsListview.ItemsSource = bl.Product.GetByCategory((BO.ECategory)CategorySelector.SelectedItem);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow product = new ProductWindow(bl);
            product.Show();
            Close();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (BO.ProductForList)(sender as ListView).SelectedItem;
            try
            {
                BO.Product selectedItem=bl.Product.Get(product.ID);
                ProductWindow productWindow = new ProductWindow(bl, selectedItem);
                productWindow.Show();
                Close();
            }
            catch (BO.DalException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            ProductsListview.ItemsSource = bl.Product.GetAll();
        }

        private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ProductsListview.ItemsSource = bl.Product.GetAll((item => item.Category==(DO.ECategory)CategorySelector.SelectedItem));
        }
    }
}
