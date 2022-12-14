using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    /// <summary>
 ///property  id for OrderItem
 /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property  ProductID for OrderItem
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    ///property  ProductName for OrderItem
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    ///property  ProductPrice for OrderItem
    /// </summary>
    public double ProductPrice { get; set; }
    /// <summary>
    ///property  AmountsItems for OrdeItem
    /// </summary>
    public int AmountsItems { get; set; }
    /// <summary>
    ///property  TotalPriceOfItems for OrderItem
    /// </summary>
    public double TotalPriceOfItems { get; set; }
    /// <summary>
    /// function "to string" for  print the OrderItem
    /// </summary>
    public override string ToString() => $@"
            ID: {ID},
            Product ID: {ProductID}, 
            Product Name: {ProductName},
    	    Product Price: {ProductPrice},
    	    Amounts Items: {AmountsItems},
            Total Price Of Items: {TotalPriceOfItems}
            ";
}
