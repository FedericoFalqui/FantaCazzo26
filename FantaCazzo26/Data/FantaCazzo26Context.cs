using FantaCazzo26.Models;
using Microsoft;
using Microsoft.EntityFrameworkCore;

namespace FantaCazzo26.Data;

public class FantaCazzo26Context : DbContext
{
    
    public FantaCazzo26Context(DbContextOptions<FantaCazzo26Context> options) : base(options){}

    public DbSet<Squad> Squads { get; set; }
    public DbSet<Player> Players { get; set; }
}