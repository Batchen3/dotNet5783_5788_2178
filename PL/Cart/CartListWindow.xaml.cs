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
            DataContext= ICart.cart.Items;
            //CartListview.ItemsSource = ICart.cart.Items;
        }

        private void CartListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)CartListview.SelectedItem;
            CartWindow cartWindow =new CartWindow(orderItem);
            cartWindow.ShowDialog();
            CartListview.Items.Refresh();
        }

        private void btnSaveCart_Click(object sender, RoutedEventArgs e)
        {
            Customer.CustomerWindow customerWindow=new Customer.CustomerWindow();
            customerWindow.Show();
            Close();
        }
    }
}
