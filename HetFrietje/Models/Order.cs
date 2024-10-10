using System.Text.Json.Serialization;

namespace HetFrietje.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? Username { get; set; }
        public User? User { get; set; }
        public IList<ProductOrder>? Products { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubtotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Calculates the total price based on all the products in the Order and assigns it to the TotalPrice property of the object.
        /// Also calls CalculateSubtotalPrice()
        /// </summary>
        /// <returns>The calculated TotalPrice.</returns>
        public decimal CalculateTotalPrice()
        {
            decimal total = 0;

            if (Products != null)
            {
                foreach (var productOrder in Products)
                {
                    total += CalculateTaxOfProduct(productOrder.Product) * productOrder.ProductCount + productOrder.Product.Price * productOrder.ProductCount; // tax * count + price * count
                }
            }

            CalculateSubtotalPrice();
            TotalPrice = total;
            return total;
        }

        /// <summary>
        /// Calculates the subtotal price based on all the products in this Order and assigns it to the SubtotalPrice property of the object.
        /// </summary>
        /// <returns>The calculated SubtotalPrice</returns>
        private decimal CalculateSubtotalPrice()
        {
            decimal total = 0;
            if (Products != null)
            {
                foreach (var productOrder in Products)
                {
                    total += productOrder.Product.Price * productOrder.ProductCount;
                }
            }

            SubtotalPrice = total;
            return total;
        }

        private decimal CalculateTaxOfProduct(Product product)
        {
            return (product.Price / 100) * product.Tax;
        }
    }
}
