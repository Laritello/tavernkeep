﻿using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers;

namespace Tavernkeep.Core.Contracts.Interfaces
{
	public interface IModifierManager
	{
		public ModifierTarget Target { get; }
		public ModifierSummary GetSummary();
	}
}
