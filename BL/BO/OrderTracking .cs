using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    /// <summary>
    ///property id for OrderTracking
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property OrderStatus for OrderTracking
    /// </summary>
    public EStatus OrderStatus { get; set; }
    /// <summary>
    ///property DateAndDescriptionOrder for OrderTracking
    /// </summary>
    public List<(DateTime,string)> DateAndDescriptionOrder { get; set; }
    /// <summary>
    /// function "to string" for  print the OrderTracking
    /// </summary>
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Order Status: {OrderStatus}, 
            Date And Status Order: {DateAndDescriptionOrder}
            ";
}
