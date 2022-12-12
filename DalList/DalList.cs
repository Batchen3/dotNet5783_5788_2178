using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal sealed class DalList : IDal
/// <summary>
/// doing interface IDal
/// </summary>
{
    public static IDal Instance { get; } = new DalList();
    /// <summary>
    /// initialize Product to do the functions in DalProduct
    /// </summary>
    public IProduct Product => new DalProduct();
    /// <summary>
    /// initialize Order to do the functions in DalOrder
    /// </summary>
    public IOrder Order => new DalOrder();
    /// <summary>
    /// initialize OrderItem to do the functions in DalOrderItem
    /// </summary>
    public IOrderItem OrderItem => new DalOrderItem();
    private DalList()
    {

    }
}
