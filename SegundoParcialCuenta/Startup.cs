using Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure.Repository;
using Microsoft.Extensions.Configuration;
using Infraestructure.Repository;

namespace SegundoParcialCuenta
{
    public class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            ConfigureServices(builder, configuration);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder, IConfiguration configuration)
        {

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Agrega tus servicios personalizados aquí
            builder.Services.AddScoped<OperacionService>(); 
            builder.Services.AddScoped<CuentaRepository>(provider =>
            {
                var connectionString = configuration.GetConnectionString("postgresDB");
                return new CuentaRepository(connectionString);
            });



        }
        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }


    }
}
