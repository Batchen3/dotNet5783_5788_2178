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
                        int categor = int.Parse(Console.ReadLine());
                        while (categor < 0 || categor > 2)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            categor = int.Parse(Console.ReadLine());
                        }
                        product.Category = (BO.ECategory)categor;
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
                    case 'e'://update a product
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
                    case 'f':

                        break;
                    default:
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("for product press 1");
            Console.WriteLine("for order press 2");
            Console.WriteLine("for cart press 3");
            Console.WriteLine("for exit press 0");
            int select = int.Parse(Console.ReadLine());
            char x;
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
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("enter a number");
                select = int.Parse(Console.ReadLine());
            }


        }
    }
}