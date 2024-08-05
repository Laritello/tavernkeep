﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Modifiers.Managers;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities
{
    public class Skill : IModifiable
    {
        #region Backing fields

        private IModifierManager _manager;

		#endregion
		#region Constructors

		public Skill() 
        {

        }

        public Skill(Character owner, AbilityType baseAbility, SkillType type)
        {
            Owner = owner;
            BaseAbility = baseAbility;
            Type = type;
        }

        #endregion

        #region Properties

        [JsonIgnore]
        public Character Owner { get; set; } = default!;

        [JsonIgnore]
        [NotMapped]
        public IModifierManager Manager => _manager ??= new SkillModifierManager(Owner, Type);
        public AbilityType BaseAbility { get; set; }
        public SkillType Type { get; set; }
        public Proficiency Proficiency { get; set; }
        public int Bonus => Owner.GetSkillAbility(Type).Modifier + Proficiency.GetProficiencyBonus(Owner) + Manager.GetSummary().Total;

        #endregion

        #region Methods

        public SkillSnapshot AsSnapshot()
        {
            return new()
            {
                Type = Type,
                Proficiency = Proficiency,
                Bonus = Bonus
            };
        }

        #endregion
    }
}
