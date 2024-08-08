using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Users.Dtos
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string Login { get; set; } = default!;
		public UserRole Role { get; set; }
		public Guid? ActiveCharacterId { get; set; }
		public List<Guid> CharactersId { get; set; } = [];
	}
}
