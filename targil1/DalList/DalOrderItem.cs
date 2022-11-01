//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
    public int create(OrderItem oi)
    {
        oi.ID = Config.IdOrderItem;
        if (Config.moneOrderItem > arrayOrderItem.Length)
            throw new Exception("the orderItems array is full");
        else
            arrayOrderItem[Config.moneOrderItem++] = oi;
        return oi.ID;
    }//create order item
    public OrderItem read(int id)
    {
        for (int i = 0; i < Config.moneOrderItem; i++)
        {
            if (arrayOrderItem[i].ID == id)
                return arrayOrderItem[i];
        }
        throw new Exception("the orderItem not found");
    }//read order item
    public OrderItem readByOrderAndProduct(int idOrder, int idProduct)
    {
        for (int i = 0; i < Config.moneOrderItem; i++)
        {
            if (arrayOrderItem[i].OrderID == idOrder && arrayOrderItem[i].ProductID == idProduct)
                return arrayOrderItem[i];
        }
        throw new Exception("the orderItem not found");
    }//read order item by order id and product id
    public OrderItem[] readByOrder(int idOrder)
    {
        OrderItem[] arr = new OrderItem[Config.moneOrderItem];
        int counter = 0;
        for (int i = 0; i < Config.moneOrderItem; i++)
        {
            if (arrayOrderItem[i].OrderID == idOrder)
            {
                arr[counter] = arrayOrderItem[i];
                counter++;
            }
        }
        return arr;
    }//read by order id
    public OrderItem[] readAll()
    {
        OrderItem[] tmpOrderItem = new OrderItem[Config.moneOrderItem];
        for (int i = 0; i < Config.moneOrderItem; i++)
        {
            tmpOrderItem[i] = arrayOrderItem[i];
        }
        return tmpOrderItem;
    }//read all order item
    public void update(OrderItem oi)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < Config.moneOrderItem && !isExist; j++)
        {
            if (arrayOrderItem[j].ID == oi.ID)
                isExist = true;

        }
        if (!isExist)
            throw new Exception("this order item is not exist");
        for (int i = 0; i < Config.moneOrderItem; i++)
        {
            if (arrayOrderItem[i].ID == oi.ID)
                arrayOrderItem[i] = oi;
        }
    }//update the order item
    public void delete(int id)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < Config.moneOrderItem && !isExist; j++)
        {
            if (arrayOrderItem[j].ID == id)
                isExist = true;

        }
        if (!isExist)
            throw new Exception("this order item is not exist");
        for (int i = 0; i < Config.moneOrderItem; i++)
        {
            if (arrayOrderItem[i].ID == id)
            {
                if (i == Config.moneOrderItem)
                    Config.moneOrderItem--;
                else
                    arrayOrderItem[i] = arrayOrderItem[Config.moneOrderItem--];
            }

        }
    }//delete order item by id
}
