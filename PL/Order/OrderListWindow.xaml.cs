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
        private ObservableCollection<OrderForList> _myCollection = new ObservableCollection<OrderForList>();

        public OrderListWindow()
        {
            InitializeComponent();
            //IEnumerable<OrderForList> list = bl.Order.GetOrders();
            //list.ToList().ForEach(item => _myCollection.Add(item));
            _myCollection = new ObservableCollection<OrderForList>(bl.Order.GetOrders());
            DataContext = _myCollection;
            //this.DataContext = bl.Order.GetOrders();
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
                _myCollection = new ObservableCollection<OrderForList>(bl.Order.GetOrders());
                DataContext = _myCollection;
                //DataContext = _myCollection;
                //this.DataContext = bl.Order.GetOrders();
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
