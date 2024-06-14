namespace CodeFirst.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException(){}

    public AuthorizationException(string? messege) : base(messege)
    {
        
    }
}