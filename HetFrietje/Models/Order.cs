namespace HetFrietje.Models
{
    public class Order
    {
        public int OrderId { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public List<Product> Products { get; private set; }
        public OrderStatus Status { get; private set; }
        public double TotalPrice { get; private set; }
        public double SubtotalPrice { get; private set; }

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
