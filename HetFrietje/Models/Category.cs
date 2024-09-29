namespace HetFrietje.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public IList<Product>? Products { get; set; } // Dit is nodig voor de many-to-many relatie.
    }
}
