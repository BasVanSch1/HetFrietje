namespace HetFrietje.Models
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public decimal Tax { get; private set; }
        public List<ProductOption> Options { get; private set; }

        public int SetName(string name)
        {
            return -1;
        }

        public int SetDescription(string description)
        {
            return -1;
        }

        public int SetCategory(Category category)
        {
            return -1;
        }

        public int SetPrice(decimal price)
        {
            return -1;
        }

        public int SetTax(decimal tax)
        {
            return -1;
        }

        public int AddProductOption(ProductOption option)
        {
            return -1;
        }

        public int RemoveProductOption(ProductOption option)
        {
            return -1;
        }

        public IList<Product> GetProducts()
        {
            List<Product> products = new();
            return products;
        }
    }
}
