using System;
using System.ComponentModel.DataAnnotations;

namespace integration_tests.Data.Models
{
    public class Person
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Age { get; set; }

    }
    
}

