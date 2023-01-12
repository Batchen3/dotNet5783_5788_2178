using BO;
using PL.Order;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PL.Customer;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    BlApi.IBl bl = BlApi.Factory.Get();
    public string State { get; set; }
    public OrderTracking OrderTracking { get; set; } = new OrderTracking();
    public OrderTrackingWindow()
    {
        InitializeComponent();
        State = "before";
        DataContext = new { State = State, OrderTracking = OrderTracking };
    }

    private void btnSearch_Click(object sender, RoutedEventArgs e)
    {       
        try
        {
            OrderTracking = bl.Order.OrderTracking(OrderTracking.ID);
            State = "after";
            DataContext = new { State = State, OrderTracking = OrderTracking };       
        }
        catch (BO.DalException ex)
        {
            MessageBox.Show(ex.InnerException?.Message);
        }
    }

    private void btnDetails_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Order order = bl.Order.GetDetailsOfOrder(OrderTracking.ID);
            OrderWindow orderWindow = new OrderWindow(order, "orderTracking");
            orderWindow.Show();
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
public class StateVisibilityConverter : IValueConverter
{
    public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "before")
        {
            return Visibility.Hidden;
        }
        else
        {
            return Visibility.Visible;
        }
    }
    public object ConvertBack(
    object value,
    Type targetType,
    object parameter,

    CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class IDIsEnableConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "before")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public object ConvertBack(
    object value,
    Type targetType,
    object parameter,

    CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
