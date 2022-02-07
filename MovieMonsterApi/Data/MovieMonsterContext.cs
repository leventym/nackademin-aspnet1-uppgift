using Microsoft.EntityFrameworkCore;
using MovieMonsterApi.Entities;

namespace MovieMonsterApi.Data
{
    public class MovieMonsterContext : DbContext
    {
        public MovieMonsterContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
