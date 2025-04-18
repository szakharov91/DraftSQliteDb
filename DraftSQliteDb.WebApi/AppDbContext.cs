using DraftSQliteDb.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DraftSQliteDb.WebApi;

public class AppDbContext: DbContext
{
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=myapp.db")
            .EnableSensitiveDataLogging(); // для логирования
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(author => {

            // Храним как JSON
            author.OwnsOne(a => a.Contact, cb =>
            {
                cb.ToJson();
                cb.OwnsOne(c => c.Address);
            });

            // Добавляем сгенерированную колонку AddressCity
            author.Property<string>("AddressCity")
                .HasComputedColumnSql("json_extract(Contact, '$.Address.City')", stored: true);

            // Индексируем её
            author.HasIndex("AddressCity")
             .HasDatabaseName("IX_Authors_AddressCity");

        });
    }
}
