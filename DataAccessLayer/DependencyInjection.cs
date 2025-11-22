using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerce.ProductsService.DataAccessLayer;

public static class DependencyInjection
{
    private static readonly ILogger Logger =
        LoggerFactory.Create(builder => builder.AddLog4Net())
                     .CreateLogger("DependencyInjection");
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
  {
        //TO DO: Add Data Access Layer services into the IoC container
        //var hostFromEnv = Environment.GetEnvironmentVariable("MYSQL_HOST");
        //var pwdFromEnv = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

        string connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;
        string connectionString = connectionStringTemplate
          .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
          .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

        Logger.LogInformation($"{connectionString}: connectionString");
        Logger.LogInformation($"{connectionStringTemplate}: connectionStringTemplate");
        Logger.LogInformation($"{Environment.GetEnvironmentVariable("MYSQL_HOST")}: Environment.GetEnvironmentVariable(\"MYSQL_HOST\")");
        Logger.LogInformation($"{Environment.GetEnvironmentVariable("MYSQL_PASSWORD")}: Environment.GetEnvironmentVariable(\"MYSQL_PASSWORD\")");

        services.AddDbContext<ApplicationDbContext>(options => {
      options.UseMySQL(connectionString);
    });

    services.AddScoped<IProductsRepository, ProductsRepository>();
    return services;
  }
}
