using BlApi;
using BO;
using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    class Properties : DependencyObject
    {
        public string Status
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("Status", typeof(string), typeof(Properties), new UIPropertyMetadata());
        public Array AllCategories
        {
            get { return (Array)GetValue(AllCategoriesProperty); }
            set { SetValue(AllCategoriesProperty, value); }
        }
        public static readonly DependencyProperty AllCategoriesProperty = DependencyProperty.Register("AllCategories", typeof(Array), typeof(Properties), new PropertyMetadata());
        public BO.Product Product
        {
            get { return (BO.Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }
        public static readonly DependencyProperty ProductProperty = DependencyProperty.Register("Product", typeof(BO.Product), typeof(Properties), new PropertyMetadata(new BO.Product()));
    }


    Properties properties=new Properties();
    BlApi.IBl bl = BlApi.Factory.Get();
    public ProductWindow()
    {
        InitializeComponent();
        properties.AllCategories = Enum.GetValues(typeof(BO.ECategory));
        properties.Status = "add";
        DataContext = properties;
    }
    public ProductWindow(BO.Product selectedProduct, string state)
    {
        InitializeComponent();
        properties.Status = state;
        properties.AllCategories = Enum.GetValues(typeof(BO.ECategory));
        properties.Product = selectedProduct;
        DataContext = properties;
    } 
    private void btnSaveAdding_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            if (properties.Status == "add")
            {
                bl.Product.Add(properties.Product);
            }
            else
            {
                bl.Product.Update(properties.Product);
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
            ICart.cart = bl.Cart.Add(ICart.cart, properties.Product.ID);
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