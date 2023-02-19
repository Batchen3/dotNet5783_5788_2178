//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using DalApi;
using DO;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrder : IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order value)
    {
        value.Id = Config.IdOrder++;
        if (s_listOrder.Count == NUMORDERS)
            throw new FullListException();
        else
            s_listOrder.Add(value);
        return value.Id;
    }//create an order

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int id)
    {
        var order = (from item in s_listOrder
                       where item.Id == id
                       select new Order{Id=item.Id, CustomerName=item.CustomerName, CustomerEmail=item.CustomerEmail, CustomerAddress =item.CustomerAddress, OrderDate =item.OrderDate, ShipDate=item.ShipDate,Delivery=item.Delivery }).FirstOrDefault();
        if (order.Id == 0)
            throw new NoSuchObjectException();
        return order;

    }//read order according id 

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        return (func == null) ? s_listOrder : s_listOrder.Where(func);
    }//read all the orders by condition or not


    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Predicate<Order> func)
    {
        return s_listOrder.Find(func);
    }//get by condition


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order value)
    {
        try
        {
            Order order = Get(value.Id);
            var index = s_listOrder.IndexOf(order);
            s_listOrder[index] = value;
        }
        catch (NoSuchObjectException ex)
        {
            throw ex;
        }


    }//update the order


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            Order order = Get(id);
            s_listOrder.Remove(order);
        }
        catch (NoSuchObjectException ex)
        {
            throw ex;
        }

    }//delete an order
}
