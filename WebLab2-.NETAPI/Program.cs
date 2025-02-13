using WebLab2_.NETAPI.Data;
using WebLab2_.NETAPI.Models;

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

            PotatoCRUD _potato = new PotatoCRUD("Potatis");


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //Create
            app.MapPost("/potatis", async (Potatoes potato) =>
            {
                var potatoDB = await _potato.AddPotato("Potatisar", potato);
                return Results.Ok(potatoDB);
            });


            //Get
            app.MapGet("/potatisar", async () =>
            {
                var potatoes = await _potato.GetAllPotatoes("Potatisar");
                return Results.Ok(potatoes);
            });


            //Get by Id
            app.MapGet("/potatis/{id}", async (string id) =>
            {
                var potato = await _potato.GetPotatoById("Potatisar", id);
                return Results.Ok(potato);
            });



            //Update
            app.MapPut("/potais/{id}", async (string id, Potatoes potato) =>
            {
                var potatoDB = await _potato.UpdatePotatoById("Potatisar", id, potato);
                return Results.Ok(potatoDB);
            });


            //Delete
            app.MapDelete("/potatis/{id}", async (string id) =>
            {
                var potato = await _potato.DeletePotatoById("Potatisar", id);
                return Results.Ok(potato);
            });

            app.Run();

        }
    }
}
