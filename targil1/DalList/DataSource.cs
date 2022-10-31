using System;
using System.Net;

namespace Dal;

static internal class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    const int SIZE_ARRAY_ORDER = 100;
    const int SIZE_ARRAY_ORDERITEM = 200;
    const int SIZE_ARRAY_PRODUCT = 50;
    static int curentIndexOrder = 0;
    static int curentIndexOrderItem = 0;
    static int curentIndexProduct = 0;
    readonly static Random random = new Random();
    static internal DO.Order[] arrayOrder = new DO.Order[SIZE_ARRAY_ORDER];
    private static void addOrder(DO.Order order)
    {
        arrayOrder[config.curentIndexOrder++] = order;
    }
    static internal DO.OrderItem[] arrayOrderItem = new DO.OrderItem[SIZE_ARRAY_ORDERITEM];
    private static void addOrderItem(DO.OrderItem orderItem)
    {
        arrayOrderItem[config.curentIndexOrderItem++] = orderItem;
    }
    static internal DO.Product[] arrayProduct = new DO.Product[SIZE_ARRAY_PRODUCT];
    private static void addProduct(DO.Product product)
    {
        arrayProduct[config.curentIndexProduct++] = product;
    }
    private static void s_Initialize()
    {
        int daysForSending, daysForDeliver;
        (DO.enumCategory productCategory, string productName)[] animals = new[] { 
            (DO.enumCategory.dogs, "Sausage Dog"), 
            (DO.enumCategory.cats,"Ragdoll Cat"),
            (DO.enumCategory.fish, "Gold Fish"),  
            (DO.enumCategory.snakes,"Corn Snake"), 
            (DO.enumCategory.snakes,"Milk Snake"), 
            (DO.enumCategory.dogs,"Germans Shepherd Dog"), 
            (DO.enumCategory.fish,"angelfish"), 
            (DO.enumCategory.snakes,"Garter Snake"),
            (DO.enumCategory.dogs,"Labrador Retriever Dog"), 
            (DO.enumCategory.dogs,"Poodle Dog")};

        for (int i = 0; i < 10; i++)
        {
            DO.Product product = new DO.Product();
            int randomProductId = config.ProductId;
            for (int j = 0; j < arrayProduct.Length; j++)
            {
                if (randomProductId == arrayProduct[j].productId)
                {
                    randomProductId = config.ProductId;
                    j = 0;
                }
            }
            product.productId= randomProductId;
            product.productName = animals[i].productName;
            product.productCategory = animals[i].productCategory;
            product.productPrice = random.NextDouble();
            product.productAmountInStock = (int)random.NextInt64(0,20);
            addProduct(product);
        }
        (string clientName, string clientEmail, string addressForDelivery)[] orders = new[] {
            ("Tamar Boyer", "Tamar3758@gmail.com","Uziel 78"),
            ("Rachel Cohen", "Rachel1234@gmail.com","Argov 8"),
            ("Ruty Elyiach", "Ruty4567@gmail.com","David Meretz 72"),
            ("Batya Shapira", "Batya8520@gmail.com","Agasi 1"),
            ("Chana Rotenberg", "Chana7542@gmail.com","Hapisga 29"),
            ("Michal Levy", "Michal8888@gmail.com","Casuto 2"),
            ("Yudit Shub", "Yudit7832@gmail.com","Rashi 111"),
            ("Tzipi Chevroni", "Tzipi2222@gmail.com","Lilach 87"),
            ("Shira Rozenthal", "Shira8543@gmail.com","Kadish Looz 3"),
            ("Esther Ebert", "Esther7544@gmail.com","Bayit Vegan 75"),
            ("Leah Yudlov", "Leah5632@gmail.com","Elyashiv 19"),
            ("Chaya Rosenberg", "Chaya1111@gmail.com","Moshe Zilberg 15"),
            ("Shani Zeevi", "Shani1254@gmail.com","Mutzafi 79"),
            ("Hadassah Teib", "Hadassah4566@gmail.com","Broyer 84"),
            ("Orit Reiter", "Orit7520@gmail.com","Mintz 44"),
            ("Dasi Feld", "Dasi7899@gmail.com","sulam Yaacov 6"),
            ("Shifra Fisher", "Shifra4500@gmail.com","Zolti 96"),
            ("Tzvi Madmon", "Tzvi3333@gmail.com","Fatal 82"),
            ("Yaacov Goldsmidth", "Yaacov5566@gmail.com","Brand 102"),
            ("Yitzchak Omesi", "Yitzchak1010@gmail.com","Druk 73"),};

        for (int i = 0; i < 20; i++)
        {
            daysForSending = (int)random.NextInt64(2, 3);
            daysForDeliver = (int)random.NextInt64(4, 12);
            DO.Order order = new DO.Order();
            order.orderId = config.OrderId;
            order.clientName = orders[i].clientName;
            order.clientEmail = orders[i].clientEmail;
            order.addressForDelivery = orders[i].addressForDelivery;
            order.dateOrdered = DateTime.MinValue;
            TimeSpan tdaysForSending = new TimeSpan(daysForSending, 0, 0, 0);
            order.dateSent = order.dateOrdered.Add(tdaysForSending);
            TimeSpan tdaysForDeliver = new TimeSpan(daysForDeliver, 0, 0, 0);
            order.dateDelivered = order.dateSent.Add(tdaysForDeliver);
            addOrder(order);
        }
   
        for (int i = 0; i < 10; i++)
        {
            int randomOrder = (int)random.NextInt64(0, curentIndexOrder);
            int randomProduct = (int)random.NextInt64(0, curentIndexProduct);
            int randomAmount = (int)random.NextInt64(1, 10);
            DO.OrderItem orderItem = new DO.OrderItem();
            orderItem.orderItemId = config.OrderItemId;
            orderItem.orderId = arrayOrder[randomOrder].orderId;
            orderItem.itemId = arrayProduct[randomProduct].productId;
            orderItem.priceForUnit = arrayProduct[randomProduct].productPrice;
            orderItem.amount = randomAmount;
            addOrderItem(orderItem);
        }

    }

    static internal class config 
    {
       internal static int curentIndexOrder = 0;
       internal static int curentIndexOrderItem = 0;
       internal static int curentIndexProduct = 0;
       static private int productId = (int) random.NextInt64(100000, 1000000);
       static public int ProductId
        {
            get { return productId= (int)random.NextInt64(100000, 1000000);  }
     
        }
        static private int orderId = 0;
        
        static public int OrderId
        {
            get { return orderId++; }
        }
        static private int orderItemId = 0;
        static public int OrderItemId
        {
            get { return orderItemId++; }

        }


    }

}
