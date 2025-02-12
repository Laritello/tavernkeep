using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Extensions
{
	public static class EnumExtensions
	{
		public static bool IsEarlierThan(this EncounterStatus source, EncounterStatus destination)
		{
			return (int) source < (int) destination;
		}
	}
}
