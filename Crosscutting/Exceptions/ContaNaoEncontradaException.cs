namespace Crosscutting.Exceptions;

public class ContaNaoEncontradaException : Exception
{
<<<<<<< Updated upstream
    public ContaNaoEncontradaException() :
        base("Conta não encontrada.")
    {
    }

    public ContaNaoEncontradaException(string message) :
        base(message)
    {
    }

    public ContaNaoEncontradaException(string message, Exception inner) :
        base(message, inner)
=======
    public ContaNaoEncontradaException(string message) : base(message)
>>>>>>> Stashed changes
    {
    }
}