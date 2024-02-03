namespace Tavernkeep.Core.Contracts.Users
{
    public class CreateUserRequest
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
