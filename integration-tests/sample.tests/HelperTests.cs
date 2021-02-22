using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using PersonModel = integration_tests.Data.Models.Person;
namespace sample.tests
{
    public static class HelperTests
    {
        public static IEnumerable<PersonModel> GetFakePersons()
        {
            var fakePerson = new Faker<PersonModel>()
                   .UseSeed(88)
                   .RuleFor(r => r.Id, f => (f.IndexVariable++).ToString())
                   .RuleFor(r => r.Name, f => f.Person.FirstName)
                   .RuleFor(r => r.LastName, f => f.Person.LastName)
                   .RuleFor(r => r.Email, f => f.Person.Email)
                   .RuleFor(r => r.Age, f => f.Random.Int(1, 99));

            
            PersonModel CreteListFakePersons(int seed)
            {
                return fakePerson.UseSeed(seed).Generate();
            }

            return Enumerable.Range(1, 5)
                .Select(CreteListFakePersons)
                .ToList();
        }
    }
}
