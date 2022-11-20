using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BlApi;
using BO;
using DalApi;
using DalFacade;
using System.Collections;
using DO;
using Dal;

namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal dalList = new Dal.DalList();
    public BO.Cart Add(BO.Cart c, int id)
    {
        DO.Product product = dalList.Product.Get(id);//ישלח חריגה אם לא מצא
        if (product._inStock <= 0)//ישלח חריגה אם אין מלאי
            throw new BO.OutOfStockException();
        bool exist = false;
        BO.OrderItem orderItemOfCart = new BO.OrderItem();
        foreach (var orderItem in c.Items)//קיים בסל קניה
            if (orderItem.ProductID == id)
            {
                exist = true;
                orderItemOfCart = orderItem;
            }
        if (!exist)
        {
            BO.OrderItem newOrderItem = new BO.OrderItem();
            //לא איתחלנו id
            newOrderItem.ProductID = product._id;
            newOrderItem.ProductName = product._name;
            newOrderItem.ProductPrice = product._price;
            newOrderItem.AmountsItems = 1;
            newOrderItem.TotalPriceOfItems = product._price;
            c.TotalPrice += product._price;
            c.Items.Add(newOrderItem);
        }
        else
        {
            orderItemOfCart.AmountsItems++;
            orderItemOfCart.TotalPriceOfItems += product._price;
            c.TotalPrice += product._price;
        }
        return c;
    }
    public BO.Cart Update(BO.Cart c, int id, int amount)
    {
        bool exist = false;
        foreach (var orderItem in c.Items)//קיים בסל קניה
            if (orderItem.ProductID == id)
            {
                exist = true;
                if (amount > orderItem.AmountsItems)
                {
                    orderItem.TotalPriceOfItems = amount * orderItem.ProductPrice;
                    c.TotalPrice = c.TotalPrice + orderItem.ProductPrice * (amount - orderItem.AmountsItems);
                    orderItem.AmountsItems = amount;
                }
                else if (amount < orderItem.AmountsItems && amount != 0)
                {
                    orderItem.TotalPriceOfItems = amount * orderItem.ProductPrice;
                    c.TotalPrice = c.TotalPrice - orderItem.ProductPrice * (orderItem.AmountsItems - amount);
                    orderItem.AmountsItems = amount;
                }
                else if (amount == 0)
                {
                    c.TotalPrice -= orderItem.AmountsItems * orderItem.ProductPrice;
                    c.Items.Remove(orderItem);
                }
            }
        if (!exist)
            throw new NoSuchObjectException();
        return c;
    }
    public void SaveCart(BO.Cart c)
    {
        if (c.CustomerName == "" || c.CustomerAddress == "")
            throw new NotValidException();
        if (c.CustomerEmail != "")
        {
            try
            {
                var addr = new MailAddress(c.CustomerEmail);
            }
            catch
            {
                throw new NotValidException();
            }
        }
        DO.Product product = new DO.Product();
        foreach (var orderItem in c.Items)
        {
            product = dalList.Product.Get(orderItem.ProductID);
            if (orderItem.AmountsItems <= 0)
                throw new NotValidException();
            if (orderItem.AmountsItems > product._inStock)
                throw new OutOfStockException();
        }
        DO.Order order = new DO.Order();
        order._customerName = c.CustomerName;
        order._customerAddress = c.CustomerAddress;
        order._customerEmail = c.CustomerEmail;
        order._orderDate = DateTime.Now;
        order._shipDate = DateTime.MinValue;
        order._delivery = DateTime.MinValue;
        int idOrder = dalList.Order.Add(order);
        foreach (var orderItem in c.Items)
        {
            DO.OrderItem orderItemAdd = new DO.OrderItem();
            orderItemAdd._productID = orderItem.ProductID;
            orderItemAdd._price = orderItem.ProductPrice;
            orderItemAdd._amount = orderItem.AmountsItems;
            orderItemAdd._orderID = idOrder;
            dalList.OrderItem.Add(orderItemAdd);
            product = dalList.Product.Get(orderItem.ProductID);
            product._inStock -= orderItem.AmountsItems;
            dalList.Product.Update(product);
        }
    }
}
