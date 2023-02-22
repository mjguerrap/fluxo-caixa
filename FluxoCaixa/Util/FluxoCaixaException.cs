using System.Runtime.Serialization;
namespace FluxoCaixa.Util;

[Serializable]
public class FluxoCaixaException : Exception
{
    public FluxoCaixaException() : base() { }
    public FluxoCaixaException(string? message) : base(message) { }
    public FluxoCaixaException(string? message, Exception? innerException) : base(message, innerException) { }
    protected FluxoCaixaException(SerializationInfo info, StreamingContext context): base(info, context){} 
}
