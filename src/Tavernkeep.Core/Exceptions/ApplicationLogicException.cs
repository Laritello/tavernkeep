namespace Tavernkeep.Core.Exceptions
{
    public class ApplicationLogicException : Exception
    {
        public ApplicationLogicException(string message) : base(message) { }
        public ApplicationLogicException(string message, Exception innerException) : base(message, innerException) { }
    }
}
