using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    /// <summary>
    ///property  id for OrderForList
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property  customer name for OrderForList
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    ///property  order status for OrderForList
    /// </summary>
    public EStatus OrderStatus { get; set; }
    /// <summary>
    ///property  amount of items for OrderForList
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    ///property  total price for OrderForList
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// function "to string" for  print the orderForList
    /// </summary>
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Customer Name: {CustomerName}, 
            OrderStatus: {OrderStatus},
    	    Amount Of Items: {AmountOfItems},
    	    Total Price: {TotalPrice}
            ";
}
