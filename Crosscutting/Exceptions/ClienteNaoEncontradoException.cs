namespace Crosscutting.Exceptions;

public class ClienteNaoEncontradoException : Exception
{
    public ClienteNaoEncontradoException() :
        base("Cliente não encontrado.")
    {
    }

    public ClienteNaoEncontradoException(string message) :
        base(message)
    {
    }

    public ClienteNaoEncontradoException(string message, Exception inner) :
        base(message, inner)
    {
    }
}