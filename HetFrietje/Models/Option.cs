namespace HetFrietje.Models
{
    public class Option
    {
        public int ProductOptionId { get; set; }
        public string Name { get; set; }
        public List<string> Values { get; set; }

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
