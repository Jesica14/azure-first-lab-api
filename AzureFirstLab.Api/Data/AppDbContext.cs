using Microsoft.EntityFrameworkCore;
using AzureFirstLab.Api.Models;

namespace AzureFirstLab.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
