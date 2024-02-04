using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zal.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ZalUser class
public class ZalUser : IdentityUser
{
    public string Imie {  get; set; }
    public string Nazwisko {  get; set; }
}

