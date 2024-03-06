using System.ComponentModel.DataAnnotations;

namespace proshuteria.Data
{
    public class MeatCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime RegisterOn { get; set; }

        //Connection 1 - - - > M
        
        public ICollection<Meat> Meats { get; set; }
    }
}
