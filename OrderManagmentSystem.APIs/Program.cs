using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderManagmentSystem.APIs.Helpers;
using OrderManagmentSystem.APIs.Middllewares;
using System.Domain.Services.Contract;
using System.Repository.Data;
using System.Repository;
using System.Services;
using System.Text.Json.Serialization;
using System.Text;
using OrderManagmentSystem.APIs.Errors;
using System.Domain.Entities;

namespace OrderManagmentSystem.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Service Configuration
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IOrderServices), typeof(OrderService));
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            builder.Services.AddScoped(typeof(IInvoice), typeof(InvoiceService));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    };

                }
                );
            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                o.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
            }
            );

            builder.Services.AddDbContext<SystemContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                    

            builder.Services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actioncontext =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count > 0)
                    .SelectMany(P => P.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                    var validateError = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validateError);
                };
            });
            #endregion

            var app = builder.Build();

            #region Update DataBase
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var dbcontext = service.GetRequiredService<SystemContext>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await dbcontext.Database.MigrateAsync();
                await SystemDataSeed.SeedAsync(dbcontext);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occuerd During Apply The Migration");
            }
            #endregion

            #region Kestrel Configuration
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}

