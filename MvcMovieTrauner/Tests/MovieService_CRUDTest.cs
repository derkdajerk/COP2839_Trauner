using Microsoft.EntityFrameworkCore;
using MvcMovieTrauner.Data;
using MvcMovieTrauner.Models;
using MvcMovieTrauner.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests;

public class MovieService_CRUDTest
{
    private static MovieService CreateService()
    {
        var dbCtxBuilder = new DbContextOptionsBuilder<MvcMovieTraunerContext>();
        var inMemDb = dbCtxBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        var options = inMemDb.Options;
        var context = new MvcMovieTraunerContext(options);

        return new MovieService(context);
    }

    [Fact]
    public async Task MovieService_CanPerformCRUDOp()
    {
        var svc = CreateService();
        // CREATE
        var movie = new Movie
        {
            Title = "Inception",
            Genre = "Sci-Fi",
            Price = 18.99M,
            Rating = "PG-13",
            ReleaseDate = DateTime.Parse("2010-05-01")
        };

        await svc.AddAsync(movie);
        // READ
        var read = await svc.GetByIdAsync(movie.Id);
        Assert.NotNull(read);
        Assert.Equal(movie.Title, read.Title);

        // UPDATE
        read.Title = "Inception (20th year anniversary edition)";
        await svc.UpdateAsync(read);
        var updated = await svc.GetByIdAsync(movie.Id);
        Assert.NotNull(updated);
        Assert.Equal(read.Title, updated.Title);

        // DELETE
        await svc.DeleteAsync(movie.Id);
        var deleted = await svc.GetByIdAsync(movie.Id);
        Assert.Null(deleted);

    }
}
