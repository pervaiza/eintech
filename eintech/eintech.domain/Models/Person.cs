using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.domain.Models
{
    public class Person : IPerson
    {
        public Person()
        {
            CreatedOn = DateTime.Now;
        }
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
