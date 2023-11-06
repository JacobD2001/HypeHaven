using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HypeHaven.Areas.Identity.Data;

public class HypeHavenUser : IdentityUser
{
    public string? Name { get; set; } 
    public string? Address { get; set; }
}

