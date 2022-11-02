﻿using DO;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;

namespace DalTest;
class Program
{
    private static Dal.DalProduct dalProduct = new Dal.DalProduct();
    private static Dal.DalOrder dalOrder = new Dal.DalOrder();
    private static Dal.DalOrderItem dalOrderItem = new Dal.DalOrderItem();

    public static void infoOfProduct(char x)
    {
        switch (x)
        {
            case 'a'://add

                Product p = new Product();
                Console.WriteLine("enter product's id to add");
                p.id = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product's name");
                p.name = Console.ReadLine();
                Console.WriteLine("enter product's price");
                p.price = double.Parse(Console.ReadLine());
                Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                p.category = (ECategory)int.Parse(Console.ReadLine());
                Console.WriteLine("enter product's instock");
                p.inStock = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product's parve(0/1)");
                p.parve = int.Parse(Console.ReadLine());
                try
                {

                    dalProduct.create(p);
                    Console.WriteLine("the product was added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter product's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalProduct.read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the products:");
                Product[] arrReadAllProducts = dalProduct.readAll();
                foreach (var item in arrReadAllProducts)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of product to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalProduct.read(idUpdate));
                    Product pUpdate = new Product();
                    pUpdate.id = idUpdate;
                    Console.WriteLine("enter product's name");
                    pUpdate.name = Console.ReadLine();
                    Console.WriteLine("enter product's price");
                    pUpdate.price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                    pUpdate.category = (ECategory)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's instock");
                    pUpdate.inStock = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product's parve(0/1)");
                    pUpdate.parve = int.Parse(Console.ReadLine());
                    dalProduct.update(pUpdate);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e'://delete a product
                Console.WriteLine("enter id of product to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalProduct.delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            default:
                break;
        }
    }
    public static void infoOfOrder(char x)
    {
        switch (x)
        {
            case 'a'://add
                Order o = new Order();
                Console.WriteLine("enter order's customer name");
                o.customerName = Console.ReadLine();
                Console.WriteLine("enter order's customer email");
                o.customerEmail = Console.ReadLine();
                Console.WriteLine("enter order's date");
                o.orderDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter order's date of ship");
                o.shipDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter order's date of delivery");
                o.delivery = Convert.ToDateTime(Console.ReadLine());
                try
                {
                    dalOrder.create(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter order's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalOrder.read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the orders with their customers:");
                Order[] arrReadAllOrders = dalOrder.readAll();
                foreach (var item in arrReadAllOrders)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of order to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalOrder.read(idUpdate));
                    Order oUpdate = new Order();
                    oUpdate.id = idUpdate;
                    Console.WriteLine("enter order's customer name");
                    oUpdate.customerName = Console.ReadLine();
                    Console.WriteLine("enter order's customer email");
                    oUpdate.customerEmail = Console.ReadLine();
                    Console.WriteLine("enter order's date");
                    oUpdate.orderDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter order's date of ship");
                    oUpdate.shipDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter order's date of delivery");
                    oUpdate.delivery = Convert.ToDateTime(Console.ReadLine());
                    dalOrder.update(oUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e'://delete an order
                Console.WriteLine("enter id of product to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalOrder.delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            default:
                break;
        }
    }
    public static void infoOfOrderItem(char x)
    {
        switch (x)
        {
            case 'a'://add
                OrderItem oi = new OrderItem();
                Console.WriteLine("enter id product of order item");
                oi.productID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter id order of order item");
                oi.orderID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter price of order item");
                oi.price = double.Parse(Console.ReadLine());
                Console.WriteLine("enter amount of order item");
                oi.amount = int.Parse(Console.ReadLine());
                try
                {
                    dalOrderItem.create(oi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter order  item's id to read");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalOrderItem.read(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the items in orders:");
                OrderItem[] arrReadAllOrdersItems = dalOrderItem.readAll();
                foreach (var item in arrReadAllOrdersItems)
                    Console.WriteLine(item);
                break;
            case 'd'://update
                Console.WriteLine("enter id of order item to update");
                int idUpdate = int.Parse(Console.ReadLine());//search of the id to update
                try
                {
                    Console.WriteLine(dalOrderItem.read(idUpdate));
                    OrderItem oiUpdate = new OrderItem();
                    oiUpdate.id = idUpdate;
                    Console.WriteLine("enter id product of order item");
                    oiUpdate.productID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter id order of order item");
                    oiUpdate.orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter price of order item");
                    oiUpdate.price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter amount of order item");
                    oiUpdate.amount = int.Parse(Console.ReadLine());
                    dalOrderItem.update(oiUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'e'://delete an item in order
                Console.WriteLine("enter id of item in order to delete");
                int idDelete = int.Parse(Console.ReadLine());
                try
                {
                    dalOrderItem.delete(idDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'f':// read item in order by ids of order and product
                Console.WriteLine("enter ids of order and product");
                int idOrder = int.Parse(Console.ReadLine());
                int idProduct = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(dalOrderItem.readByOrderAndProduct(idOrder, idProduct));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case 'g'://read all items of specific order
                Console.WriteLine("enter id order");
                int idOfOrder = int.Parse(Console.ReadLine());
                OrderItem[] allItemsInOrders = dalOrderItem.readByOrder(idOfOrder);
                foreach (var item in allItemsInOrders)
                {
                    Console.WriteLine(item);
                }
                break;
            default:
                break;
        }
    }
    public static void Main(string[] args)
    {
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
                    infoOfProduct(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for add an order press a");
                    Console.WriteLine("for read an order press b");
                    Console.WriteLine("for read all orders press c");
                    Console.WriteLine("for update an order press d");
                    Console.WriteLine("for delete an order press e");
                    x = char.Parse(Console.ReadLine());
                    infoOfOrder(x); //doing this function 
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
                    infoOfOrderItem(x);//doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            select = int.Parse(Console.ReadLine());
        }

    }
}
