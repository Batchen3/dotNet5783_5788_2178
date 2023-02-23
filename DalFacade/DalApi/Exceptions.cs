using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public class NoSuchObjectException : Exception//No Such Object Exception
{
    public override string Message => "An error occurred no such object";
}
public class NullException : Exception//value with null Exception
{
    public override string Message => "An error occurred there is a value empty";
}
public class ExistException : Exception//Exist Exception
{
    public override string Message => "An error occurred the object alredy exist";
}
public class FullListException : Exception   // Exception of full list
{
    public override string Message => "An error occurred the list is full";
}
