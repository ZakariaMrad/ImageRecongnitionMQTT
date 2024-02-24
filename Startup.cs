using System.Text.Json.Serialization;
using ImageRecognitionMQTT;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services and dependencies here
        // For example:
        services.AddDbContext<ImageRecognitionContext>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddControllers();
        // services.AddScoped<MarkerDetectionService>();
        services.AddScoped<BeamDetectionService>();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    JsonIgnoreCondition.WhenWritingNull;
            });
        // Add other services and dependencies as needed

        // Configure other services as needed
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowMyOrigin",
                builder =>
                    builder
                        .WithOrigins("*") // Allows requests from this origin
                        .AllowAnyMethod() // Allows all HTTP methods
                        .AllowAnyHeader()
            ); // Allows all headers
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configure production environment settings
        }
        app.UseCors("AllowMyOrigin"); // Apply the CORS policy

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Map controllers
        });
    }
}
