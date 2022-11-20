using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrders();
    public Order GetDetailsOfOrder(int id);
    public Order UpdateSentOrder(int id);
    public Order UpdateArrivedOrder(int id);
    public Order UpdateOrder(int id);
}
