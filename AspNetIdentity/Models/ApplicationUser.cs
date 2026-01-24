using Microsoft.AspNetCore.Identity;
using System.Net;

namespace AspNetIdentity.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; } 
        public DateTime? LastLogin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
       // Navigation Prop
        public virtual List<Address>? Addresses { get; set; }
    }
}
