﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models {
    public static class AnSeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new AnMvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AnMvcMovieContext>>())) {
                // Look for any movies.
                if (context.Movie.Any()) {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new AnMovie {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-1-11"),
                        Genre = "Romantic Comedy",
                        Rating = "PG-13",
                        Price = 7.99M
                    },

                    new AnMovie {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 8.99M
                    },

                    new AnMovie {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 9.99M
                    },

                    new AnMovie {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = "M",
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}