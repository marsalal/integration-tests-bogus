﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using integration_tests.Data;
using integration_tests.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace sample.tests
{
    public class BaseWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<SampleContext>));

                services.Remove(descriptor);

                services.AddDbContext<SampleContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SampleContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<BaseWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        private void InitializeDbForTests(SampleContext db)
        {
            db.Persons.AddRange(GetFakePersons());
        }

        private IEnumerable<integration_tests.Data.Models.Person> GetFakePersons()
        {
            return new Faker<integration_tests.Data.Models.Person>()
                .RuleFor(r => r.Name, f => f.Person.FirstName)
                .RuleFor(r => r.LastName, f => f.Person.LastName)
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.Age, f => f.Random.Int(1,99))
                .Generate(5);
        }
    }
}