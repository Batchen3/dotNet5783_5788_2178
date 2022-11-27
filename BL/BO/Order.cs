using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class Order
{
    /// <summary>
    ///property  id for order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property  customer name for order
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    ///property  customer email for order
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    ///property  customer address for order
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    ///property  order status for order 
    /// </summary>
    public EStatus OrderStatus { get; set; }
    /// <summary>
    ///property  order date for order
    /// </summary>
    public System.DateTime OrderDate { get; set; }
    /// <summary>
    ///property  ship date  for order
    /// </summary>
    public System.DateTime ShipDate { get; set; }//arrive to the delivery
    /// <summary>
    ///property  delivery date for order
    /// </summary>
    public System.DateTime Delivery { get; set; } //arrive to the customer
    /// <summary>
    ///property  items for order
    /// </summary>
    public List<OrderItem> Items { get; set; }
    /// <summary>
    ///property  total price for order
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// function "to string" for  print the order
    /// </summary>
    public override string ToString()
    {
        string details = "ID: " + ID +
                "\nCustomer Name: " + CustomerName +
                 "\nCustomer Email: " + CustomerEmail +
                 "\nCustomerAddress: " + CustomerAddress +
                  "\nStatus: " + OrderStatus +
                 "\nOrder Date: " + OrderDate +
                 "\nShip Date: " + ShipDate +
                 "\nDelivery: " + Delivery+"\n";
        for (int i = 0; i < Items.Count; i++)
        {
            details += "item " + (i + 1) + ": " + Items[i] + "\n";
        }
        details += "TotalPrice: " + TotalPrice;
        return details;
    }
}
