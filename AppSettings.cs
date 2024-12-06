namespace PortfolioService;

public class AppSettings
{
    public static int LocalPort { get; private set; } = 5001;

    public static void Initialize(IConfiguration configuration)
    {
        if (int.TryParse(configuration["BACKEND_LOCAL_PORT"] ?? "5001", out var port))
        {
            LocalPort = port;
        }
    }
}