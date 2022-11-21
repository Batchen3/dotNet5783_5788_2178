using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList> GetOrders();
    public BO.Order GetDetailsOfOrder(int id);
    public BO.Order UpdateSentOrder(int id);
    public BO.Order UpdateArrivedOrder(int id);
    public BO.Order UpdateOrder(int id);
}
