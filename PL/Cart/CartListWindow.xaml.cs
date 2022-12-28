using BlApi;
using BO;
using PL.Product;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartListWindow.xaml
    /// </summary>
    public partial class CartListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public CartListWindow()
        {
            InitializeComponent();
            CartListview.ItemsSource = ProductListWindow.cart.Items;
        }

        private void CartListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
            BO.OrderItem orderItem = (BO.OrderItem)(sender as ListView).SelectedItem;
            CartWindow cartWindow =new CartWindow(orderItem); 
            cartWindow.Show();
        }

        private void CartListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSaveCart_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.SaveCart(ProductListWindow.cart);

        }
    }
}
