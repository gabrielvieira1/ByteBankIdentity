using ByteBankIdentity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ByteBankIdentity.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}
