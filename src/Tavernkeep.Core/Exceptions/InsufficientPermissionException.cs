namespace Tavernkeep.Core.Exceptions
{
	public class InsufficientPermissionException : Exception
	{
		public InsufficientPermissionException(string message) : base(message) { }
		public InsufficientPermissionException(string message, Exception innerException) : base(message, innerException) { }
	}
}
