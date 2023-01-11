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
    public OrderTrackingWindow()
    {
        InitializeComponent();
        State = "before";
        DataContext = State;
    }

    private void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        string? idString = txtOrderTrackingId.Text;
        int.TryParse(idString, out int id);
        try
        {
            BO.OrderTracking orderTracking = bl.Order.OrderTracking(id);
            State = "after";
            DataContext = State;
            // lblStatus.Visibility = Visibility.Visible;
            txtStatus.Text = orderTracking.OrderStatus.ToString();
            //txtStatus.Visibility = Visibility.Visible;
            txtStatus.IsEnabled = false;
            listViewStatus.ItemsSource = orderTracking.DateAndDescriptionOrder;
            //listViewStatus.Visibility = Visibility.Visible;
            //btnDetails.Visibility = Visibility.Visible;
        }
        catch (BO.DalException ex)
        {
            MessageBox.Show(ex.InnerException?.Message);
        }
    }

    private void btnDetails_Click(object sender, RoutedEventArgs e)
    {
        string? idString = txtOrderTrackingId.Text;
        int.TryParse(idString, out int id);
        try
        {
            BO.Order order = bl.Order.GetDetailsOfOrder(id);
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
    public object Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
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
