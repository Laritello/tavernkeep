using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Character.Dtos;

public class SavingThrowDto
{
	public required string Name { get; set; }
	public required Proficiency Proficiency { get; set; }
	public int Bonus { get; set; }
}
