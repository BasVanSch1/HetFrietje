namespace HetFrietje.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; }
        public IList<string> Values { get; set; }
        public IList<Product>? Products { get; set; } // Dit is nodig voor de many-to-many relatie.
    }
}
