using BlApi;
using BlImplementation;
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

namespace PL.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = txtName.Text;
            ICart.cart.CustomerName = name;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? email = txtEmail.Text;
            ICart.cart.CustomerEmail = email;
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? address = txtAddress.Text;
            ICart.cart.CustomerAddress = address;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.SaveCart(ICart.cart);
                MessageBox.Show("the order was sent");
                Close();
                ICart.cart = new BO.Cart { Items = new List<BO.OrderItem?>() };
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
}
