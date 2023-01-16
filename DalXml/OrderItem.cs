using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;
using DalApi;


namespace Dal;
internal class OrderItem:IOrderItem
{
    public int Add(DO.OrderItem value)
    {
        XElement? Products = XDocument.Load("Products.xml").Root;
        var list = Products?.Elements().ToList().Where(product => product?.Element("Id")?.Value.ToString() == value.Id.ToString());
        if (list?.Count() > 0)
            throw new ExistException();
        Products?.Save("Products.xml");
        return value.Id;

    }
    public void Delete(int id)
    {

        id = 0;
    }
    public void Update(DO.OrderItem value)
    {
        int id = 0;
    }
    public DO.OrderItem Get(int id)
    {
        XElement? Products = XDocument.Load("Products.xml").Root;
        var found = Products?.Elements().ToList().Find(product => product?.Element("Id")?.Value.ToString() == id.ToString());
        if (found?.Element("Id")?.Value.ToString() == "0")
            throw new ExistException();
        // return found;
        return new DO.OrderItem();

        //List<DO.Product> lst= new List<DO.Product> { };
        ////DO.Product p;
        //StreamReader sr = new StreamReader("Products.xml");
        //XmlSerializer ser= new XmlSerializer(typeof(List<Product>));
        //lst = ser.Deserialize(sr);











        //StreamWriter w = new StreamWriter("products.xml");
        // XmlSerializer ser = new XmlSerializer(typeof(List<Product>));

        //ser.Serialize(lst, w);

        // StreamReader r = new StreamReader();

        //lst = ser.Deserialize(r)
        //  w.Close();

    }
    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        IEnumerable<DO.OrderItem> i = new List<DO.OrderItem>();
        return i;
    }
    public DO.OrderItem Get(Predicate<DO.OrderItem> func)
    {
        return new DO.OrderItem ();

    }
}
