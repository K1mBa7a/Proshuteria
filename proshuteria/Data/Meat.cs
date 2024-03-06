using System.ComponentModel.DataAnnotations.Schema;

namespace proshuteria.Data
{
    public class Meat
    {
        public int Id { get; set; }

        public int CatalogeNumber { get; set; }

        public string Name { get; set; }

       
        public int MeatCategoryId { get; set; } // FK M  - - - > 1
        public MeatCategory MeatCategories { get; set; } // Table

        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; }

        public DateTime DateCreated { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
