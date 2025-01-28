using MongoDB.Bson;
using MongoDB.Driver;


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

            var MongoClient = new MongoClient("mongodb://localhost:5013");


            var potatisar = new List<Potatis>
            {
                new Potatis { Id = 1, Name = "Potato", Type = "Normal", Rank = 100 },
                new Potatis { Id = 2, Name = "Mega-Potato", Type = "Stronk", Rank = 500 },
                new Potatis { Id = 3, Name = "Sanctified-Potato", Type = "Holy", Rank = 777 },
                new Potatis { Id = 4, Name = "Kakashi-Potato", Type = "Legendary", Rank = 9000 }
            };



            //Create
            app.MapPost("/potatis", (Potatis potatis) =>
            {
                potatisar.Add(potatis);
                return Results.Ok(potatis);
            });


            //Read all
            app.MapGet("/potatisar", () =>
            {
                return Results.Ok(potatisar);
            });


            //Read by ID
            app.MapGet("/potatis/{id}", (int id) =>
            {
                var potatis = potatisar.Find(p => p.Id == id);
                if (potatis == null)
                {
                    return Results.NotFound("Sorry, this potatis does not exist.");
                }

                return Results.Ok(potatis);
            });


            //Update
            app.MapPut("/potatis/{id}", (Potatis updatePotatis, int id) =>
            {
                var potatis = potatisar.Find(p => p.Id == id);
                if (potatis == null)
                {
                    return Results.NotFound("Sorry, this potatis does not exist.");
                };

                potatisar[id - 1] = updatePotatis;

                return Results.Ok(potatis);

            });


            //Delete
            app.MapDelete("/potatis/{id}", (int id) =>
            {
                var potatis = potatisar.Find(p => p.Id == id);
                if (potatis == null)
                {
                    return Results.NotFound("Sorry, this potatis does not exist.");
                };

                potatisar.Remove(potatis);
                return Results.Ok();
            });

            app.Run();

        }
    }
}
