namespace Crosscutting.Exceptions;

public class ContaNaoEncontradaException : Exception
{
    public ContaNaoEncontradaException(string message) : base(message)
    {
    }
}