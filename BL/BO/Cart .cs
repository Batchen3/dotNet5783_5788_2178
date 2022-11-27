using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    /// <summary>
    ///property  customer name for cart
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    ///property  Customer Email for cart
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    ///property  Customer Address for cart
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    ///property  Items for cart
    /// </summary>
    public List<OrderItem> Items { get; set; }
    /// <summary>
    ///property  TotalPrice for cart
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// function "to string" for  print the cart
    /// </summary>
    public override string ToString()
    {
        string details = "Customer Name: " + CustomerName +
                "\nCustomer Email: " + CustomerEmail +
                 "\nCustomerAddress: " + CustomerAddress + "\n";
        for (int i = 0; i < Items.Count; i++)
        {
            details += "item " + (i + 1) + ": " + Items[i] + "\n";
        }
        details += "TotalPrice: " + TotalPrice;
        return details;
    }
}
