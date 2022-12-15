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
        public ProductListWindow(string state)
        {
            InitializeComponent();
            if(state=="admin")
               ProductsListview.ItemsSource = bl.Product.GetAll();
            if (state == "newOrder")
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
            var product = (BO.ProductForList)(sender as ListView).SelectedItem;
            try
            {
                BO.Product selectedItem=bl.Product.Get(product.ID);
                ProductWindow productWindow = new ProductWindow(selectedItem);
                productWindow.ShowDialog();
                ProductsListview.ItemsSource = bl.Product.GetAll();
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
            ProductsListview.ItemsSource = bl.Product.GetByCategory((BO.ECategory)CategorySelector.SelectedItem);
        }
    }
}
