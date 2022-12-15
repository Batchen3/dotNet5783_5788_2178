using BlImplementation;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public OrderListWindow()
        {
            InitializeComponent();
            OrdersListview.ItemsSource = bl.Order.GetOrders();
        }

        private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var order = (BO.OrderForList)(sender as ListView).SelectedItem;
            try
            {
                BO.Order selectedItem = bl.Order.GetDetailsOfOrder(order.ID);
                OrderWindow orderWindow = new OrderWindow(selectedItem);
                orderWindow.ShowDialog();
                OrdersListview.ItemsSource = bl.Order.GetOrders();
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

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
