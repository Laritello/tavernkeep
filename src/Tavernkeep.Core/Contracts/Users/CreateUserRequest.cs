using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Users
{
    public class CreateUserRequest
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; } = default!;
    }
}
