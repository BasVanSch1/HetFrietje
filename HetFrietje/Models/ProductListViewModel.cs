namespace HetFrietje.Models
{
    public class ProductListViewModel
    {
        public IList<Product>? Products { get; set; }
        public IList<Category>? Categories { get; set; }
        public int? OrderProductCount { get; set; }
    }
}
