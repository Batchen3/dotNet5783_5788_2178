using BlImplementation;
using BO;
using DO;
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
        string? state = null;
        public OrderListWindow(string? from = null)
        {
            InitializeComponent();
            state = from;
            if (state == null)
                OrdersListview.ItemsSource = bl.Order.GetOrders();
            if (state == "orderTracking")
            {
                IEnumerable<BO.OrderForList> allOrders = bl.Order.GetOrders();
                List<BO.OrderTracking> orderTrackings = new List<BO.OrderTracking>();
                foreach (BO.OrderForList order in allOrders)
                {
                    orderTrackings.Add(bl.Order.OrderTracking(order.ID));
                }
                OrdersListview.ItemsSource = orderTrackings;

            }
        }

        private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderTracking? orderTracking = null;
            BO.OrderForList? orderForList = null;
            BO.Order? selectedItem=null;
            
            try
            {
                if (state == "orderTracking")
                {
                    orderTracking = (BO.OrderTracking)(sender as ListView).SelectedItem;
                    selectedItem = bl.Order.GetDetailsOfOrder(orderTracking.ID);
                    OrderWindow orderWindow = new OrderWindow(selectedItem,"orderTracking");
                    orderWindow.Show();
                }
                if (state == null)
                {
                    orderForList = (BO.OrderForList)(sender as ListView).SelectedItem;
                    selectedItem = bl.Order.GetDetailsOfOrder(orderForList.ID);
                    OrderWindow orderWindow = new OrderWindow(selectedItem);
                    orderWindow.Show();

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

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
}
