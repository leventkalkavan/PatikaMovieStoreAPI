using Microsoft.Extensions.Configuration;

namespace Persistence;

public class Configuration
{
    public static string? GetConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "/Users/leventkalkavan/Desktop/Projeler/MovieStore/Presentation/WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("Mssql");
        }
    }
}