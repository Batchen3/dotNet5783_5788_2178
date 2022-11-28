using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OutOfStockException: Exception//Out Of Stock Exception
{
    public override string Message => "out of stock";
}

public class ObjectNotFoundException : Exception//Object Not Found Exception
{
   public override string Message => "object not found";
}

public class DalException : Exception//Dal Exception
{
    public DalException(Exception ex) : base("exception in dal", ex) { }
}

public class NotValidException : Exception//Not Valid Exception
{
    public override string Message => "one or more parameters are not valid";
}
public class ProductInOrderException : Exception//Product In Order Exception
{
    public override string Message => "can't delete: the product in order";
}
public class DateWasException : Exception//Date Was Exception
{
    public override string Message => "the date was already";
}
public class DatesNotInCorrectOrderException : Exception//Date Was Exception
{
    public override string Message => "dates not in correct order";
}