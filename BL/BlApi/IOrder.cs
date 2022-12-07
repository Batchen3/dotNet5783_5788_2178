using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;
/// <summary>
/// the interface has 3 functions and BlOrder doing them
/// </summary>
public interface IOrder
{
    /// <summary>
    /// function get orders
    /// return all orders in orderforlist from dal
    /// </summary>
    public IEnumerable<BO.OrderForList> GetOrders(Func<DO.Order, bool>? func = null);
    /// <summary>
    /// function Get Details Of Order
    /// recive id of order 
    ///return the details of order   
    /// </summary>
    public BO.Order GetDetailsOfOrder(int id);
    /// <summary>
    /// function Update Sent Order 
    /// recive order id
    /// return order with new ship date and update it
    /// </summary>
    public BO.Order UpdateSentOrder(int id);
    /// <summary>
    /// function Update Arrived Order
    /// recive order id
    /// return order with new delivery date and update it
    /// </summary>
    public BO.Order UpdateArrivedOrder(int id);
    //bonus
    public void UpdateOrder(int idOrder,int idProduct,int amount);
}
