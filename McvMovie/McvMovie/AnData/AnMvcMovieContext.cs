using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data {
    public class AnMvcMovieContext : DbContext {
        public AnMvcMovieContext(DbContextOptions<AnMvcMovieContext> options)
            : base(options) {
        }

        public DbSet<AnMovie> Movie { get; set; }
    }
}