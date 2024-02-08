using Microsoft.EntityFrameworkCore.Infrastructure;

namespace proshuteria.Data
{
    public class Client:IdentityRole
    {
        public int ClientId { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public ICollection<Order>Orders { get; set; } //1:M
    }
}
