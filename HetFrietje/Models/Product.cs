using System.Text.Json.Serialization;

namespace HetFrietje.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal Tax { get; set; }
        public int Stock {  get; set; }
        public IList<Option>? Options { get; set; }
        public IList<ProductOrder>? Orders { get; set; }
        public IList<Category>? Categories { get; set; }
    }
}
