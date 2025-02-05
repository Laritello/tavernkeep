using Tavernkeep.Core.Contracts.Character.Dtos;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the skills of a character.
	/// </summary>
	public class EditSkillsRequest
	{
		/// <summary>
		/// The updated proficiencies.
		/// </summary>
		public Dictionary<string, SkillEditDto> Skills { get; set; } = [];
	}
}
