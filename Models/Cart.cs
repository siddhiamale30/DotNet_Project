namespace OnlineFoodOrdering.Models
{
    public class CartItem
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
