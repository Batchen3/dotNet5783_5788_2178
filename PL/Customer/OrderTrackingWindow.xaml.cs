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
    Properties properties = new Properties();
    class Properties : DependencyObject
    { 
        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(string), typeof(Properties), new PropertyMetadata());
        public OrderTracking OrderTracking
        {
            get { return (OrderTracking)GetValue(OrderTrackingProperty); }
            set { SetValue(OrderTrackingProperty, value); }
        }
        public static readonly DependencyProperty OrderTrackingProperty = DependencyProperty.Register("OrderTracking", typeof(OrderTracking), typeof(Properties), new PropertyMetadata(new OrderTracking()));

    }

    public OrderTrackingWindow()
    {
        InitializeComponent();
        properties.State = "before";
        DataContext = properties;
    }

    private void btnSearch_Click(object sender, RoutedEventArgs e)
    {       
        try
        {
            properties.OrderTracking = bl.Order.OrderTracking(properties.OrderTracking.ID);
            properties.State = "after";
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
            BO.Order order = bl.Order.GetDetailsOfOrder(properties.OrderTracking.ID);
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
