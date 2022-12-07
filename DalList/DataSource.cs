//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


using DO;
using System;
using System.Xml.Linq;

namespace Dal;

static internal class DataSource
{
    static DataSource()
    {
        s_initialize();
    }
    static internal class Config
    {

        private static int idOrder = 88417;
        static public int IdOrder
        {
            get { return idOrder; }
            set { idOrder = value; }
        }

        private static int idOrderItem = 5223122;
        static public int IdOrderItem
        {
            get { return idOrderItem; }
            set { idOrderItem = value; }
        }
    }

  
    static readonly Random rand = new Random();
    internal const int NUMPRODUCTS = 50;
    internal const int NUMORDERS = 100;
    internal const int NUMORDERITEM = 200;
    static internal List<Product> s_listProduct = new List<Product> { };
    static internal List<Order> s_listOrder = new List<Order> { };
    static internal List<OrderItem> s_listOrderItem=new List<OrderItem> { };
    private static void addProduct(Product p)
    {
            s_listProduct.Add(p);     
    }
    private static void addOrder(Order o)
    {
        s_listOrder.Add(o);
    }
    private static void addOrderItem(OrderItem oi)
    {
        s_listOrderItem.Add(oi);
    }
    private static void s_initialize()
    {
        int index, daysShip, daysDelivery, id;
        (string, ECategory)[] tInfoOfProduct = {("mousse",ECategory.cups),
          ("chocolate_balls",ECategory.cups),
          ("Cheesecake",ECategory.cakes),
          ("kurason",ECategory.cookies),
          ("rogalach",ECategory.cookies),
          ("makaroon",ECategory.cookies),
          ("alfachores",ECategory.cookies),
          ("mousse_cake",ECategory.cakes),
          ("oreo_cups",ECategory.cups),
          ("lotus_cups",ECategory.cups)};


        for (int i = 0; i < 10; i++)
        {

            index = (int)rand.Next(10);
            id = (int)rand.Next(100000, 999999);
            bool flag = false;//checks if there are two equal ids
            bool flag2 = true;
            while (!flag)
            {
                for (int j = 0; j < s_listProduct.Count && flag2; j++)
                {
                    if (s_listProduct[j].Id == id)
                        flag2 = false;
                }
                if (!flag2)
                    id = (int)rand.Next(100000, 999999);
                else
                {
                    flag = true;
                }
            }
            Product p = new Product();
            p.Id = id;
            p.Name = tInfoOfProduct[index].Item1;
            p.Price = ((double)rand.NextDouble() + 0.05) * 100;
            p.Category = tInfoOfProduct[index].Item2;
            p.InStock = (int)rand.Next(0, 100);
            p.Parve = (int)rand.Next(2);
            addProduct(p);
        }

        (string, string, string)[] tInfoOfOrder = {("ayala_miler","ayala@gmail.com","rashi_4"),
            ("yael_choen","yael@gmail.com","moshe_raz_9"),
           ("miri_levi","miri@gmail.com","brand_3"),
           ("shira_hever","shira_hever@gmail.com","hapisga_8"),
          ("leha_kaz","leha_kaz@gmail.com","mozafi_9"),
          ("chaya_tov","chaya@gmail.com","eliashiv_55"),
          ("shani_tyb","shani_tyb@gmail.com","mintz_43"),
          ("orit_raiter","orit_raiter@gmail.com","sulam_taakov_2"),
          ("hadasa_zehavi","hadasa_zehavi@gmail.com","yigal_9"),
           ("david_fisher","david_fisher@gmail.com","tlalim_4"),
        ("yedidia_madmon","yedidia_madmon@gmail.com","revivim_34"),
            ("yaakov_omesi","yaakov_omesi@gmail.com","hadaf_hayomi_7"),
           ("batya_boier","batya_boier@gmail.com","fatal_6"),
           ("miriam_erlanger","miriam_erlanger@gmail.com","druk_5"),
          ("efrat_yelin","efrat_yelin@gmail.com","chavakuk_3"),
          ("tamar_bloyi","tamar_bloyi@gmail.com","yirmiyahoo_11"),
          ("shirel_bashari","shirel_bashari@gmail.com","king_david_1"),
          ("menachem_gros","menachem_gros@gmail.com","zolti_40"),
          ("shifra_levi","shifra_levi@gmail.com","rozental_88"),
           ("tzvi_hevert","tzvi_hevert@gmail.com","chahaneman_78")};

        for (int i = 0; i < tInfoOfOrder.Length; i++)
        {
            Order o = new Order();
            index = (int)rand.Next(20);
            daysShip = (int)rand.Next(1, 3);
            daysDelivery = (int)rand.Next(3, 7);
            o.Id = Config.IdOrder++;
            o.CustomerName = tInfoOfOrder[index].Item1;
            o.CustomerEmail = tInfoOfOrder[index].Item2;
            o.CustomerAddress = tInfoOfOrder[index].Item3;
            o.OrderDate = DateTime.Now;
            if (i < tInfoOfOrder.Length * 0.2)//20% with just order date
            {
                o.ShipDate = DateTime.MinValue;
                o.Delivery = DateTime.MinValue;
            }
            else
            {
                TimeSpan tDaysShip = new TimeSpan(daysShip, 0, 0, 0);
                o.ShipDate = o.OrderDate.Add(tDaysShip);
                if (i < tInfoOfOrder.Length * 0.2 + (tInfoOfOrder.Length * 0.8 * 0.6))//60% of 80% with order, ship and delivery dates.
                {
                    TimeSpan tDaysDelivery = new TimeSpan(daysDelivery, 0, 0, 0);
                    o.Delivery = o.OrderDate.Add(tDaysDelivery);
                }
                else
                    o.Delivery = DateTime.MinValue;//the other with just order and ship dates.
            }
            addOrder(o);
        }

        for (int i = 0; i < 20; i++)//doing item to evrey order.
        {
            OrderItem oi = new OrderItem();
            index = (int)rand.Next(10);
            oi.Id = Config.IdOrderItem++;
            oi.ProductID = s_listProduct[index].Id;
            oi.OrderID = s_listOrder[i].Id;
            oi.Price = s_listProduct[index].Price;
            oi.Amount = (int)rand.Next(30);
            addOrderItem(oi);
        }
        int counter = 0;
        for (int i = 20; i < 40;)//adding items to order not more than 3 items
        {
            OrderItem oi2 = new OrderItem();
            index = (int)rand.Next(1, 4);
            for (int j = 0; j < index; j++)
            {
                oi2.OrderID = s_listOrder[counter].Id;
                int iProduct = (int)rand.Next(10);
                oi2.Id = Config.IdOrderItem++;
                oi2.ProductID = s_listProduct[iProduct].Id;
                oi2.Price = s_listProduct[iProduct].Price;
                oi2.Amount = (int)rand.Next(30);
                addOrderItem(oi2);
            }
            i += index;
            counter++;
        }
    }
}
