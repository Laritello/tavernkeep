using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Entities.Library.Creatures;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Extensions;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.AddParticipant
{
	public class AddCreatureEncounterParticipantStrategy(ICreatureLibraryRepository creatureRepository) : IAddEncounterParticipantStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Creature;
		public async Task AddParticipantAsync(Encounter encounter, Guid entityId, CancellationToken cancellationToken)
		{
			var creature = await creatureRepository.GetCreatureAsync(entityId, cancellationToken)
				?? throw new BusinessLogicException("Creature with specified ID not found");

			CreatureEncounterParticipant participant = new()
			{
				Encounter = encounter,
				Origin = creature,
				CurrentHealth = creature.Health,
				TemporaryHealth = 0,
			};

			participant.Creature = CreateFromOrigin(participant);

			encounter.AddParticipant(participant);
		}

		private Creature CreateFromOrigin(CreatureEncounterParticipant creature)
		{
			var origin = creature.Origin;

			var result = new Creature
			{
				Id = origin.Id,
				Type = origin.Type,
				Name = origin.Name,
				Level = origin.Level,
				Health = origin.Health,
				Traits = origin.Traits,
				Statblock = string.Empty
			};

			GenerateStatblock(creature, result);

			return result;
		}

		private void GenerateStatblock(CreatureEncounterParticipant creature, Creature target)
		{
			var parser = new HtmlParser();
			var document = parser.ParseDocument(creature.Origin.Statblock);

			var spans = document.QuerySelectorAll("span").OfType<IHtmlSpanElement>().ToList();
			var rollable = spans.Where(x => x.ClassList.Contains("rollable")).ToList();

			foreach (var span in rollable)
			{
				var type = span.Dataset["type"];

				switch (type)
				{
					case "melee" or "ranged":
						ApplyAttackModifiers(span, type.Capitalize(), creature);
						break;
					case "save" or "skill":
						ApplyDefaultModifiers(span, creature);
						break;
				}
			}

			target.ArmorClass = FindProperty<int>(spans, "armor");
			target.Perception = FindProperty<int>(spans, "perception");

			// TODO: Find out why null
			// TODO: Unify createion and fill in sepoarate service
			target.SavingThrows = new Dictionary<string, int>
			{
				{ "Fortitude", FindProperty<int>(spans, "save", "fortitude") },
				{ "Will", FindProperty<int>(spans, "save", "will") },
				{ "Reflex", FindProperty<int>(spans, "save", "reflex") },
			};

			target.Statblock = document.Body!.InnerHtml;
		}

		
		private static T? FindProperty<T>(IReadOnlyCollection<IHtmlSpanElement> spans, string propertyName) where T : IParsable<T>
		{
			var text = spans.Where(x => x.Dataset["type"] == propertyName).FirstOrDefault()?.TextContent;

			if (T.TryParse(text, null, out var result))
			{
				return result;
			}
			else
			{
				return default;
			}
		}

		private static T? FindProperty<T>(IReadOnlyCollection<IHtmlSpanElement> spans, string type, string name) where T : IParsable<T>
		{
			var text = spans.Where(x => x.Dataset["type"] == type && x.Dataset["name"] == name).FirstOrDefault()?.TextContent;

			if (T.TryParse(text, null, out var result))
			{
				return result;
			}
			else
			{
				return default;
			}
		}

		private void ApplyDefaultModifiers(IHtmlSpanElement span, CreatureEncounterParticipant creature)
		{
			// TODO: Invert braces and add logging.
			if (int.TryParse(span.TextContent, out var total) && span.Dataset["name"] is string name)
			{
				var evaluator = new CreatureModifierEvaluator(creature.Conditions, name);
				var bonus = evaluator.Value;

				if (bonus > 0)
				{
					span.ClassList.Add("positive");
				}
				else if (bonus < 0)
				{
					span.ClassList.Add("negative");
				}

				total += bonus;
				span.TextContent = $"{(total >= 0 ? "+" : "")}{total}";
			}
		}

		private void ApplyAttackModifiers(IHtmlSpanElement span, string type, CreatureEncounterParticipant creature)
		{
			// TODO: Invert braces and add logging.
			if (int.TryParse(span.TextContent, out var total))
			{
				var evaluator = new CreatureModifierEvaluator(creature.Conditions, type);
				var bonus = evaluator.Value;

				if (bonus > 0)
				{
					span.ClassList.Add("positive");
				}
				else if (bonus < 0)
				{
					span.ClassList.Add("negative");
				}

				total += bonus;
				span.TextContent = $"{(total >= 0 ? "+" : "")}{total}";
			}
		}
	}
}
