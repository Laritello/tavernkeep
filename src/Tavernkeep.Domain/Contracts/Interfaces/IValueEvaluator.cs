namespace Tavernkeep.Domain.Contracts.Interfaces
{
	public interface IValueEvaluator<T>
	{
		public T Value { get; }
	}
}
