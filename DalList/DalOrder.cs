﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DalApi;
using DO;
using System;
using System.Diagnostics.Metrics;
using static Dal.DataSource;

namespace Dal;

internal class DalOrder:IOrder
{
    public int Add(Order value)
    {
        value._id = Config.IdOrder++;
        if (s_listOrder.Count == NUMORDERS)
            throw new FullListException();
        else
            s_listOrder.Add(value);
        return value._id;
    }//create an order
    public Order Get(int id)
    {
        for (int i = 0; i < s_listOrder.Count; i++)
        {
            if (s_listOrder[i]._id == id)
                return s_listOrder[i];
        }
        throw new NoSuchObjectException();
    }//read order according id 
    public IEnumerable<Order> GetAll()
    {
        List<Order> tmpOrders = new List<Order>();
        for (int i = 0; i < s_listOrder.Count; i++)
        {
            tmpOrders.Add(s_listOrder[i]);
        }
        return tmpOrders;
    }//read all the orders
    public void Update(Order value)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listOrder.Count && !isExist; j++)
        {
            if (s_listOrder[j]._id == value._id)
            {
                isExist = true;
                s_listOrder[j] = value;
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//update the order
    public void Delete(int id)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listOrder.Count && !isExist; j++)
        {
            if (s_listOrder[j]._id == id)
            {
                isExist = true;
                s_listOrder.Remove(s_listOrder[j]);
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//delete an order
}