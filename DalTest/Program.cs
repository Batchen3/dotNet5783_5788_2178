using DalApi;
using DO;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;

namespace DalTest;
class Program
{

    private static IDal? dalList = DalApi.Factory.Get();

    public static void InfoOfProduct(char x)
    {
        string? idString, nameString, priceString, categoryString, inStockString, parveString;
        int idInt, categoryInt, inStockInt, parveInt;
        double priceDouble;
        switch (x)
        {
            case 'a'://add
                Console.WriteLine("enter product's id to add");
                idString = Console.ReadLine();
                int.TryParse(idString, out idInt);
                Console.WriteLine("enter product's name");
                nameString = Console.ReadLine();
                Console.WriteLine("enter product's price");
                priceString = Console.ReadLine();
                double.TryParse(priceString, out priceDouble);
                Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                categoryString = Console.ReadLine();
                int.TryParse(categoryString, out categoryInt);
                Console.WriteLine("enter product's instock");
                inStockString = Console.ReadLine();
                int.TryParse(inStockString, out inStockInt);
                Console.WriteLine("enter product's parve(0/1)");
                parveString = Console.ReadLine();
                int.TryParse(parveString, out parveInt);
                Product p = new Product { Id = idInt, Name = nameString, InStock = inStockInt, Parve =Convert.ToBoolean(parveInt), Price = priceDouble, Category = (ECategory)categoryInt };
                try
                {
                    int result = dalList?.Product.Add(p)??throw new NullException();
                    Console.WriteLine("the product was added");
                }
                catch (ExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FullListException fEx)
                {
                    Console.WriteLine(fEx.Message);
                }
                catch (NullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter product's id to read");
                string? idStringForGet = Console.ReadLine();
                int.TryParse(idStringForGet, out int idIntForGet);
                try
                {
                    Console.WriteLine(dalList?.Product.Get(idIntForGet));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the products:");
                try
                {
                    IEnumerable<Product> arrReadAllProducts = dalList?.Product.GetAll() ?? throw new NullException();
                    foreach (var item in arrReadAllProducts)
                        Console.WriteLine(item);
                }
                catch (NullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'd'://update
                Console.WriteLine("enter id of product to update");
                string? idStringUpdate = Console.ReadLine();//search of the id to update
                int.TryParse(idStringUpdate, out int idUpdate);
                //int idUpdate = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine("enter product's name");
                    nameString = Console.ReadLine();
                    Console.WriteLine("enter product's price");
                    priceString = Console.ReadLine();
                    double.TryParse(priceString, out priceDouble);
                    Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                    categoryString = Console.ReadLine();
                    int.TryParse(categoryString, out categoryInt);
                    Console.WriteLine("enter product's instock");
                    inStockString = Console.ReadLine();
                    int.TryParse(inStockString, out inStockInt);
                    Console.WriteLine("enter product's parve(0/1)");
                    parveString = Console.ReadLine();
                    int.TryParse(parveString, out parveInt);
                    Product pUpdate = new Product { Id = idUpdate, Name = nameString, InStock = inStockInt, Parve = Convert.ToBoolean(parveInt), Price = priceDouble, Category = (ECategory)categoryInt };
                    dalList?.Product.Update(pUpdate);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e'://delete a product
                Console.WriteLine("enter id of product to delete");
                idString = Console.ReadLine();
                int.TryParse(idString, out int idDelete);
                try
                {
                    dalList?.Product.Delete(idDelete);
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
        string? idString;
        switch (x)
        {
            case 'a'://add
                Order o = new Order();
                Console.WriteLine("enter order's customer name");
                o.CustomerName = Console.ReadLine();
                Console.WriteLine("enter order's customer email");
                o.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter order's customer address");
                o.CustomerAddress = Console.ReadLine();
                Console.WriteLine("enter order's date");
                o.OrderDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter order's date of ship");
                o.ShipDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter order's date of delivery");
                o.Delivery = Convert.ToDateTime(Console.ReadLine());
                try
                {
                    dalList?.Order.Add(o);
                }
                catch (FullListException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter order's id to read");
                idString = Console.ReadLine();
                int.TryParse(idString, out int id);
                try
                {
                    Console.WriteLine(dalList?.Order.Get(id));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the orders with their customers:");
                try
                {
                    IEnumerable<Order> arrReadAllOrders = dalList?.Order.GetAll() ?? throw new NullException(); ;
                    foreach (var item in arrReadAllOrders)
                        Console.WriteLine(item);
                }
                catch (NullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'd'://update
                Console.WriteLine("enter id of order to update");
                idString = Console.ReadLine();//search of the id to update
                int.TryParse(idString, out int idUpdate);
                try
                {
                    Console.WriteLine(dalList?.Order.Get(idUpdate));
                    Order oUpdate = new Order();
                    oUpdate.Id = idUpdate;
                    Console.WriteLine("enter order's customer name");
                    oUpdate.CustomerName = Console.ReadLine();
                    Console.WriteLine("enter order's customer email");
                    oUpdate.CustomerEmail = Console.ReadLine();
                    Console.WriteLine("enter order's customer address");
                    oUpdate.CustomerAddress = Console.ReadLine();
                    Console.WriteLine("enter order's date");
                    oUpdate.OrderDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter order's date of ship");
                    oUpdate.ShipDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter order's date of delivery");
                    oUpdate.Delivery = Convert.ToDateTime(Console.ReadLine());
                    dalList?.Order.Update(oUpdate);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e'://delete an order
                Console.WriteLine("enter id of product to delete");
                idString = Console.ReadLine();
                int.TryParse(idString, out int idDelete);
                try
                {
                    dalList?.Order.Delete(idDelete);
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
        string? productIdString, orderIdString, priceString, amountString,idString;
        int productId, orderId, amount;
        double price;
        switch (x)
        {
            case 'a'://add
                Console.WriteLine("enter id product of order item");
                productIdString = Console.ReadLine();
                int.TryParse(productIdString, out productId);
                Console.WriteLine("enter id order of order item");
                orderIdString = Console.ReadLine();
                int.TryParse(orderIdString, out orderId);
                Console.WriteLine("enter price of order item");
                priceString = Console.ReadLine();
                double.TryParse(priceString, out price);
                Console.WriteLine("enter amount of order item");
                amountString = Console.ReadLine();
                int.TryParse(amountString, out amount);
                OrderItem oi = new OrderItem { ProductID = productId, OrderID = orderId, Amount = amount, Price = price };
                try
                {
                    dalList?.OrderItem.Add(oi);
                }
                catch (FullListException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b'://read by id
                Console.WriteLine("enter order  item's id to read");
                idString = Console.ReadLine();
                int.TryParse(idString, out int id);
                try
                {
                    Console.WriteLine(dalList?.OrderItem.Get(id));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c'://read all
                Console.WriteLine("all the items in orders:");
                try
                {
                    IEnumerable<OrderItem> arrReadAllOrdersItems = dalList?.OrderItem.GetAll() ?? throw new NullException();
                    foreach (var item in arrReadAllOrdersItems)
                        Console.WriteLine(item);
                }
                catch (NullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'd'://update
                Console.WriteLine("enter id of order item to update");
                string? idUpdateString = Console.ReadLine();//search of the id to update
                int.TryParse(idUpdateString, out int idUpdate);
                try
                {
                    Console.WriteLine(dalList?.OrderItem.Get(idUpdate));
                    Console.WriteLine("enter id product of order item");
                    productIdString = Console.ReadLine();
                    int.TryParse(productIdString, out productId);
                    Console.WriteLine("enter id order of order item");
                    orderIdString = Console.ReadLine();
                    int.TryParse(orderIdString, out orderId);
                    Console.WriteLine("enter price of order item");
                    priceString = Console.ReadLine();
                    double.TryParse(priceString, out price);
                    Console.WriteLine("enter amount of order item");
                    amountString = Console.ReadLine();
                    int.TryParse(amountString, out amount);
                    OrderItem oiUpdate = new OrderItem { Id = idUpdate, ProductID = productId, OrderID = orderId, Amount = amount, Price = price };
                    dalList?.OrderItem.Update(oiUpdate);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e'://delete an item in order
                Console.WriteLine("enter id of item in order to delete");
                idString = Console.ReadLine();
                int.TryParse(idString, out int idDelete);
                try
                {
                    dalList?.OrderItem.Delete(idDelete);
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'f':// read item in order by ids of order and product
                Console.WriteLine("enter ids of order and product");
                productIdString = Console.ReadLine();
                int.TryParse(productIdString, out int idOrder);
                orderIdString = Console.ReadLine();
                int.TryParse(orderIdString, out int idProduct);
                try
                {
                    Console.WriteLine(dalList?.OrderItem.Get(element => element.OrderID == idOrder && element.ProductID == idProduct));
                }
                catch (NoSuchObjectException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'g'://read all items of specific order
                Console.WriteLine("enter id order");
                orderIdString = Console.ReadLine();
                int.TryParse(orderIdString, out int idOfOrder);
                try
                {
                    IEnumerable<OrderItem> allItemsInOrders = dalList?.OrderItem.GetAll(element => element.Id == idOfOrder) ?? throw new NullException(); ;
                    foreach (var item in allItemsInOrders)
                    {
                        if (item.OrderID == idOfOrder)
                            Console.WriteLine(item);
                    }
                }
                catch (NullException ex)
                {
                    Console.WriteLine(ex.Message);
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
        string? selectString, xString;
        int select;
        selectString=Console.ReadLine();
        int.TryParse(selectString, out select);
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
                    xString= Console.ReadLine();
                    char.TryParse(xString, out x);
                    InfoOfProduct(x);//doing this function 
                    break;
                case 2:
                    Console.WriteLine("for add an order press a");
                    Console.WriteLine("for read an order press b");
                    Console.WriteLine("for read all orders press c");
                    Console.WriteLine("for update an order press d");
                    Console.WriteLine("for delete an order press e");
                    xString = Console.ReadLine();
                    char.TryParse(xString, out x);
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
                    xString = Console.ReadLine();
                    char.TryParse(xString, out x);
                    InfoOfOrderItem(x);//doing this function 
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter a number");
            selectString = Console.ReadLine();
            int.TryParse(selectString, out select);
        }

    }
}
