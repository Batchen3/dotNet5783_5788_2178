using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    BlApi.IBl bl = BlApi.Factory.Get();
    public string State { get; set; }
    public Array AllCategories { get; set; }

    public ProductListWindow(string from)
    {
        InitializeComponent();
        State = from;
        AllCategories = Enum.GetValues(typeof(BO.ECategory));
        this.DataContext = new { State = State , AllCategories = AllCategories };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ProductWindow product = new ProductWindow();
        product.ShowDialog();
        ProductsListview.ItemsSource = bl.Product.GetAll();
    }

    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList? productForList = null;
        BO.ProductItem? productItem = null;
        BO.Product? selectedItem = null;
        try
        {
            if (State == "admin")
            {
                productForList = (BO.ProductForList)ProductsListview.SelectedItem;
                selectedItem = bl.Product.Get(productForList.ID);
                ProductWindow productWindow = new ProductWindow(selectedItem, "update");
                productWindow.ShowDialog();
               ProductsListview.ItemsSource = bl.Product.GetAll();
            }
            if (State == "newOrder")
            {
                productItem = (BO.ProductItem)ProductsListview.SelectedItem;
                selectedItem = bl.Product.Get(productItem.ID);
                ProductWindow productWindow = new ProductWindow(selectedItem, "display");
                productWindow.ShowDialog();
                ProductsListview.ItemsSource = bl.Product.GetCatalog();
            }
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
    private void btnShowAll_Click(object sender, RoutedEventArgs e)
    {
        if (State == "admin")
            ProductsListview.ItemsSource = bl.Product.GetAll();
        if (State == "newOrder")
            ProductsListview.ItemsSource = bl.Product.GetCatalog();
    }

    private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        if (State == "admin")
            ProductsListview.ItemsSource = bl.Product.GetByCategoryAdmin((BO.ECategory)CategorySelector.SelectedItem);
        if (State == "newOrder")
            ProductsListview.ItemsSource = bl.Product.GetByCategoryForOrder((BO.ECategory)CategorySelector.SelectedItem);
    }

    private void btnGoToCart_Click(object sender, RoutedEventArgs e)
    {
        Cart.CartListWindow cartListWindow = new Cart.CartListWindow();
        cartListWindow.ShowDialog();
        ProductsListview.ItemsSource = bl.Product.GetCatalog();
    }
}

public class ChooseListViewConverter : IValueConverter
{
    BlApi.IBl bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "admin")
        {
            //return new ObservableCollection<BO.ProductForList>(bl.Product.GetAll());
            return bl.Product.GetAll();
        }
        else
        {
            return bl.Product.GetCatalog();
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "admin")
        {
            return bl.Product.GetAll();
        }
        else
        {
            return bl.Product.GetCatalog();
        }
    }
}

public class BtnCart_Available_Amount_Converter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "admin")
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
public class BtnAddProductConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "admin")
        {
            return Visibility.Visible;
        }
        else
        {
            return Visibility.Hidden;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}