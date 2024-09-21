namespace HetFrietje.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public User User { get; set; }
        public List<ProductOrder>? Products { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubtotalPrice { get; set; }

        public int PlaceOrder()
        {
            return -1;
        }

        public int SetStatus(OrderStatus status)
        {
            return -1;
        }

        public int AddProduct(Product product)
        {
            return -1;
        }

        public int RemoveProduct(Product product)
        {
            return -1;
        }

        private decimal CalculateTotal()
        {
            return 0M;
        }

        private decimal CalculateSubtotal()
        {
            return 0M;
        }
    }
}
