using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Dal;

internal class Program
{
    public static void Main(string[] args)
    {
       IDal? dalList = DalApi.Factory.Get();

       // var y = dalList?.OrderItem.GetAll();
        DO.Order order = new DO.Order { Id=88439,CustomerName="aaa",CustomerAddress="aaa",CustomerEmail="aaa@aa.aa", OrderDate=DateTime.Now};
        dalList?.Order.Update(order);
        //DO.Order order1 = new DO.Order { Id=11111,CustomerName="aaa",CustomerAddress="aaa",CustomerEmail="aaa@aa.aa", OrderDate=DateTime.Now};
        // DO.OrderItem aa = new DO.OrderItem();
        //var e= dalList?.OrderItem.Add(aa);
        //var y = dalList?.Order.Get(88417);
        //var yw = dalList?.Order.Add(order1);


        // var x = dalList?.Product.GetAll();
        //var x=dalList?.Product.GetAll();
        //IEnumerable<DO.Order> lst = dalList?.Order.GetAll();
        //StreamWriter w = new StreamWriter("../Orders.xml");
        //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        //ser.Serialize(w, lst);
    }

}
