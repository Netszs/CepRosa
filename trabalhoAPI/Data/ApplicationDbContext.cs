using Microsoft.EntityFrameworkCore;
using trabalhoAPI.Models;

namespace trabalhoAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
}
