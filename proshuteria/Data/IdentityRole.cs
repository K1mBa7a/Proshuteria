namespace proshuteria.Data
{
    public class IdentityRole
    {
        public int Id { get; set; }

        public string Administrator { get; set; }

        public string Client { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
