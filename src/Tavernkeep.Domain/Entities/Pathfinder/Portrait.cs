using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Entities.Base;

namespace Tavernkeep.Domain.Entities.Pathfinder;

[Table("Portrait")]
public class Portrait : GuidEntity
{
	public Character Owner { get; set; }
	public byte[] Bytes { get; set; }
	public string MimeType { get; set; }
}
