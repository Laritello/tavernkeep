using CommandLine;

namespace Tavernkeep.Shared.Options
{
    public class LaunchOptions
    {
        [Option('c', "campaign", Required = true, HelpText = "Selected campaign name.")]
        public string? CampaignName { get; set; }

        [Option('l', "login", Required = false, HelpText = "Admin login. Used only during campaaign creation.")]
        public string? Login { get; set; }

        [Option('p', "password", Required = false, HelpText = "Admin password. Used only during campaaign creation.")]
        public string? Password { get; set; }
    }
}
