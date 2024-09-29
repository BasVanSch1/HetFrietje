namespace HetFrietje.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IList<Category>? Categories { get; set; }
        public IList<int>? SelectedCategoryIds { get; set; }
        public IList<Option>? Options { get; set; }
        public IList<int>? SelectedOptionsIds { get; set; }
    }
}
