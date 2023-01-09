using BlApi;
using BlImplementation;
using DalApi;
using System.Text.RegularExpressions;


namespace BlTest
{
    internal class Program
    {
        private static IBl Bl = new Bl();

        private static void _product(char x)
        {
            if (x != 'g')
            {
                int pId, id, category, inStock, parve;
                string? idString, pIdString, pName, pPriceString, categoryString, inStockString, parveString;
                double pPrice;
                string nameChecking;
                switch (x)
                {
                    case 'a'://get all products as product-for-list
                        IEnumerable<BO.ProductForList> listaOfProduct = Bl.Product.GetAll();
                        foreach (var item in listaOfProduct)
                            Console.WriteLine(item);
                        break;
                    case 'b'://get all products as product-item
                        IEnumerable<BO.ProductItem> listOfProductItem = Bl.Product.GetCatalog();
                        foreach (var item in listOfProductItem)
                            Console.WriteLine(item);
                        break;
                    case 'c'://get a product
                        Console.WriteLine("enter id of product");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        try
                        {
                            Console.WriteLine(Bl.Product.Get(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'd'://add a product
                        Console.WriteLine("enter product's id to add");
                        pIdString = Console.ReadLine();
                        int.TryParse(pIdString, out pId);
                        Console.WriteLine("enter product's name");
                        pName = Console.ReadLine();
                        try
                        {
                            nameChecking = pName ?? throw new NullException();
                            while (Regex.IsMatch(nameChecking, @"^\d+$"))
                            {
                                Console.WriteLine("the name is incorrect, please enter name again");
                                pName = Console.ReadLine();
                                nameChecking = pName ?? throw new NullException();
                            }
                        }
                        catch (NullException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine("enter product's price");
                        pPriceString = Console.ReadLine();
                        double.TryParse(pPriceString, out pPrice);
                        Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                        categoryString = Console.ReadLine();
                        int.TryParse(categoryString, out category);
                        while (category < 0 || category > 2)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            categoryString = Console.ReadLine();
                            int.TryParse(categoryString, out category);
                        }
                        Console.WriteLine("enter product's instock");
                        inStockString = Console.ReadLine();
                        int.TryParse(inStockString, out inStock);
                        Console.WriteLine("enter product's parve(0/1)");
                        parveString = Console.ReadLine();
                        int.TryParse(parveString, out parve);
                        BO.Product product = new BO.Product { ID = pId, Name = pName, Price = pPrice, Category = (BO.ECategory)category, InStock = inStock, Parve = parve };
                        try
                        {
                            Bl.Product.Add(product);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        break;
                    case 'e'://delete a product
                        Console.WriteLine("enter id of product");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        try
                        {
                            Bl.Product.Delete(id);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.ProductInOrderException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'f'://update a product
                        Console.WriteLine("enter product's id to update");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        Console.WriteLine("enter product's name");
                        pName = Console.ReadLine();
                        try
                        {
                            nameChecking = pName ?? throw new NullException();
                            while (Regex.IsMatch(nameChecking, @"^\d+$"))
                            {
                                Console.WriteLine("the name is incorrect, please enter name again");
                                pName = Console.ReadLine();
                                nameChecking = pName ?? throw new NullException();
                            }
                        }
                        catch (NullException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine("enter product's price");
                        pPriceString = Console.ReadLine();
                        double.TryParse(pPriceString, out pPrice);
                        Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                        categoryString = Console.ReadLine();
                        int.TryParse(categoryString, out category);
                        while (category < 0 || category > 2)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            categoryString = Console.ReadLine();
                            int.TryParse(categoryString, out category);
                        }
                        Console.WriteLine("enter product's instock");
                        inStockString = Console.ReadLine();
                        int.TryParse(inStockString, out inStock);
                        Console.WriteLine("enter product's parve(0/1)");
                        parveString = Console.ReadLine();
                        int.TryParse(parveString, out parve);
                        BO.Product productForUpdate = new BO.Product { ID = id, Name = pName, Price = pPrice, Category = (BO.ECategory)category, InStock = inStock, Parve = parve }; 
                        try
                        {
                            Bl.Product.Update(productForUpdate);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private static void _order(char x)
        {
            if (x != 'f')
            {
                int id, orderId, productId, amount;
                string? idString, orderIdString, productIdString, amountString;
                switch (x)
                {
                    case 'a'://get all orders 
                        IEnumerable<BO.OrderForList> listaOfOrders = Bl.Order.GetOrders();
                        foreach (var item in listaOfOrders)
                            Console.WriteLine(item);
                        break;
                    case 'b'://Get Details Of Order
                        Console.WriteLine("enter order's id");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        try
                        {
                            Console.WriteLine(Bl.Order.GetDetailsOfOrder(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'c'://update sent of order
                        Console.WriteLine("enter id for update sent of order");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        try
                        {
                            Console.WriteLine(Bl.Order.UpdateSentOrder(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.DateWasException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'd'://update arrived of order
                        Console.WriteLine("enter id for update arrived of order");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        try
                        {
                            Console.WriteLine(Bl.Order.UpdateArrivedOrder(id));
                        }
                        catch (BO.DateWasException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.DatesNotInCorrectOrderException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'e'://update amount for order item / manager-bonus
                        Console.WriteLine("enter id of order");
                        orderIdString = Console.ReadLine();
                        int.TryParse(orderIdString, out orderId);                      
                        Console.WriteLine("enter id of product");
                        productIdString = Console.ReadLine();
                        int.TryParse(productIdString, out productId);
                        Console.WriteLine("enter amount");
                        amountString = Console.ReadLine();
                        int.TryParse(amountString, out amount);
                        try
                        {
                            Bl.Order.UpdateOrder(orderId, productId, amount);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.DateWasException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private static BO.Cart _cart(char x, BO.Cart cart)
        {
            if (x != 'd')
            {
                int id, amount;
                string? idString,amountString;
                switch (x)
                {
                    case 'a'://for add cart
                        Console.WriteLine("enter product's id to add");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        try
                        {
                            Console.WriteLine(Bl.Cart.Add(cart, id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'b'://for update cart
                        Console.WriteLine("enter product's id to add");
                        idString = Console.ReadLine();
                        int.TryParse(idString, out id);
                        Console.WriteLine("enter new amount");
                        amountString = Console.ReadLine();
                        int.TryParse(amountString, out amount);
                        try
                        {
                            Console.WriteLine(Bl.Cart.Update(cart, id, amount));
                        }
                        catch (BO.ObjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'c'://for save cart
                        Console.WriteLine("enter your name");
                        cart.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter your address");
                        cart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("enter your email");
                        cart.CustomerEmail = Console.ReadLine();
                        try
                        {
                            Bl.Cart.SaveCart(cart);
                            Console.WriteLine(cart);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
            return cart;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("for product press 1");
            Console.WriteLine("for order press 2");
            Console.WriteLine("for cart press 3");
            Console.WriteLine("for exit press 0");
            string? selectString, xString;
            int select;
            char x;
            selectString =Console.ReadLine();
            int.TryParse(selectString, out select);
            BO.Cart cart = new BO.Cart();
            cart.Items = new List<BO.OrderItem?>();
            while (select != 0)
            {
                switch (select)
                {
                    case 1:
                        Console.WriteLine("for watching all products (manager) press a");
                        Console.WriteLine("for watching all products (customer) press b");
                        Console.WriteLine("for get a certain product press c");
                        Console.WriteLine("for adding a product press d");
                        Console.WriteLine("for delete a product press e");
                        Console.WriteLine("for update a product press f");
                        Console.WriteLine("for exit press g");
                        xString= Console.ReadLine();
                        char.TryParse(xString,out x);
                        _product(x);//doing this function 
                        break;
                    case 2:
                        Console.WriteLine("for watching all orders (manager) press a");
                        Console.WriteLine("for get a certain order press b");
                        Console.WriteLine("for update sent order press c");
                        Console.WriteLine("for update arrived order press d");
                        Console.WriteLine("for update amount of order item (manager) press e");
                        Console.WriteLine("for exit press f");
                        xString = Console.ReadLine();
                        char.TryParse(xString, out x);
                        _order(x);//doing this function 
                        break;
                    case 3:
                        Console.WriteLine("for add cart press a");
                        Console.WriteLine("for update cart press b");
                        Console.WriteLine("for save cart press c");
                        Console.WriteLine("for exit press d");
                        xString = Console.ReadLine();
                        char.TryParse(xString, out x);
                        cart = _cart(x, cart);//doing this function 
                        break;
                    default:
                        break;
                }
                Console.WriteLine("for product press 1");
                Console.WriteLine("for order press 2");
                Console.WriteLine("for cart press 3");
                Console.WriteLine("for exit press 0");
                selectString = Console.ReadLine();
                int.TryParse(selectString, out select);
            }

        }
    }
}