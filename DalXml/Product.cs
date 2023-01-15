using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class Product : IProduct
{
    public int Add(DO.Product value)
    {
        XElement? Products = XDocument.Load("Products.xml").Root;
        var list = Products?.Elements().ToList().Where(product => product?.Element("Id")?.Value.ToString() == value.Id.ToString());
        if (list?.Count() > 0)
            throw new ExistException();
        XElement? product = new XElement("Product");
        XElement? Id = new XElement("Id", value.Id);
        product.Add(Id);
        XElement? Name = new XElement("Name", value.Name);
        product.Add(Name);
        XElement? Price = new XElement("Price", value.Price);
        product.Add(Price);
        XElement? Category = new XElement("Category", value.Category);
        product.Add(Category);
        XElement? InStock = new XElement("InStock", value.InStock);
        product.Add(InStock);
        XElement? Parve = new XElement("Parve", value.Parve);
        product.Add(Parve);
        Products?.Add(product);
        Products?.Save("Products.xml");
        return value.Id;

    }
    public void Delete(int id)
    {

        id = 0;
    }
    public void Update(DO.Product value)
    {
        int id = 0;
    }
    public DO.Product Get(int id)
    {
        XElement? Products = XDocument.Load("Products.xml").Root;
        var found = Products?.Elements().ToList().Find(product => product?.Element("Id")?.Value.ToString() ==id.ToString());
        if (found?.Element("Id")?.Value.ToString()=="0")
            throw new ExistException();
        return found;


        List<DO.Product> lst= new List<DO.Product> { };
        //DO.Product p;
        StreamReader sr = new StreamReader("Products.xml");
        XmlSerializer ser= new XmlSerializer(typeof(List<Product>));
        lst = ser.Deserialize(sr);

        //StreamWriter w = new StreamWriter("products.xml");
       // XmlSerializer ser = new XmlSerializer(typeof(List<Product>));

        //ser.Serialize(lst, w);

       // StreamReader r = new StreamReader();

        //lst = ser.Deserialize(r)
          //  w.Close();

    }
    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        IEnumerable<DO.Product> i = new List<DO.Product>();
        return i;
    }
    public DO.Product Get(Predicate<DO.Product> func)
    {
        return new DO.Product();

    }
}
