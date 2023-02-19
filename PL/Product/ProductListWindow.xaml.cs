using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace PL.Product;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    class Properties : DependencyObject
    {
        public string State
        {
            get{return(string)GetValue(StateProperty);}
            set{SetValue(StateProperty, value);}
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(string), typeof(Properties), new UIPropertyMetadata());
        public Array AllCategories
        {
            get{return(Array)GetValue(AllCategoriesProperty);}
            set{SetValue(AllCategoriesProperty, value);}
        }
        public static readonly DependencyProperty AllCategoriesProperty = DependencyProperty.Register("AllCategories", typeof(Array), typeof(Properties), new PropertyMetadata());
        public List<BO.ProductForList> ProductsListForAdmin
        {
            get { return (List<BO.ProductForList>)GetValue(ProductsListForAdminProperty); }
            set { SetValue(ProductsListForAdminProperty, value); }
        }
        public static readonly DependencyProperty ProductsListForAdminProperty = DependencyProperty.Register("ProductsListForAdmin", typeof(List<BO.ProductForList>), typeof(Properties), new PropertyMetadata());

        public List<BO.ProductItem> ProductsListForNewOrder
        {
            get { return (List<BO.ProductItem>)GetValue(ProductsListForNewOrderProperty); }
            set { SetValue(ProductsListForNewOrderProperty, value); }
        }
        public static readonly DependencyProperty ProductsListForNewOrderProperty = DependencyProperty.Register("ProductsListForNewOrder", typeof(List<BO.ProductItem>), typeof(Properties), new PropertyMetadata());

    }


    BlApi.IBl bl = BlApi.Factory.Get();
    Properties properties=new Properties();
    private void SortByCategory()
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(properties.ProductsListForNewOrder);
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
        view.GroupDescriptions.Add(groupDescription);
    }
    public ProductListWindow(string from)
    {
        InitializeComponent();
        properties.State = from;
        properties.AllCategories = Enum.GetValues(typeof(BO.ECategory));
        if (properties.State == "admin")
        {
            properties.ProductsListForAdmin = new List<BO.ProductForList>(bl.Product.GetAll());
            DataContext = properties;
        }
        else
        {
            properties.ProductsListForNewOrder = new List<BO.ProductItem>(bl.Product.GetCatalog());
            DataContext = properties;
            SortByCategory();
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        ProductWindow product = new ProductWindow();
        product.ShowDialog();
        properties.ProductsListForAdmin = bl.Product.GetAll().ToList();
    }

    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList? productForList = null;
        BO.ProductItem? productItem = null;
        BO.Product? selectedItem = null;
        try
        {
            if (properties.State == "admin")
            {
                productForList = (BO.ProductForList)ProductsListview.SelectedItem;
                selectedItem = bl.Product.Get(productForList.ID);
                ProductWindow productWindow = new ProductWindow(selectedItem, "update");
                productWindow.ShowDialog();
                properties.ProductsListForAdmin = bl.Product.GetAll().ToList();
            }
            if (properties.State == "newOrder")
            {
                productItem = (BO.ProductItem)ProductsListviewCatalog.SelectedItem;
                selectedItem = bl.Product.Get(productItem.ID);
                ProductWindow productWindow = new ProductWindow(selectedItem, "display");
                productWindow.ShowDialog();
                properties.ProductsListForNewOrder = bl.Product.GetCatalog().ToList();
                SortByCategory();
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
        if (properties.State == "admin")
            properties.ProductsListForAdmin = bl.Product.GetAll().ToList();
        if (properties.State == "newOrder")
        {
            properties.ProductsListForNewOrder = bl.Product.GetCatalog().ToList();
            SortByCategory();
        }         
    }

    private void CategorySelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        if (properties.State == "admin")
            properties.ProductsListForAdmin = bl.Product.GetByCategoryAdmin((BO.ECategory)CategorySelector.SelectedItem).ToList();
        if (properties.State == "newOrder")
        {
            properties.ProductsListForNewOrder = bl.Product.GetByCategoryForOrder((BO.ECategory)CategorySelector.SelectedItem).ToList();
            SortByCategory();
        }
    }

    private void btnGoToCart_Click(object sender, RoutedEventArgs e)
    {
        Cart.CartListWindow cartListWindow = new Cart.CartListWindow();
        cartListWindow.ShowDialog();
        properties.ProductsListForNewOrder = bl.Product.GetCatalog().ToList();
        SortByCategory();
    }
}

public class NewOrder_Converter : IValueConverter
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
public class Admin_Converter : IValueConverter
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