public class FlightNotFoundException : Exception
{
    public FlightNotFoundException()
    {
    }

    public FlightNotFoundException(string message) : base(message)
    {
    }

    public FlightNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}