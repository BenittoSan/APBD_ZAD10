namespace CodeFirst.Exceptions;

public class DomainException : Exception
{
    public DomainException() 
    {
        
    }

    public DomainException(string? messege) : base( messege)
    {
        
    }
}