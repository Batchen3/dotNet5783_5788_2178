using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public EStatus OrderStatus { get; set; }
    public List<(DateTime,EStatus)> DateAndStatusOrder { get; set; }
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Order Status: {OrderStatus}, 
            Date And Status Order: {DateAndStatusOrder}
            ";
}
