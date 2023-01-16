using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        XElement? Products = XDocument.Load("../Product.xml").Root;
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
        Products?.Save("../Product.xml");
        return value.Id;

    }
    public void Delete(int id)
    {
        XElement? Products = XDocument.Load("../Product.xml").Root;
        DO.Product pDelete=Get(id);
        IEnumerable<DO.Product> lst = GetAll();
        int index= lst.ToList().IndexOf(pDelete);



        //Products.documentElement.removeChild
        //Products?.Remove(pDelete);

        //XElement? product = new XElement("Product");
        //XElement? Id = new XElement("Id", value.Id);
        //product.Add(Id);
        //XElement? Name = new XElement("Name", value.Name);
        //product.Add(Name);
        //XElement? Price = new XElement("Price", value.Price);
        //product.Add(Price);
        //XElement? Category = new XElement("Category", value.Category);
        //product.Add(Category);
        //XElement? InStock = new XElement("InStock", value.InStock);
        //product.Add(InStock);
        //XElement? Parve = new XElement("Parve", value.Parve);
        //product.Add(Parve);
        //Products?.Elements().Remove(product);
        id = 0;
    }
    public void Update(DO.Product value)
    {
       
    }
    public DO.Product Get(int id)
    {
        XElement? Products = XDocument.Load("../Product.xml").Root;
        var found = Products?.Elements().ToList().Find(product => Convert.ToInt32(product?.Element("Id")?.Value) == id);
        if (found == null)
            throw new ExistException();
        DO.ECategory.TryParse(found?.Element("Category")?.Value, out DO.ECategory myCategory); ;
        return new DO.Product { Id = Convert.ToInt32(found?.Element("Id")?.Value), Name = found?.Element("Name")?.Value.ToString(), InStock = Convert.ToInt32(found?.Element("InStock")?.Value), Category = myCategory, Parve = Convert.ToBoolean(found?.Element("Parve")?.Value), Price = Convert.ToInt32(found?.Element("Price")?.Value) };

        //List<DO.Product> lst= new List<DO.Product> { };
        ////DO.Product p;
        //StreamReader sr = new StreamReader("Products.xml");
        //XmlSerializer ser= new XmlSerializer(typeof(List<Product>));
        //lst = ser.Deserialize(sr);
    }
    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {

        XElement? Products = XDocument.Load("../Product.xml").Root;
        List<DO.Product> lst = new List<DO.Product> { };
        Products?.Elements().ToList().ForEach(e =>
        {
            DO.ECategory.TryParse(e?.Element("Category")?.Value, out DO.ECategory myCategory); ;
            lst.Add(new DO.Product { Id = Convert.ToInt32(e?.Element("Id")?.Value), Name = e?.Element("Name")?.Value.ToString(), InStock = Convert.ToInt32(e?.Element("InStock")?.Value), Category = myCategory, Parve = Convert.ToBoolean(e?.Element("Parve")?.Value), Price = Convert.ToInt32(e?.Element("Price")?.Value) });
        });

        return func == null ? lst : lst.Where(func);
        //List<DO.Product> lst = new List<DO.Product> { };
        //StreamReader sr = new StreamReader("../Product.xml");
        //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        //lst = (List<DO.Product>)ser.Deserialize(sr);
        //sr.Close();
        //return lst;
    }
    public DO.Product Get(Predicate<DO.Product> func)
    {
        IEnumerable<DO.Product> lst = GetAll();
        return lst.ToList().Find(func);
    }
}
