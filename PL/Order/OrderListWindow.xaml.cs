using BlImplementation;
using BO;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Properties properties = new Properties();
        class Properties : DependencyObject
        {
            public List<BO.OrderForList> Orders
            {
                get { return (List<BO.OrderForList>)GetValue(OrdersProperty); }
                set { SetValue(OrdersProperty, value); }
            }
            public static readonly DependencyProperty OrdersProperty = DependencyProperty.Register("Orders", typeof(List<BO.OrderForList>), typeof(Properties), new PropertyMetadata());

        }
        public OrderListWindow()
        {
            InitializeComponent();
            properties.Orders =bl.Order.GetOrders().ToList();
            DataContext = properties;
        }

        private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? orderForList = null;
            BO.Order? selectedItem = null;
            try
            {
                orderForList = (BO.OrderForList)OrdersListview.SelectedItem;
                selectedItem = bl.Order.GetDetailsOfOrder(orderForList.ID);
                OrderWindow orderWindow = new OrderWindow(selectedItem,"admin");
                orderWindow.ShowDialog();
                properties.Orders = bl.Order.GetOrders().ToList();
            }
            catch (BO.DalException ex)
            {
                MessageBox.Show(ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }      
    }
}
