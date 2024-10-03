namespace HetFrietje.Models
{
    public class ProductOrderViewModel
    {
        public ProductOrder ProductOrder { get; set; }
        public IList<Category>? Categories { get; set; }
        public IList<Option>? Options { get; set; }
    }
}
