using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Talabat.Api.Errors;
using Talabat.Api.Helpers;
using Talabat.Api.MiddleWares;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services  Add services to the container
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
           // builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));
           builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var Errors= actionContext.ModelState.Where(P=>P.Value.Errors.Count()>0)
                                             .SelectMany(P=>P.Value.Errors)
                                             .Select(P=>P.ErrorMessage).ToArray();

                    var ValidationErrorResponce = new ApiValidationErrorResponce()
                    {
                        Error = Errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponce);
                };
            });

            var app = builder.Build();

            #region Update-Database
            using var scope = app.Services.CreateScope();
            //Group of services Lifetime Scopped
            var Services = scope.ServiceProvider;
            //Services It self

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = Services.GetRequiredService<StoreContext>();
                //Ask CLR Cretae object From DbContext Explicity            
                await dbcontext.Database.MigrateAsync(); //Update DataBase

              await StoreContextSeed.SeedAsync(dbcontext);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An error occurd during appling Migration");
            }
            #endregion

           

        // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           // app.UseStatusCodePagesWithRedirects("/errors/{0}"); // make two request
            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // make one request
            app.UseStaticFiles();
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            

         
             app.Run();
        }
    }
}
