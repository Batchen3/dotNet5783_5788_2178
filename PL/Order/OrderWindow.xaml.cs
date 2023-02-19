using BO;
using DO;
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
using System.Xml.Linq;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    class Properties : DependencyObject
    {
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public static readonly DependencyProperty OrderProperty = DependencyProperty.Register("Order", typeof(BO.Order), typeof(Properties), new PropertyMetadata(new BO.Order()));
        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(string), typeof(Properties), new PropertyMetadata());

    }
    BlApi.IBl bl = BlApi.Factory.Get();
    Properties properties = new Properties();
    public OrderWindow(BO.Order selectedOrder, string status)
    {
        InitializeComponent();
        properties.State = status;
        properties.Order = selectedOrder;
        DataContext= properties;
    }
    private void btnShipDate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Order updateOrder = bl.Order.UpdateSentOrder(Convert.ToInt32(txtId.Text));
            properties.Order=updateOrder;
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

    private void btnDeliveryDate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Order updateOrder = bl.Order.UpdateArrivedOrder(Convert.ToInt32(txtId.Text));
            properties.Order = updateOrder;
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
public class BtndateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "orderTracking")
        {
            return Visibility.Hidden;
        }
        else
        {
            return Visibility.Visible;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class BtnShipDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "confirmed")
        {
            return true;
        }
        else
        { 
            return false;
        }

    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class BtnDeliveryDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "arrived")
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}