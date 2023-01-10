using BlApi;
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
using BlImplementation;
using System.Globalization;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    BO.Product product = new BO.Product();
    BlApi.IBl bl = BlApi.Factory.Get();
    public string Status { get; set; }
    public Array AllCategories { get; set; }
    public ProductWindow()
    {
        InitializeComponent();
        AllCategories = Enum.GetValues(typeof(BO.ECategory));
        Status = "add";
        this.DataContext = new{ product,Status = Status, AllCategories = AllCategories };
    }
    public ProductWindow(BO.Product selectedProduct, string state)
    {
        InitializeComponent();
        Status = state;
        AllCategories = Enum.GetValues(typeof(BO.ECategory));
        product = selectedProduct;
        this.DataContext = new { product, Status = Status, AllCategories = AllCategories };
    } 
    private void btnSaveAdding_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            if (Status == "add")
            {
                bl.Product.Add(product);
            }
            else
            {
                bl.Product.Update(product);
            }
            Close();
        }

        catch (DalException ex)
        {
            MessageBox.Show(ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void btnAddToCart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ICart.cart = bl.Cart.Add(ICart.cart, product.ID);
        }
        catch (BO.DalException ex)
        {
            MessageBox.Show(ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        Close();
    }
}
public class RadioButtonParveConverter : IValueConverter
{
    public object Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
    {
        bool boolValue = (bool)value;
        return !boolValue;
    }
    public object ConvertBack(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
    {
        bool boolValue = (bool)value;
        return !boolValue;
        //throw new NotImplementedException();
    }
}

public class StatusForbtnAddToCartConverter : IValueConverter
{
    public object Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "display")
        {
            return Visibility.Visible;
        }
        else
        {
            return Visibility.Hidden;
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

public class StatusForBtnSaveAddingConverter : IValueConverter
{
    public object Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "display")
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

public class StatusForTextBox : IValueConverter
{
    public object Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "display")
            return false;
        return true;
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
public class StatusForTxtId : IValueConverter
{
    public object Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "add")
            return true;
        return false;
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