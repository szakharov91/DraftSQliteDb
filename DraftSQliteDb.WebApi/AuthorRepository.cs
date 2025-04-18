using DraftSQliteDb.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DraftSQliteDb.WebApi;

public class AuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
        => _context = context;

    /// <summary>
    /// Находит всех авторов из указанного города,
    /// используя прямой доступ к вложенному свойству JSON.
    /// </summary>
    public async Task<List<Author>> GetAuthorsByCityAsync(string city)
    {
        return await _context.Authors
            .Where(a => a.Contact.Address.City == city)
            .ToListAsync();
    }

    /// <summary>
    /// То же самое, но через «вычисляемую» shadow‑свойство AddressCity,
    /// проиндексированное как вычисляемый столбец.
    /// </summary>
    public async Task<List<Author>> GetAuthorsByCityViaComputedColumnAsync(string city)
    {
        return await _context.Authors
            .Where(a => EF.Property<string>(a, "AddressCity") == city)
            .ToListAsync();
    }
}
