
using Bogus;
using DraftSQliteDb.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DraftSQliteDb.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddScoped<AuthorRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // � ������ ����������
            using var ctx = new AppDbContext();
            await ctx.Database.MigrateAsync(); // �������� �����

            // ���� ������� ����� � ����������
            if (!ctx.Authors.Any())
            {
                // ��������� ��������� �������
                var authorFaker = new Faker<Author>()
                    .RuleFor(a => a.Name, f => f.Person.FullName)
                    .RuleFor(a => a.Contact, f => new Contact
                    {
                        Phone = f.Phone.PhoneNumber("+420 ### ### ###"),
                        Address = new Address(
                            street: f.Address.StreetAddress(),
                            city: f.Address.City(),
                            postcode: f.Address.ZipCode(),
                            country: f.Address.Country())
                    });

                // ���������� 100 �������
                var authors = authorFaker.Generate(10000);
                ctx.Authors.AddRange(authors);
                ctx.SaveChanges();
            }

            app.MapGet("/authors/by-city/{city}", async (string city, AuthorRepository repo) =>
            {
                var authors = await repo.GetAuthorsByCityAsync(city);
                return authors; // ����� JSON-������ Author
            });

            app.Run();

            Console.ReadLine();
        }
    }
}
