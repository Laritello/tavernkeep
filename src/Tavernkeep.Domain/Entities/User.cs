using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Base;
using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Domain.Entities;

[Table("Users")]
public class User : GuidEntity
{
	#region Backing fields

	private readonly List<Character> _characters = [];

	#endregion

	#region Constructors

	public User(string login, string password, UserRole role)
	{
		Login = login;
		Password = password;
		Role = role;
	}

	#endregion

	#region Properties

	public string Login { get; set; }
	public string Password { get; set; }
	public UserRole Role { get; set; }
	public Character? ActiveCharacter { get; set; }
	public IReadOnlyList<Character> Characters => _characters.AsReadOnly();

	#endregion

	#region Methods

	public void AddCharacter(Character character)
	{
		// TODO: Validation
		_characters.Add(character);
	}

	public void RemoveCharacter(Character character)
	{
		// TODO: Validation
		_characters.Remove(character);
	}

	#endregion
}
