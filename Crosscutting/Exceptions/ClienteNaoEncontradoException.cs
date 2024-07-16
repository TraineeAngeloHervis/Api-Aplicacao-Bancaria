namespace Crosscutting.Exceptions;

public class ClienteNaoEncontradoException : Exception
{
<<<<<<< Updated upstream
    public ClienteNaoEncontradoException() :
        base("Cliente não encontrado.")
    {
    }

    public ClienteNaoEncontradoException(string message) :
        base(message)
    {
    }

    public ClienteNaoEncontradoException(string message, Exception inner) :
=======
    public ClienteNaoEncontradoException() : 
        base("Cliente não encontrado.")
    {
    }
    
    public ClienteNaoEncontradoException(string message) : 
        base(message)
    {
    }
    
    public ClienteNaoEncontradoException(string message, Exception inner) : 
>>>>>>> Stashed changes
        base(message, inner)
    {
    }
}