using System;

namespace StubbingDemo;

public class CouldNotAddToDatabaseException : Exception
{
    public CouldNotAddToDatabaseException(string message) : base(message)
    {
    }

    public CouldNotAddToDatabaseException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public CouldNotAddToDatabaseException()
    {
    }
}
