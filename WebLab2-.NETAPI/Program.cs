using MongoDB.Bson;
using MongoDB.Driver;
using WebLab2_.NETAPI.Models;
using WebLab2_.NETAPI.Data;

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

            PotatoCRUD _potato = new PotatoCRUD("Potatoes");

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
            app.MapPost("/potato", async (Potatoes potato) =>
            {
                var potatoDB = await _potato.AddPotato("Potatoes", potato);
                return Results.Ok(potatoDB);
            });


            //Get
            app.MapGet("/potatoes", async () =>
            {
                var potatoes = await _potato.GetAllPotatoes("Potatoes");
                return Results.Ok(potatoes);
            });


            //Get by Id
            app.MapGet("/potato/{id}", async (string id) =>
            {
                var potato = await _potato.GetPotatoById("Potatoes", id);
                return Results.Ok(potato);
            });



            //Update
            app.MapPut("/potato/{id}", async (string id, Potatoes potato) =>
            {
                var potatoDB = await _potato.UpdatePotatoById("Potatoes", id, potato);
                return Results.Ok(potatoDB);
            });


            //Delete
            app.MapDelete("/potato/{id}", async (string id) =>
            {
                var potato = await _potato.DeletePotatoById("Potatoes", id);
                return Results.Ok(potato);
            });

            app.Run();

        }
    }
}
