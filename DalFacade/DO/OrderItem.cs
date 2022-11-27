//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct OrderItem
{
    /// <summary>
    /// property Id for OrderItem
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// property ProductId for OrderItem
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// property OrderId for OrderItem
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// property Price for OrderItem
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// property Amount for OrderItem
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// function toString for print OrderItem
    /// </summary>
    public override string ToString() => $@"
            ID: {Id},
            Product ID: {ProductID}, 
            Order ID : {OrderID},
    	    Price: {Price},
    	    Amount in order: {Amount}
            ";

}
