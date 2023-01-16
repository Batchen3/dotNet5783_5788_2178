using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal;

internal class Order:IOrder
{
    public int Add(DO.Order value)
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
    public void Update(DO.Order value)
    {
        int id = 0;
    }
    public DO.Order Get(int id)
    {
        XElement? Products = XDocument.Load("Products.xml").Root;
        var found = Products?.Elements().ToList().Find(product => product?.Element("Id")?.Value.ToString() == id.ToString());
        if (found?.Element("Id")?.Value.ToString() == "0")
            throw new ExistException();
        // return found;
        return new DO.Order();

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
    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        IEnumerable<DO.Order> i = new List<DO.Order>();
        return i;
    }
    public DO.Order Get(Predicate<DO.Order> func)
    {
        return new DO.Order();

    }
}
