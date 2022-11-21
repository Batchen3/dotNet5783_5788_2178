using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OutOfStockException:Exception
{
    public override string Message => "out of stock";
}

public class ObjectNotFoundException : Exception
{
   public override string Message => "object not found";
}

public class DalException : Exception
{
    public DalException(Exception ex) : base("exception in dal", ex) { }
    public override string Message => "exception in dal";
}


public class NotValidException : Exception
{
    public override string Message => "one or more parameters are not valid";
}
