using System;

namespace StubbingDemo.Exceptions;

public class ShipperNotFoundException : Exception
{
    public ShipperNotFoundException() : base("Shipper not found")
    {
    }

    public ShipperNotFoundException(string message) : base(message)
    {
    }

    public ShipperNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }  

}
