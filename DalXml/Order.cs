using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order value)
    {
        if(value.Id == 0)//status of add
        {
            XElement? Config = XDocument.Load("../config.xml").Root;
            XElement? idOrder = Config?.Element("OrderId");
            int id = Convert.ToInt32(idOrder?.Value);
            value.Id = id++;
            idOrder.Value = id.ToString();
            Config?.Save("../config.xml");
        }
        List<DO.Order> lst = GetAll().ToList();
        lst.Add(value);
        StreamWriter write = new StreamWriter("../Order.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        ser.Serialize(write, lst);
        write.Close();
        return value.Id;
    }
    public void Delete(int id)
    {
        List<DO.Order> lst = GetAll().ToList();
        lst.Remove(Get(id));
        StreamWriter w = new StreamWriter("../Order.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        ser.Serialize(w, lst);
        w.Close();
    }
    public void Update(DO.Order value)
    {
        Delete(value.Id);
        Add(value);
    }
    public DO.Order Get(int id)
    {
        List<DO.Order> lst = GetAll().ToList();
        var found = lst.Find(x => x.Id == id);
        if (found.Id == 0)
            throw new ExistException();
        return new DO.Order { Id = found.Id, CustomerName = found.CustomerName, CustomerAddress = found.CustomerAddress, CustomerEmail = found.CustomerEmail, OrderDate = found.OrderDate, ShipDate = found.ShipDate, Delivery = found.Delivery };
    }
    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        List<DO.Order> lst = new List<DO.Order> { };
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader r = new StreamReader("../Order.xml");
        lst = (List<DO.Order>)ser.Deserialize(r);
        r.Close();
        return (func == null) ? lst : lst?.Where(func);
    }
    public DO.Order Get(Predicate<DO.Order> func)
    {
        List<DO.Order> lst = GetAll().ToList();
        return lst.Find(func);
    }
}
