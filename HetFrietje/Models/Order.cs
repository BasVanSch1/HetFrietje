namespace HetFrietje.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public User User { get; set; }
        public IList<ProductOrder>? Products { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubtotalPrice { get; set; }

        public int PlaceOrder()
        {
            return -1;
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;

            if (Products != null)
            {
                foreach (var productOrder in Products)
                {
                    total += CalculateTaxOfProduct(productOrder.Product) * productOrder.ProductCount + productOrder.Product.Price * productOrder.ProductCount; // tax * count + price * count
                }
            }

            return total;
        }

        private decimal CalculateSubtotal()
        {
            decimal total = 0;
            if (Products != null)
            {
                foreach (var productOrder in Products)
                {
                    total += productOrder.Product.Price * productOrder.ProductCount;
                }
            }

            return total;
        }

        private decimal CalculateTaxOfProduct(Product product)
        {
            return (product.Price / 100) * product.Tax;
        }
    }
}
