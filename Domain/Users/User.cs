using Domain.Attributes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    [Auditable]
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
