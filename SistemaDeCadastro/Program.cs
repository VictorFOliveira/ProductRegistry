
using Serilog;
using Microsoft.Extensions.Hosting;
using SistemaDeCadastro.Repositories.Interfaces;
using SistemaDeCadastro.Repositories;
using SistemaDeCadastro.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeCadastro
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            try
            {

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
               

                builder.Host.UseSerilog();

                //MySQL configuração
                builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                //Registro de repositório
                builder.Services.AddScoped<IProduct, ProductRepository>();
                

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }

            catch (Exception ex)
            {
                Log.Fatal(ex, "A aplicação falohu ao iniciar.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
