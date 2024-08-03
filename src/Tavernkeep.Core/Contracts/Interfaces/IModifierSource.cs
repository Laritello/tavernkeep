using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Contracts.Interfaces
{
	public interface IModifierSource
    {
        public string Name { get; }
        public List<Modifier> Modifiers { get; }
    }
}
