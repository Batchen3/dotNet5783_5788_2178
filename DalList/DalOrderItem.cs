//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


using DalApi;
using DO;
using System;
using static Dal.DataSource;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem value)
    {
        value.Id = Config.IdOrderItem++;
        if (s_listOrderItem.Count == NUMORDERITEM)
            throw new FullListException();
        else
            s_listOrderItem.Add(value);
        return value.Id;
    }//create order item
    public OrderItem Get(int id)
    {
        var orderItem = (from item in s_listOrderItem
                               where item.Id == id
                               select item).FirstOrDefault();
        if (orderItem.Id == 0)
            throw new NoSuchObjectException();
        return orderItem;
    }//read order item
    //public OrderItem readByOrderAndProduct(int idOrder, int idProduct)
    //{
    //    for (int i = 0; i < s_listOrderItem.Count; i++)
    //    {
    //        if (s_listOrderItem[i].OrderID == idOrder && s_listOrderItem[i].ProductID == idProduct)
    //            return s_listOrderItem[i];
    //    }
    //    throw new NoSuchObjectException();
    //}
    //read order item by order id and product id
    //public IEnumerable<OrderItem> readByOrder(int idOrder)
    //{
    //    List<OrderItem> list = new List<OrderItem>();
    //    for (int i = 0; i < s_listOrderItem.Count; i++)
    //    {
    //        if (s_listOrderItem[i].OrderID == idOrder)
    //        {
    //            list.Add(s_listOrderItem[i]);
    //        }
    //    }
    //    return list;
    //}//read by order id
    public OrderItem Get(Predicate<OrderItem> func)
    {
        return s_listOrderItem.Find(func);
    }//get by condition
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null)
    {
        return (func == null) ? s_listOrderItem : s_listOrderItem.Where(func);
    }
    public void Update(OrderItem value)
    {
        try
        {
            OrderItem orderItem = Get(value.Id);
            var index = s_listOrderItem.IndexOf(orderItem);
            s_listOrderItem[index] = value;
        }
        catch (NoSuchObjectException ex)
        {
            throw ex;
        }

    }//update the order item
    public void Delete(int id)
    {
        try
        {
            OrderItem orderItem = Get(id);
            s_listOrderItem.Remove(orderItem);
        }
        catch (NoSuchObjectException ex)
        {
            throw ex;
        }

    }//delete order item by id
}
