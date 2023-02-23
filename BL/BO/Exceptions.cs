using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OutOfStockException: Exception//Out Of Stock Exception
{
    public override string Message => "An error occurred out of stock";
}

public class ObjectNotFoundException : Exception//Object Not Found Exception
{
   public override string Message => "An error occurred object not found";
}

public class DalException : Exception//Dal Exception
{
    public DalException(Exception ex) : base("exception in dal", ex) { }
}

public class NotValidException : Exception//Not Valid Exception
{
    public override string Message => "An error occurred one or more parameters are not valid";
}
public class ProductInOrderException : Exception//Product In Order Exception
{
    public override string Message => "An error occurred can't delete: the product in order";
}
public class DateWasException : Exception//Date Was Exception
{
    public override string Message => "An error occurred the date was already";
}
public class DatesNotInCorrectOrderException : Exception//Date Was Exception
{
    public override string Message => "An error occurred dates not in correct order";
}
public class ObjectNotInCartException : Exception//Object Not In Cart Exception
{
    public override string Message => "An error occurred product not in cart";
}