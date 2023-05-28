using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Infrastructure.Repositories;

namespace UniversityScheduler.Api;

public class Startup
{
    // get the connection string
    private const string ConnectionString =
        "User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=UniversitySchedulerDatabase;";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // set up dependency injection
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddScoped(typeof(IGenericRepository<>), typeof(InMemoryGenericRepository<>));

        services.AddScoped(typeof(IDatabaseGenericRepository<>), typeof(DatabaseGenericRepository<>));

        services.InjectServices("UniversityScheduler.Api.Core");

        services.AddDbContext<DataContext>(options => { options.UseNpgsql(new NpgsqlConnection(ConnectionString)); });
        
        services.AddControllers(options => { }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1",
                Description = "A simple ASP.NET Core Web API"
            });
        });

        services.AddCors(o => o.AddPolicy("FrontEndLocalhost", builder =>
        {
            builder
                .AllowAnyOrigin() //TO DO Remove this after further testing
                .AllowAnyHeader()
                .AllowAnyMethod();
        }));
    }

    // set up application stuff
    public void Configure(IApplicationBuilder app)
    {
        app.UseCors("FrontEndLocalhost");
        app.UseSwagger();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"); });
    }
}