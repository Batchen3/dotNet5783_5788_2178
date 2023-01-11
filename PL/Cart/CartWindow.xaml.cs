using BlApi;
using BlImplementation;
using BO;
using DO;
using PL.Product;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {

        //לא מעדכן את המחיר כי הבינדינג משנה לפני כבר את הכמות

        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.OrderItem OrderItem { get; set; }
        public CartWindow(BO.OrderItem orderItem)
        {
            InitializeComponent();
            OrderItem = orderItem;
            DataContext = orderItem;
        }

        private void btnChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            //string? id = txtIdProduct.Text;
            //int.TryParse(id, out int productId);
            //string? newAmount = txtAmountItems.Text;
            //int.TryParse(newAmount, out int amount);
            try
            {
                ICart.cart = bl.Cart.Update(ICart.cart, OrderItem.ProductID, OrderItem.AmountsItems);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //string? id = txtIdProduct.Text;
            //int.TryParse(id, out int productId);
            try
            {
                ICart.cart = bl.Cart.Update(ICart.cart, OrderItem.ProductID, 0);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
