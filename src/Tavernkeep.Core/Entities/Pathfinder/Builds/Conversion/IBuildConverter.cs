namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion
{
	public interface IBuildConverter<T>
	{
		public T Convert();
	}
}
