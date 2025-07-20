namespace Tavernkeep.Domain.Characters;

public readonly record struct CharacterId(Guid Value)
{
	public static CharacterId Empty => new(Guid.Empty);
}
