using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public class NoSuchObjectException : Exception//No Such Object Exception
{
    public override string Message => "no such object";
}
public class ExistException : Exception//Exist Exception
{
    public override string Message => "the object alredy exist";
}
public class FullListException : Exception   // Exception of full list
{
    public override string Message => "the list is full";
}
