using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovieTrauner.Models;

namespace MvcMovieTrauner.Data
{
    public class MvcMovieTraunerContext : DbContext
    {
        public MvcMovieTraunerContext (DbContextOptions<MvcMovieTraunerContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovieTrauner.Models.Movie> Movie { get; set; } = default!;
    }
}
