namespace OnlineFoodOrdering.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } // Ensure this is initialized in the constructor if necessary
        public int CategoryId { get; set; }
        public string Description { get; set; } // Ensure this is initialized in the constructor if necessary
        public double Price { get; set; }

        // ImageUrl property dynamically constructs the URL based on the Id
        public string ImageUrl
        {
            get { return $"{Id}.jpg"; } // Image filename corresponds to the Id
        }
    }
}
