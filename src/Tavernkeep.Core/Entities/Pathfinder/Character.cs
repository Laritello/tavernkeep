using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Entities.Pathfinder
{
	[Table("Characters")]
	public class Character : Entity
	{
		#region Constructors

		public Character()
		{
			Health = new(1, 1, 0);
			Armor = new(this);

			Strength = new(this, AbilityType.Strength);
			Dexterity = new(this, AbilityType.Dexterity);
			Constitution = new(this, AbilityType.Constitution);
			Intelligence = new(this, AbilityType.Intelligence);
			Wisdom = new(this, AbilityType.Wisdom);
			Charisma = new(this, AbilityType.Charisma);

			Acrobatics = new(this, AbilityType.Dexterity, SkillType.Acrobatics);
			Arcana = new(this, AbilityType.Intelligence, SkillType.Arcana);
			Athletics = new(this, AbilityType.Strength, SkillType.Athletics);
			Crafting = new(this, AbilityType.Intelligence, SkillType.Crafting);
			Deception = new(this, AbilityType.Charisma, SkillType.Deception);
			Diplomacy = new(this, AbilityType.Charisma, SkillType.Diplomacy);
			Intimidation = new(this, AbilityType.Charisma, SkillType.Intimidation);
			Medicine = new(this, AbilityType.Wisdom, SkillType.Medicine);
			Nature = new(this, AbilityType.Wisdom, SkillType.Nature);
			Occultism = new(this, AbilityType.Intelligence, SkillType.Occultism);
			Performance = new(this, AbilityType.Charisma, SkillType.Performance);
			Religion = new(this, AbilityType.Wisdom, SkillType.Religion);
			Society = new(this, AbilityType.Intelligence, SkillType.Society);
			Stealth = new(this, AbilityType.Dexterity, SkillType.Stealth);
			Survival = new(this, AbilityType.Wisdom, SkillType.Survival);
			Thievery = new(this, AbilityType.Dexterity, SkillType.Thievery);

			Conditions = [];
			Lores = [];
		}

		#endregion

		#region Properties
		public User Owner { get; set; } = default!;

		public string Name { get; set; } = default!;
		public int Level { get; set; }
		public Health Health { get; set; }
		public ArmorClass Armor { get; set; }
		public List<Condition> Conditions { get; set; }

		public Ability Strength { get; set; }
		public Ability Dexterity { get; set; }
		public Ability Constitution { get; set; }
		public Ability Intelligence { get; set; }
		public Ability Wisdom { get; set; }
		public Ability Charisma { get; set; }

		public Skill Acrobatics { get; set; }
		public Skill Arcana { get; set; }
		public Skill Athletics { get; set; }
		public Skill Crafting { get; set; }
		public Skill Deception { get; set; }
		public Skill Diplomacy { get; set; }
		public Skill Intimidation { get; set; }
		public Skill Medicine { get; set; }
		public Skill Nature { get; set; }
		public Skill Occultism { get; set; }
		public Skill Performance { get; set; }
		public Skill Religion { get; set; }
		public Skill Society { get; set; }
		public Skill Stealth { get; set; }
		public Skill Survival { get; set; }
		public Skill Thievery { get; set; }

		public List<Lore> Lores { get; set; }

		#endregion

		#region Methods

		public Ability GetAbility(AbilityType type)
		{
			return type switch
			{
				AbilityType.Strength => Strength,
				AbilityType.Dexterity => Dexterity,
				AbilityType.Constitution => Constitution,
				AbilityType.Intelligence => Intelligence,
				AbilityType.Wisdom => Wisdom,
				AbilityType.Charisma => Charisma,
				_ => throw new NotImplementedException(),
			};
		}

		public Skill GetSkill(SkillType type)
		{
			return type switch
			{
				SkillType.Acrobatics => Acrobatics,
				SkillType.Deception => Deception,
				SkillType.Arcana => Arcana,
				SkillType.Athletics => Athletics,
				SkillType.Crafting => Crafting,
				SkillType.Diplomacy => Diplomacy,
				SkillType.Intimidation => Intimidation,
				SkillType.Medicine => Medicine,
				SkillType.Nature => Nature,
				SkillType.Occultism => Occultism,
				SkillType.Performance => Performance,
				SkillType.Religion => Religion,
				SkillType.Society => Society,
				SkillType.Stealth => Stealth,
				SkillType.Survival => Survival,
				SkillType.Thievery => Thievery,
				_ => throw new NotImplementedException(),
			};
		}

		public void AddCondition(Condition condition)
		{
			Conditions.Add(condition);
		}

		#endregion
	}
}
