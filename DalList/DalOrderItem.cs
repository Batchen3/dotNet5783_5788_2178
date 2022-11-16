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

internal class DalOrderItem:IOrderItem
{
    public int Add(OrderItem value)
    {
        value._id = Config.IdOrderItem++;
        if (s_listOrderItem.Count == NUMORDERITEM)
            throw new FullListException();
        else
            s_listOrderItem.Add(value);
        return value._id;
    }//create order item
    public OrderItem Get(int id)
    {
        for (int i = 0; i < s_listOrderItem.Count; i++)
        {
            if (s_listOrderItem[i]._id == id)
                return s_listOrderItem[i];
        }
        throw new NoSuchObjectException();
    }//read order item
    public OrderItem readByOrderAndProduct(int idOrder, int idProduct)
    {
        for (int i = 0; i < s_listOrderItem.Count; i++)
        {
            if (s_listOrderItem[i]._orderID == idOrder && s_listOrderItem[i]._productID == idProduct)
                return s_listOrderItem[i];
        }
        throw new NoSuchObjectException();
    }//read order item by order id and product id
    public IEnumerable<OrderItem> readByOrder(int idOrder)
    {
        List<OrderItem> list = new List<OrderItem>();
        for (int i = 0; i < s_listOrderItem.Count; i++)
        {
            if (s_listOrderItem[i]._orderID == idOrder)
            {
                list.Add(s_listOrderItem[i]);
            }
        }
        return list;
    }//read by order id
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> tmpOrderItem = new List<OrderItem>();
        for (int i = 0; i < s_listOrderItem.Count; i++)
        {
            tmpOrderItem.Add(s_listOrderItem[i]);
        }
        return tmpOrderItem;
    }//read all order item
    public void Update(OrderItem value)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listOrderItem.Count && !isExist; j++)
        {
            if (s_listOrderItem[j]._id == value._id)
            {
                isExist = true;
                s_listOrderItem[j] = value;
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//update the order item
    public void Delete(int id)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listOrderItem.Count && !isExist; j++)
        {
            if (s_listOrderItem[j]._id == id)
            {
                isExist = true;
                s_listOrderItem.Remove(s_listOrderItem[j]);
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//delete order item by id
}
