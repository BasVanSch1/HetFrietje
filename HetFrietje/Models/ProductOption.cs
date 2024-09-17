namespace HetFrietje.Models
{
    public class ProductOption
    {
        public int ProductOptionId { get; private set; }
        public string Name { get; private set; }
        public List<string> Values { get; private set; }

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
