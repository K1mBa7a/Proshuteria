namespace proshuteria.Data
{
    public class Order
    {
        public int Id { get; set; }

        public string ClientId { get; set; } //FK M - - - > 1
        public Client Clients { get; set; } // Table

        public int MeatId { get; set; } //FK  - - - > 1
        public Meat Meats { get; set; } // Table

        public int Quantity { get; set; }

        public DateTime OrderOn { get;  }= DateTime.Now;
    }
}
