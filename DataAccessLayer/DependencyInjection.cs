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

        string connectionStringTemplate = configuration.GetConnectionString("ISLOCAL")!;
        string connectionString = connectionStringTemplate == "1"? "Server=ecommerceproduct.mysql.database.azure.com;Port=3306;Database=ecommerceproductsdatabase;User ID=mysqladmin;Password=I@mthebest10"
            : Environment.GetEnvironmentVariable("CONNECTIONSTRINGS_DEFAULTCONNECTION")!;
        Logger.LogInformation($"{connectionString}: connectionString");

        services.AddDbContext<ApplicationDbContext>(options => {
      options.UseMySQL(connectionString);
    });

    services.AddScoped<IProductsRepository, ProductsRepository>();
    return services;
  }
}
