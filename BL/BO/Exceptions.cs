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
public class NotValidException : Exception
{
    public override string Message => "one or more parameters are not valid";
}
