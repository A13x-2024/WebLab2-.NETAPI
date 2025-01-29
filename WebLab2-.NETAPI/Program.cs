using MongoDB.Bson;
using MongoDB.Driver;
using WebLab2_.NETAPI.Models;
using WebLab2_.NETAPI.Services;

namespace WebLab2_.NETAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            //Create
            app.MapPost("/potatis", async (PotatoServices service) =>
            {
                var newPotatis = await service.CreateAsync(new Potatis());
                return Results.Ok(newPotatis);
            });


            //Get
            app.MapGet("/potatisar", async (PotatoServices service) =>
            {
                var potatisar = await service.GetAsync();
                return Results.Ok(potatisar);
            });


            //Get by ID
            app.MapPut("/potatis{id}", async (PotatoServices service, int id) =>
            {
                var storedPotatis = await service.GetByIdAsync(id);
                if (storedPotatis == null)
                {
                    return Results.NotFound();
                }
                await service.GetByIdAsync(id);
                return Results.Ok(storedPotatis);
            });


            //Update
            app.MapPut("/potatis/{id}", async (PotatoServices service, Potatis updatedPotatis, int id) =>
            {
                var storedPotatis = await service.GetByIdAsync(id);
                if (storedPotatis == null)
                {
                    return Results.NotFound("Sorry, this potatis does not exist.");
                }
                await service.UpdateAsync(id, updatedPotatis);
                return Results.Ok(storedPotatis);

            });


            //Delete
            app.MapDelete("/potatis{id}", async (PotatoServices service, int id) =>
            {
                var potatisar = await service.GetByIdAsync(id);

                if (potatisar == null)
                {
                    return Results.NotFound();
                }
                await service.RemoveAsync(id);
                return Results.Ok();
            });

            app.Run();

        }
    }
}
