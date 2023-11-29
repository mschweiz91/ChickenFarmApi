
using ChickenFarmApi.Data;
using ChickenFarmApi.DataAccess;
using System.Reflection;

namespace ChickenFarmApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddSingleton<ChickenFarmContext>();

            var app = builder.Build();
            SeedDatabase();

            void SeedDatabase()
            {
                using var scope = app.Services.CreateScope();
                try
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<ChickenFarmContext>();
                    Seeder.SeedData(scopedContext);
                }
                catch
                {
                    throw;
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}