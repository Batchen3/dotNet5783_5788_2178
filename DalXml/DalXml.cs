using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    /// <summary>
    /// initialize Product to do the functions in DalProduct
    /// </summary>
    public IProduct Product { get; } = new Product();
    /// <summary>
    /// initialize Order to do the functions in DalOrder
    /// </summary>
    public IOrder Order { get; } = new Order();
    /// <summary>
    /// initialize OrderItem to do the functions in DalOrderItem
    /// </summary>
    public IOrderItem OrderItem { get; } = new OrderItem();
    private DalXml() { }
}
