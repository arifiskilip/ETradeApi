using Microsoft.Extensions.Configuration;

namespace ETradeApi.Persistence.Tools;

public static class Connection
{
	public static string GetConnection
	{
		get
		{
			ConfigurationManager configurationManager = new();
			configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETradeApi.API"));
			configurationManager.AddJsonFile("appsettings.json");

			return configurationManager.GetConnectionString("MsSQL");
		}
	}
}
