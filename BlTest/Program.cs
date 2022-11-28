 using BlApi;
using BlImplementation;
using System.Text.RegularExpressions;


namespace BlTest
{
    internal class Program
    {
        private static IBl Bl = new Bl();

        private static void Product(char x)
        {
            if (x != 'g')
            {
                int id;
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
                        id = int.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(Bl.Product.Get(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'd'://add a product
                        BO.Product product = new BO.Product();
                        Console.WriteLine("enter product's id to add");
                        product.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product's name");
                        product.Name = Console.ReadLine();
                        while (Regex.IsMatch(product.Name, @"^\d+$"))
                        {
                            Console.WriteLine("the name is incorrect, please enter name again");
                            product.Name = Console.ReadLine();
                        }
                        Console.WriteLine("enter product's price");
                        product.Price = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                        int category = int.Parse(Console.ReadLine());
                        while (category < 0 || category > 2)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            category = int.Parse(Console.ReadLine());
                        }
                        product.Category = (BO.ECategory)category;
                        Console.WriteLine("enter product's instock");
                        product.InStock = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product's parve(0/1)");
                        product.Parve = int.Parse(Console.ReadLine());
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
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 'e'://delete a product
                        Console.WriteLine("enter id of product");
                        id = int.Parse(Console.ReadLine());
                        try
                        {
                            Bl.Product.Delete(id);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (BO.ProductInOrderException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'f'://update a product
                        BO.Product productForUpdate = new BO.Product();
                        Console.WriteLine("enter product's id to update");
                        productForUpdate.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product's name");
                        productForUpdate.Name = Console.ReadLine();
                        while (Regex.IsMatch(productForUpdate.Name, @"^\d+$"))
                        {
                            Console.WriteLine("the name is incorrect, please enter name again");
                            productForUpdate.Name = Console.ReadLine();
                        }
                        Console.WriteLine("enter product's price");
                        productForUpdate.Price = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter product's category(0-for cups,1-for cakes,2-for cookies)");
                        int categoryUpdate = int.Parse(Console.ReadLine());
                        while (categoryUpdate < 0 || categoryUpdate > 2)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            categoryUpdate = int.Parse(Console.ReadLine());
                        }
                        productForUpdate.Category = (BO.ECategory)categoryUpdate;
                        Console.WriteLine("enter product's instock");
                        productForUpdate.InStock = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product's parve(0/1)");
                        productForUpdate.Parve = int.Parse(Console.ReadLine());
                        try
                        {
                            Bl.Product.Update(productForUpdate);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
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
        private static void Order(char x)
        {
            if (x != 'f')
            {
                int id;
                switch (x)
                {
                    case 'a'://get all orders 
                        IEnumerable<BO.OrderForList> listaOfOrders = Bl.Order.GetOrders();
                        foreach (var item in listaOfOrders)
                            Console.WriteLine(item);
                        break;
                    case 'b'://Get Details Of Order
                        Console.WriteLine("enter order's id");
                        id = int.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(Bl.Order.GetDetailsOfOrder(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'c'://update sent of order
                        Console.WriteLine("enter id for update sent of order");
                        id = int.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(Bl.Order.UpdateSentOrder(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (BO.DateWasException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'd'://update arrived of order
                        Console.WriteLine("enter id for update arrived of order");
                        id = int.Parse(Console.ReadLine());
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
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (BO.DatesNotInCorrectOrderException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'e'://update amount for order item / manager-bonus
                        Console.WriteLine("enter id of order");
                        int orderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter id of product");
                        int productId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter amount");
                        int amount = int.Parse(Console.ReadLine());
                        try
                        {
                            Bl.Order.UpdateOrder(orderId, productId,amount);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
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
        private static BO.Cart Cart(char x,BO.Cart cart)
        {
            if (x != 'd')
            {
                int id,amount;
                switch (x)
                {
                    case 'a'://for add cart
                        Console.WriteLine("enter product's id to add");
                        id = int.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(Bl.Cart.Add(cart, id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch(BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'b'://for update cart
                        Console.WriteLine("enter product's id to add");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter new amount");
                        amount =int.Parse(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(Bl.Cart.Update(cart, id, amount));
                        }
                        catch (BO.ObjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch(BO.OutOfStockException ex)
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
                        catch(BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch(BO.NotValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch(BO.OutOfStockException ex)
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
            int select = int.Parse(Console.ReadLine());
            char x;
            BO.Cart cart=new BO.Cart();
            cart.Items = new List<BO.OrderItem>();
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
                        x = char.Parse(Console.ReadLine());
                        Product(x);//doing this function 
                        break;
                    case 2:
                        Console.WriteLine("for watching all orders (manager) press a");
                        Console.WriteLine("for get a certain order press b");
                        Console.WriteLine("for update sent order press c");
                        Console.WriteLine("for update arrived order press d");
                        Console.WriteLine("for update amount of order item (manager) press e");
                        Console.WriteLine("for exit press f");
                        x = char.Parse(Console.ReadLine());
                        Order(x);//doing this function 
                        break;
                    case 3:
                        Console.WriteLine("for add cart press a");
                        Console.WriteLine("for update cart press b");
                        Console.WriteLine("for save cart press c");
                        Console.WriteLine("for exit press d");
                        x = char.Parse(Console.ReadLine());
                        cart=Cart(x,cart);//doing this function 
                        break;
                    default:
                        break;
                }
                Console.WriteLine("for product press 1");
                Console.WriteLine("for order press 2");
                Console.WriteLine("for cart press 3");
                Console.WriteLine("for exit press 0");
                select = int.Parse(Console.ReadLine());
            }



        }
    }
}