using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	/// <summary>
	/// Represents a request to edit the saving throw of a character.
	/// </summary>
	public class EditSavingThrowRequest
	{
		/// <summary>
		/// The ID of the character.
		/// </summary>
		public Guid CharacterId { get; set; } = default!;

		/// <summary>
		/// The type of the saving throw.
		/// </summary>
		public SavingThrowType Type { get; set; } = default!;

		/// <summary>
		/// The proficiency of the saving throw.
		/// </summary>
		public Proficiency Proficiency { get; set; } = default!;
	}
}
