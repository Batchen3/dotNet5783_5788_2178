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
    public List<(DateTime?,string)>? DateAndDescriptionOrder { get; set; }
    /// <summary>
    /// function "to string" for  print the OrderTracking
    /// </summary>
    /// 
    public override string ToString()
    {
        string details = "ProductItem ID: " + ID +
                "\nOrder Status: " + OrderStatus + "\n";
        DateAndDescriptionOrder?.ForEach(item => details += item.Item1 + " " + item.Item2 + "\n");
        //for (int i = 0; i < DateAndDescriptionOrder?.Count; i++)
        //{
        //    details += "Date:" + (i + 1) + ": " + DateAndDescriptionOrder[i].Item1 + " Description" + (i + 1) + ": " + DateAndDescriptionOrder[i].Item2 + "\n";
        //}
        return details;
    }
}
