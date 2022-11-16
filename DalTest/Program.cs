﻿using DalApi;
using DO;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;

namespace DalTest;
class Program
{

    private static Dal.DalList dalList = new Dal.DalList();
    
    public static void InfoOfProduct(char x)
    {
        switch (x)
        {
            case 'a'://add

                Product p = new Product();
                Console.WriteLine("enter product's id to add");
                p._id = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product's name");
                p._name = Console.ReadLine();
                Console.WriteLine("enter product's price");
                p._price = double.Parse(Console.ReadLine());
                Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                p._category = (ECategory)int.Parse(Console.ReadLine());
                Console.WriteLine("enter product's instock");
                p._inStock = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product's parve(0/1)");
                p._parve = int.Parse(Console.ReadLine());
                try
                {
                    int result= dalList.Product.Add(p);
                    Console.WriteLine("the product was added");
                }
                catch (ExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(FullListException fEx)
                {
                    Console.WriteLine(fEx.Message);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter product's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalList.Product.Get(id));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the products:");
                IEnumerable<Product> arrReadAllProducts = dalList.Product.GetAll();
                foreach (var item in arrReadAllProducts)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of product to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalList.Product.Get(idUpdate));
                    Product pUpdate = new Product();
                    pUpdate._id = idUpdate;
                    Console.WriteLine("enter product's name");
                    pUpdate._name = Console.ReadLine();
                    Console.WriteLine("enter product's price");
                    pUpdate._price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                    pUpdate._category = (ECategory)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's instock");
                    pUpdate._inStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's parve(0/1)");
                    pUpdate._parve = int.Parse(Console.ReadLine());
                    dalList.Product.Update(pUpdate);

                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e'://delete a product
                Console.WriteLine("enter id of product to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalList.Product.Delete(idDelete);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                break;
        }
    }
    public static void InfoOfOrder(char x)
    {
        switch (x)
        {
            case 'a'://add
                Order o = new Order();
                Console.WriteLine("enter order's customer name");
                o._customerName = Console.ReadLine();
                Console.WriteLine("enter order's customer email");
                o._customerEmail = Console.ReadLine();
                Console.WriteLine("enter order's customer address");
                o._customerAddress = Console.ReadLine();
                Console.WriteLine("enter order's date");
                o._orderDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter order's date of ship");
                o._shipDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter order's date of delivery");
                o._delivery = Convert.ToDateTime(Console.ReadLine());
                try
                {
                    dalList.Order.Add(o);
                }
                catch (FullListException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter order's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalList.Order.Get(id));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the orders with their customers:");
                IEnumerable<Order> arrReadAllOrders = dalList.Order.GetAll();
                foreach (var item in arrReadAllOrders)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of order to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalList.Order.Get(idUpdate));
                    Order oUpdate = new Order();
                    oUpdate._id = idUpdate;
                    Console.WriteLine("enter order's customer name");
                    oUpdate._customerName = Console.ReadLine();
                    Console.WriteLine("enter order's customer email");
                    oUpdate._customerEmail = Console.ReadLine();
                    Console.WriteLine("enter order's customer address");
                    oUpdate._customerAddress = Console.ReadLine();
                    Console.WriteLine("enter order's date");
                    oUpdate._orderDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter order's date of ship");
                    oUpdate._shipDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter order's date of delivery");
                    oUpdate._delivery = Convert.ToDateTime(Console.ReadLine());
                    dalList.Order.Update(oUpdate);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e'://delete an order
                Console.WriteLine("enter id of product to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalList.Order.Delete(idDelete);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                break;
        }
    }
    public static void InfoOfOrderItem(char x)
    {
        switch (x)
        {
            case 'a'://add
                OrderItem oi = new OrderItem();
                Console.WriteLine("enter id product of order item");
                oi._productID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter id order of order item");
                oi._orderID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter price of order item");
                oi._price = double.Parse(Console.ReadLine());
                Console.WriteLine("enter amount of order item");
                oi._amount = int.Parse(Console.ReadLine());
                try
                {
                    dalList.OrderItem.Add(oi);
                }
                catch (FullListException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter order  item's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalList.OrderItem.Get(id));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the items in orders:");
                IEnumerable<OrderItem> arrReadAllOrdersItems = dalList.OrderItem.GetAll();
                foreach (var item in arrReadAllOrdersItems)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of order item to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalList.OrderItem.Get(idUpdate));
                    OrderItem oiUpdate = new OrderItem();
                    oiUpdate._id = idUpdate;
                    Console.WriteLine("enter id product of order item");
                    oiUpdate._productID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter id order of order item");
                    oiUpdate._orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter price of order item");
                    oiUpdate._price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter amount of order item");
                    oiUpdate._amount = int.Parse(Console.ReadLine());
                    dalList.OrderItem.Update(oiUpdate);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e'://delete an item in order
                Console.WriteLine("enter id of item in order to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalList.OrderItem.Delete(idDelete);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'f':// read item in order by ids of order and product
                Console.WriteLine("enter ids of order and product");
                int idOrder = int.Parse(Console.ReadLine());
                int idProduct = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalList.OrderItem.readByOrderAndProduct(idOrder, idProduct));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'g'://read all items of specific order
                Console.WriteLine("enter id order");
                int idOfOrder = int.Parse(Console.ReadLine());
                IEnumerable<OrderItem> allItemsInOrders = dalList.OrderItem.readByOrder(idOfOrder);
                foreach (var item in allItemsInOrders)
                {
                    if (item._orderID == idOfOrder)
                        Console.WriteLine(item);
                }
                break;
            default:
                break;
        }
    }
    public static void Main(string[] args)
    {
       // dalList.Product.d();
        Console.WriteLine("for product press 1");
        Console.WriteLine("for order press 2");
        Console.WriteLine("for item in order press 3");
        Console.WriteLine("for exit press 0");
        int select = int.Parse(Console.ReadLine());
        char x;
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    Console.WriteLine("for add a product press a");
                    Console.WriteLine("for read a product press b");
                    Console.WriteLine("for read all products press c");
                    Console.WriteLine("for update a product press d");
                    Console.WriteLine("for delete a product press e");
                    x = char.Parse(Console.ReadLine());
                    InfoOfProduct(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for add an order press a");
                    Console.WriteLine("for read an order press b");
                    Console.WriteLine("for read all orders press c");
                    Console.WriteLine("for update an order press d");
                    Console.WriteLine("for delete an order press e");
                    x = char.Parse(Console.ReadLine());
                    InfoOfOrder(x); //doing this function 
                    break;
                case 3:
                    Console.WriteLine("for add an item in order press a");
                    Console.WriteLine("for read item in order press b");
                    Console.WriteLine("for read all items in orders press c");
                    Console.WriteLine("for update an item in order press d");
                    Console.WriteLine("for delete an item in order press e");
                    Console.WriteLine("for read an item in order by id of order and product press f");
                    Console.WriteLine("for read an items in order press g");
                    x = char.Parse(Console.ReadLine());
                    InfoOfOrderItem(x);//doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            select = int.Parse(Console.ReadLine());
        }

    }
}