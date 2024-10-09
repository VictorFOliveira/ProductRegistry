using Serilog;
using Microsoft.Extensions.Hosting;
using SistemaDeCadastro.Repositories.Interfaces;
using SistemaDeCadastro.Repositories;
using SistemaDeCadastro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;


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

                // MySQL configuração
                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });



                builder.Services.AddHttpContextAccessor();
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema de Cadastro de Produtos", Version = "v1" });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                });

                // Registro de repositório
                builder.Services.AddScoped<IProduct, ProductRepository>();
                builder.Services.AddScoped<IUser, UserRepository>();

                var app = builder.Build();

                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Um erro ocorreu durante a execução da requisição.");
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";

                        var errorResponse = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Ocorreu um erro interno no servidor.",
                            Detail = ex.Message 
                        };

                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }
                });

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    Log.Information("O Swagger foi inicializado");
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Cadastro de Produtos v1");
                        c.RoutePrefix = "swagger";
                    });
                }

                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "A aplicação falhou ao iniciar.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
