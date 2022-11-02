//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


using DO;
using System;

namespace Dal;

static internal class DataSource
{
    static DataSource()
    {
        s_initialize();
    }

    static readonly Random rand = new Random();
    const int NUMPRODUCTS = 50;
    static internal Product[] arrayProduct = new Product[NUMPRODUCTS];
    const int NUMORDERS = 100;
    static internal Order[] arrayOrder = new Order[NUMORDERS];
    const int NUMORDERITEM = 200;
    static internal OrderItem[] arrayOrderItem = new OrderItem[NUMORDERITEM];
    private static void addProduct(Product p)
    {
        if (Config.moneProduct > arrayProduct.Length)
            Console.WriteLine("arrayProduct is full");
        else
        {
            arrayProduct[Config.moneProduct] = p;
            Config.moneProduct++;
        }
    }
    private static void addOrder(Order o)
    {
        if (Config.moneOrder > arrayOrder.Length)
            Console.WriteLine("arrayOrder is full");
        else
        {
            arrayOrder[Config.moneOrder] = o;
            Config.moneOrder++;
        }
    }
    private static void addOrderItem(OrderItem oi)
    {
        if (Config.moneOrderItem > arrayOrderItem.Length)
            System.Console.WriteLine("arrayOrderItem is full");
        else
        {
            arrayOrderItem[Config.moneOrderItem] = oi;
            Config.moneOrderItem++;
        }
    }


    private static void s_initialize()
    {
        int index, daysShip, daysDelivery, id;
        (string, ECategory)[] tInfoOfProduct = new[] {("mousse",ECategory.cups),
          ("chocolate_balls",ECategory.cups),
          ("Cheesecake",ECategory.cakes),
          ("kurason",ECategory.cookies),
          ("rogalach",ECategory.cookies),
          ("makaroon",ECategory.cookies),
          ("alfachores",ECategory.cookies),
          ("mousse_cake",ECategory.cakes),
          ("oreo_cups",ECategory.cups),
          ("lotus_cups",ECategory.cups)};


        for (int i = 0; i < tInfoOfProduct.Length; i++)
        {
            Product p = new Product();
            index = (int)rand.NextInt64(10);
            id = (int)rand.NextInt64(100000, 999999);
            bool flag = false;//checks if there are two equal ids
            bool flag2 = true;
            while (!flag)
            {
                for (int j = 0; j < Config.moneProduct && flag2; j++)
                {
                    if (arrayOrder[j].id == p.id)
                        flag2 = false;
                }
                if (!flag2)
                    id = (int)rand.NextInt64(100000, 999999);
                else
                {
                    flag = true;
                }
            }
            p.id = id;
            p.name = tInfoOfProduct[index].Item1;
            p.price = ((double)rand.NextDouble() + 0.05) * 100;
            p.category = tInfoOfProduct[index].Item2;
            p.inStock = (int)rand.NextInt64(10, 100);
            p.parve = (int)rand.NextInt64(1);
            addProduct(p);
        }

        (string, string, string)[] tInfoOfOrder = new[] {("ayala_miler","ayala@gmail.com","rashi_4"),
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
            index = (int)rand.NextInt64(10);
            daysShip = (int)rand.NextInt64(1, 3);
            daysDelivery = (int)rand.NextInt64(3, 7);
            o.id = Config.IdOrder;
            o.customerName = tInfoOfOrder[index].Item1;
            o.customerEmail = tInfoOfOrder[index].Item2;
            o.customerAddress = tInfoOfOrder[index].Item3;
            o.orderDate = DateTime.Now;
            if (i < tInfoOfOrder.Length * 0.2)//20% with just order date
            {
                o.shipDate = DateTime.MinValue;
                o.delivery = DateTime.MinValue;
            }
            else
            {
                TimeSpan tDaysShip = new TimeSpan(daysShip, 0, 0, 0);
                o.shipDate = o.orderDate.Add(tDaysShip);
                if (i < tInfoOfOrder.Length * 0.2 + (tInfoOfOrder.Length * 0.8 * 0.6))//60% of 80% with order, ship and delivery dates.
                {
                    TimeSpan tDaysDelivery = new TimeSpan(daysDelivery, 0, 0, 0);
                    o.delivery = o.orderDate.Add(tDaysDelivery);
                }
                else
                    o.delivery = DateTime.MinValue;//the other with just order and ship dates.
            }
            addOrder(o);
        }

        for (int i = 0; i < 20; i++)//doing item to evrey order.
        {
            OrderItem oi = new OrderItem();
            index = (int)rand.NextInt64(10);
            oi.id = Config.IdOrderItem;
            oi.productID = arrayProduct[index].id;
            oi.orderID = arrayOrder[i].id;
            oi.price = arrayProduct[index].price;
            oi.amount = (int)rand.NextInt64(30);
            addOrderItem(oi);
        }
        int counter = 0;
        for (int i = 20; i < 40; i++)//adding items to order not more than 3 items
        {
            OrderItem oi = new OrderItem();
            index = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < index; j++)
            {
                oi.orderID = arrayOrder[counter].id;
                int iProduct = (int)rand.NextInt64(Config.moneProduct);
                oi.id = Config.IdOrderItem;
                oi.productID = arrayProduct[iProduct].id;
                oi.price = arrayProduct[iProduct].price;
                oi.amount = (int)rand.NextInt64(30);
                addOrderItem(oi);
            }
            i = +index;
            counter++;
        }
    }

    static internal class Config
    {
        internal static int moneProduct = 0;
        internal static int moneOrder = 0;
        internal static int moneOrderItem = 0;

        private static int idOrder = 88417;
        static public int IdOrder
        {
            get { return idOrder++; }
        }

        private static int idOrderItem = 5223122;
        static public int IdOrderItem
        {
            get { return idOrderItem++; }
        }
    }


}
