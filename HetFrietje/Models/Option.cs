namespace HetFrietje.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; }
        public IList<string> Values { get; set; }
        public IList<Product>? Products { get; set; } // Dit is nodig voor de many-to-many relatie.

        public int AddValue(string value)
        {
            return -1;
        }

        public int UpdateValue(int index, string value)
        {
            return -1;
        }

        public int RemoveValue(string value)
        {
            return -1;
        }
    }
}
