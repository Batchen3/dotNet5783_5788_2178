using PL.Product;
using PL.Order;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
            btnListProduct.Visibility = Visibility.Hidden;
            btnListOrder.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow product = new ProductListWindow("admin");
            product.Show();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            btnListProduct.Visibility = Visibility.Visible;
            btnListOrder.Visibility = Visibility.Visible;
            //btnAdmin.Visibility = Visibility.Hidden;
            //btnNewOrder.Visibility=Visibility.Hidden;
            //btnOrderTracking.Visibility=Visibility.Hidden;
        }

        private void btnListOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow order = new OrderListWindow();
            order.Show();
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow product = new ProductListWindow("newOrder");
            product.Show();
        }

        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow order = new OrderListWindow("orderTracking");
            order.Show();
        }
    }
}
