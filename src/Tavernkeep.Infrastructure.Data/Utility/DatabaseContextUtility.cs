namespace Tavernkeep.Infrastructure.Data.Utility
{
    public static class DatabaseContextUtility
    {
        public static string GetConnectionString(string? campaignName = null)
        {
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var sessionDirectory = Path.Combine(workingDirectory, "Sessions");

            // Ensure directory exists
            Directory.CreateDirectory(sessionDirectory);

            var databasePath = string.IsNullOrEmpty(campaignName)
                ? Path.Combine(sessionDirectory, $"TestCampaign.db")
                : Path.Combine(sessionDirectory, $"{campaignName}.db");

            return $"Data Source={databasePath};";
        }
    }
}
