using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RazorPagesMovieContext>>()))
            {
                // Look for any movies
                if(context.Movie.Any())
                {
                    // DB has been seeded
                    return;
                }
                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "Drunken Master",
                        ReleaseDate = DateTime.Parse("1990-2-23"),
                        Genre="Action",
                        Price=1.2M,
                        Rating="R"
                    },
                    new Movie
                    {
                        Title="Me and Rebekah",
                        ReleaseDate=DateTime.Parse("1991-3-18"),
                        Genre="Romance",
                        Price=30M,
                        Rating="PG"
                    },
                    new Movie
                    {
                        Title = "Chamber of Secrets",
                        ReleaseDate=DateTime.Parse("2008-4-17"),
                        Genre="Magical Series",
                        Price=80000,
                        Rating="XXX"
                    }
                );
                context.SaveChanges();
            }
            
        }
    }

}