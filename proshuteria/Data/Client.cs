using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace proshuteria.Data
{
    public class Client:IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order>Orders { get; set; } //1:M
    }
}
