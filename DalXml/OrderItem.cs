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
internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem value)
    {
        if (value.Id == 0)//status of add
        {
            XElement? Config = XDocument.Load("../Config.xml").Root;
            XElement? orderItemId = Config?.Element("OrderItemId");
            int id = Convert.ToInt32(orderItemId?.Value);
            value.Id = id++;
            orderItemId.Value = id.ToString();
            Config?.Save("../config.xml");
        }
        XElement? OrdersItems = XDocument.Load("../OrderItem.xml").Root;
        var list = OrdersItems?.Elements().ToList().Where(orderItem => Convert.ToInt32(orderItem?.Element("Id")?.Value) == value.Id);
        if (list?.Count() > 0)
            throw new ExistException();
        XElement? orderItem = new XElement("OrderItem");
        XElement? Id = new XElement("Id", value.Id);
        orderItem.Add(Id);
        XElement? ProductId = new XElement("ProductID", value.ProductID);
        orderItem.Add(ProductId);
        XElement? OrderID = new XElement("OrderID", value.OrderID);
        orderItem.Add(OrderID);
        XElement? Price = new XElement("Price", value.Price);
        orderItem.Add(Price);
        XElement? Amount = new XElement("Amount", value.Amount);
        orderItem.Add(Amount);
        OrdersItems?.Add(orderItem);
        OrdersItems?.Save("../OrderItem.xml");
        return value.Id;
    }
    public void Delete(int id)
    {
        XElement? OrdersItems = XDocument.Load("../OrederItem.xml").Root;
        OrdersItems?.Elements().ToList().Find(orderItem => Convert.ToInt32(orderItem?.Element("Id")?.Value) == id)?.Remove();
        OrdersItems?.Save("../OrederItem.xml");
    }
    public void Update(DO.OrderItem value)
    {
        Delete(value.Id);
        Add(value);
    }
    public DO.OrderItem Get(int id)
    {
        XElement? OrdersItems = XDocument.Load("../OrderItem.xml").Root;
        var found = OrdersItems?.Elements().ToList().Find(OrderItem => Convert.ToInt32(OrderItem?.Element("Id")?.Value) == id);
        if (found == null)
            throw new ExistException();
        return new DO.OrderItem { Id = Convert.ToInt32(found?.Element("Id")?.Value), ProductID = Convert.ToInt32(found?.Element("ProductID")?.Value), OrderID = Convert.ToInt32(found?.Element("OrderID")?.Value), Price = Convert.ToInt32(found?.Element("Price")?.Value), Amount = Convert.ToInt32(found?.Element("Amount")?.Value) };
    }
    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        XElement? OrdersItems = XDocument.Load("../OrderItem.xml").Root;
        List<DO.OrderItem> lst = new List<DO.OrderItem> { };
        OrdersItems?.Elements().ToList().ForEach(o =>
        {
            lst.Add(new DO.OrderItem { Id = Convert.ToInt32(o?.Element("Id")?.Value), ProductID = Convert.ToInt32(o?.Element("ProductID")?.Value), OrderID = Convert.ToInt32(o?.Element("OrderID")?.Value), Price = Convert.ToInt32(o?.Element("Price")?.Value), Amount = Convert.ToInt32(o?.Element("Amount")?.Value) });
        });
        return func == null ? lst : lst.Where(func);
    }
    public DO.OrderItem Get(Predicate<DO.OrderItem> func)
    {
        IEnumerable<DO.OrderItem> lst = GetAll();
        return lst.ToList().Find(func);
    }
}

