using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Norlys.Repositories;
using Norlys.Services;

public class Startup 
{
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration) 
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services) 
    {
        // Get the connection string from appsettings.json or any other configuration source
        string connectionString = Configuration.GetConnectionString("SqlConnection");

        services.AddControllers();
        services.AddAuthentication();

        //Repositories
        services.AddScoped<IOfficeLocationRepository>(provider => new OfficeLocationRepository(connectionString));
        services.AddScoped<IPeopleRepository>(provider => new PeopleRepository(connectionString));

        //Services
        services.AddScoped<IPeopleService, PeopleService>();
        services.AddScoped<IOfficeLocationService, OfficeLocationService>();
        services.AddScoped<PersonValidator>();


        // Add Swagger
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Norlys", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app) 
    {
        //Add swagger
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Norlys");
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => {
            endpoints.MapControllers(); 
        });
    }
}