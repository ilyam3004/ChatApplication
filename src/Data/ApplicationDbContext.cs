using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Data.Entities;

namespace Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaTypeNames.Application).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}