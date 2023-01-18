using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HypeHaven.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HypeHavenUser class
public class HypeHavenUser : IdentityUser
{
    public string? Name { get; set; } 
    public string? Address { get; set; }

}

