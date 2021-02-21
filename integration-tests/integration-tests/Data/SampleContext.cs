using System;
using integration_tests.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace integration_tests.Data
{
    public class SampleContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }

        public SampleContext(DbContextOptions<SampleContext> dbContextOptions) : base(dbContextOptions) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
