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
        BlApi.IBl bl = BlApi.Factory.Get();
        int debily = 0;
        string? state = null;
        static BO.Cart cart = new BO.Cart {CustomerName="aaa",CustomerEmail="aaa@gmail.com",CustomerAddress="aaa", Items = new List<BO.OrderItem?>() };

        public ProductListWindow(string? from = null)
        {
            state = from;
            InitializeComponent();
            if (from == "admin")
            {
                ProductsListview.ItemsSource = bl.Product.GetAll();
                btnGoToCart.Visibility = Visibility.Hidden;
            }

            if (from == "newOrder")
            {
                ProductsListview.ItemsSource = bl.Product.GetCatalog();
                btnAddProduct.Visibility = Visibility.Hidden;
            }
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.ECategory));
            debily = ProductsListview.Items.Count;

        }

        //private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ProductsListview.ItemsSource = bl.Product.GetByCategory((BO.ECategory)CategorySelector.SelectedItem);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow product = new ProductWindow();
            product.ShowDialog();
            ProductsListview.ItemsSource = bl.Product.GetAll();
            debily = ProductsListview.Items.Count;
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList? productForList = null;
            BO.ProductItem? productItem = null;
            BO.Product? selectedItem = null;
            try
            {
                if (state == "admin")
                {
                    productForList = (BO.ProductForList)(sender as ListView).SelectedItem;
                    selectedItem = bl.Product.Get(productForList.ID);
                    ProductWindow productWindow = new ProductWindow(selectedItem, "update");
                    productWindow.ShowDialog();
                    ProductsListview.ItemsSource = bl.Product.GetAll();
                }
                if (state == "newOrder")
                {
                    productItem = (BO.ProductItem)(sender as ListView).SelectedItem;
                    selectedItem = bl.Product.Get(productItem.ID);
                    ProductWindow productWindow = new ProductWindow(selectedItem, "display", cart);
                    productWindow.ShowDialog();

                    ProductsListview.ItemsSource = bl.Product.GetCatalog();
                }
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
            if (state == "admin")
                ProductsListview.ItemsSource = bl.Product.GetByCategoryAdmin((BO.ECategory)CategorySelector.SelectedItem);
            if (state == "newOrder")
                ProductsListview.ItemsSource = bl.Product.GetByCategoryForOrder((BO.ECategory)CategorySelector.SelectedItem);
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            Cart.CartListWindow cartListWindow = new Cart.CartListWindow(cart);
            cartListWindow.ShowDialog();
            ProductsListview.ItemsSource = bl.Product.GetCatalog();
        }
    }
}
