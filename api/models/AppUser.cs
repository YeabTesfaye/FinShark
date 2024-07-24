using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace api.models;
[Table("AppUser")]
public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = [];
}