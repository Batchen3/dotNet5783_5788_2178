using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private IDal dalList = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrders()
    {
        IEnumerable<DO.Order> allOrders = dalList.Order.GetAll();
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        foreach (var item in allOrders)
        {
            orders.Add(new OrderForList{ID=Config.OrderForListId,CustomerName=item._customerName});

        }


    //     public int ID { get; set; }
    //public string CustomerName { get; set; }
    //public EStatus OrderStatus { get; set; }
    //public int AmountOfItems { get; set; }
    //public double TotalPrice { get; set; }


}
    public BO.Order GetDetailsOfOrder(int id)
    {


    }
    public BO.Order UpdateSentOrder(int id)
    {
    }
    public BO.Order UpdateArrivedOrder(int id)
    {

    }
    public BO.Order UpdateOrder(int id)
    {

    }


}
