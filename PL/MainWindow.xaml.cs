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
using System.Globalization;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    BlApi.IBl bl = BlApi.Factory.Get();
    public string Status { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        Status = "start";
        DataContext = Status;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ProductListWindow product = new ProductListWindow("admin");
        product.Show();
    }

    private void btnAdmin_Click(object sender, RoutedEventArgs e)
    {
        Status = "admin";
        DataContext = Status;
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
        Customer.OrderTrackingWindow orderTracking = new Customer.OrderTrackingWindow();
        orderTracking.Show();
    }

    private void btnStartImaging_Click(object sender, RoutedEventArgs e)
    {
        Simulator.Simulator simulator=new Simulator.Simulator();
        simulator.Show();
    }
}
public class VisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "admin")
        {
            return Visibility.Visible;
        }
        else
        {
            return Visibility.Hidden;
        }
    }
    public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
