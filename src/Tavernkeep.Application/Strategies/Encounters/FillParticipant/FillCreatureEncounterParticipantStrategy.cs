using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Entities.Library.Creatures;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.FillParticipant
{
	public class FillCreatureEncounterParticipantStrategy(ICreatureLibraryRepository creatureRepository) : IFillEncounterParticipantStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Creature;

		public async Task FillParticipantAsync(EncounterParticipant participant, CancellationToken cancellationToken)
		{
			if (participant is CreatureEncounterParticipant creatureParticipant)
			{
				creatureParticipant.Origin = await creatureRepository.GetCreatureAsync(creatureParticipant.OriginId, cancellationToken);
				creatureParticipant.Creature = CreateFromOrigin(creatureParticipant);
			}
		}

		private Creature CreateFromOrigin(CreatureEncounterParticipant creature)
		{
			var origin = creature.Origin;

			return new Creature
			{
				Id = origin.Id,
				Type = origin.Type,
				Name = origin.Name,
				Level = origin.Level,
				Health = origin.Health,
				Traits = origin.Traits,
				Statblock = GenerateStatblock(creature),
			};
		}

		private string GenerateStatblock(CreatureEncounterParticipant creature)
		{
			var parser = new HtmlParser();
			var document = parser.ParseDocument(creature.Origin.Statblock);

			var spans = document.QuerySelectorAll("span.rollable").OfType<IHtmlSpanElement>();

			foreach(var span in spans)
			{
				if (span.Dataset["name"] is string name && span.Dataset["type"] is not ("ability" or "melee" or "ranged"))
				{
					if (int.TryParse(span.TextContent, out var total))
					{
						var evaluator = new CreatureModifierEvaluator(creature.Conditions, name);
						var bonus = evaluator.Value;
						total += bonus;
						span.TextContent = $"{(total >= 0 ? "+" : "")}{total}";
						
						if (bonus > 0)
						{
							span.ClassList.Add("positive");
						}
						else if (bonus < 0)
						{
							span.ClassList.Add("negative");
						}
					}
				}
			}

			return document.Body!.InnerHtml;
		}
	}
}
