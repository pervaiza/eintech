using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.domain.Models
{
    public interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        DateTime CreatedOn { get; set; }
    }
}
