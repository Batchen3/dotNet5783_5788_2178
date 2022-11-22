using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BlApi;
using DalApi;
using DalFacade;
using System.Collections;
using Dal;
namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal dalList = new Dal.DalList();
    public BO.Cart Add(BO.Cart c, int id)
    {
        try
        {
            DO.Product product = dalList.Product.Get(id);//ישלח חריגה אם לא מצא
            if (product._inStock <= 0)//ישלח חריגה אם אין מלאי
                throw new BO.OutOfStockException();
            bool exist = false;
            foreach (var orderItem in c.Items)//קיים בסל קניה
                if (orderItem.ProductID == id)
                {
                    exist = true;
                    orderItem.AmountsItems++;
                    orderItem.TotalPriceOfItems += product._price;
                    c.TotalPrice += product._price;
                }
            if (!exist)
            {
                BO.OrderItem newOrderItem = new BO.OrderItem{ ID=0,ProductID = product._id,ProductName = product._name,ProductPrice = product._price, AmountsItems = 1, TotalPriceOfItems = product._price };
                c.TotalPrice += product._price;
                c.Items.Add(newOrderItem);
            }
            return c;
        }
        catch (NoSuchObjectException e)
        {
            throw new BO.DalException(e);
        }
       
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
            throw new BO.ObjectNotFoundException();
        return c;
    }
    public void SaveCart(BO.Cart c)
    {
        if (c.CustomerName == "" || c.CustomerAddress == "")
            throw new BO.NotValidException();
        if (c.CustomerEmail != "")
        {
            try
            {
                var addr = new MailAddress(c.CustomerEmail);
            }
            catch
            {
                throw new BO.NotValidException();
            }
        }
        DO.Product product = new DO.Product();
        foreach (var orderItem in c.Items)
        {
            try
            {
                product = dalList.Product.Get(orderItem.ProductID);
                if (orderItem.AmountsItems <= 0)
                    throw new BO.NotValidException();
                if (orderItem.AmountsItems > product._inStock)
                    throw new BO.OutOfStockException();
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }
        }
        DO.Order order = new DO.Order { _customerName= c.CustomerName, _customerAddress= c.CustomerAddress, _customerEmail= c.CustomerEmail, _orderDate= DateTime.Now, _shipDate = DateTime.MinValue, _delivery = DateTime.MinValue };
        try
        {
            int idOrder = dalList.Order.Add(order);
            foreach (var orderItem in c.Items)
            {
                DO.OrderItem orderItemAdd = new DO.OrderItem { _productID= orderItem.ProductID, _price = orderItem.ProductPrice,_amount = orderItem.AmountsItems, _orderID = idOrder };
                dalList.OrderItem.Add(orderItemAdd);
                product = dalList.Product.Get(orderItem.ProductID);
                product._inStock -= orderItem.AmountsItems;
                dalList.Product.Update(product);
            }
        }
        catch (FullListException ex)
        {
            throw new BO.DalException(ex);
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }
    }
}
