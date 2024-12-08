using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;

namespace PortfolioService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var useHttps = builder.Configuration["BACKEND_USE_HTTPS"] ?? "true";

        var httpsPort = builder.Configuration["BACKEND_HTTPS_PORT"] ?? "5000";
        var localPort = builder.Configuration["BACKEND_LOCAL_PORT"] ?? "5001";
        var httpPort = builder.Configuration["BACKEND_HTTP_PORT"] ?? "5002";

        builder.WebHost.ConfigureKestrel((options) =>
        {
            options.ListenAnyIP(int.Parse(httpPort));
            options.ListenAnyIP(int.Parse(localPort));

            if (useHttps == "false") return;

            options.ListenAnyIP(int.Parse(httpsPort), (listenOptions) =>
            {
                listenOptions.UseHttps();
            });
        });

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddControllers();
        builder.Services.AddDbContext<BlogDbContext>((options) =>
        {
            string? port = builder.Configuration["MYSQL_PORT"];
            string? server = builder.Configuration["MYSQL_SERVER"];
            string? user = builder.Configuration["MYSQL_USER"];
            string? password = builder.Configuration["MYSQL_PASSWORD"];
            string? database = builder.Configuration["MYSQL_DATABASE"];

            if (string.IsNullOrEmpty(port) || string.IsNullOrEmpty(server) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(database))
            {
                throw new Exception("Database connection information is missing.");
            }

            string connectionString = $"server={server};port={port};user={user};password={password};database={database}";
            Console.WriteLine($"Connection string: {connectionString}");

            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
            });
        });

        AppSettings.Initialize(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        if (useHttps == "true") app.UseHttpsRedirection();

        app.UseCors();

        app.MapControllers();
        app.Run();
    }
}