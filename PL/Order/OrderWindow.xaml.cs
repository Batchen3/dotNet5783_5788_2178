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
using System.Xml.Linq;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        public OrderWindow(BO.Order selectedOrder, string? status = null)
        {
            InitializeComponent();
            txtId.Text = selectedOrder.ID.ToString();
            txtId.IsEnabled = false;
            txtCustomerName.Text=selectedOrder.CustomerName;
            txtCustomerName.IsEnabled = false;
            txtCustomerAddress.Text=selectedOrder.CustomerAddress;
            txtCustomerAddress.IsEnabled = false;
            txtCustomerEmail.Text=selectedOrder.CustomerEmail;
            txtCustomerEmail.IsEnabled = false;
            txtOrderStatus.Text = selectedOrder.OrderStatus.ToString();
            txtOrderStatus.IsEnabled = false;
            txtOrderDate.Text=selectedOrder.OrderDate.ToString();
            txtOrderDate.IsEnabled = false;
            if (selectedOrder.ShipDate == DateTime.MinValue)
                txtShipDate.Text = "there is not date";
            else
            {
                txtShipDate.Text = selectedOrder.ShipDate.ToString();
                btnShipDate.Visibility = Visibility.Hidden;
            }
            txtShipDate.IsEnabled = false;
            if (selectedOrder.Delivery == DateTime.MinValue)
                txtDelivery.Text = "there is not date";
            else
            {
                txtDelivery.Text = selectedOrder.Delivery.ToString();
                btnDeliveryDate.Visibility = Visibility.Hidden;
            }
            txtDelivery.IsEnabled = false;
            ItemsListView.ItemsSource = selectedOrder.Items;
            txtTotalPrice.Text=selectedOrder.TotalPrice.ToString();
            txtTotalPrice.IsEnabled = false; 
            if(status=="orderTracking")
            {
                btnDeliveryDate.Visibility = Visibility.Hidden;
                btnShipDate.Visibility = Visibility.Hidden;
            }
            
        }
        private void btnShipDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order updateOrder=bl.Order.UpdateSentOrder(Convert.ToInt32(txtId.Text));
                txtShipDate.Text=updateOrder.ShipDate.ToString();
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

        private void btnDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order updateOrder = bl.Order.UpdateArrivedOrder(Convert.ToInt32(txtId.Text));
                txtDelivery.Text = updateOrder.ShipDate.ToString();
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

        private void ItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
