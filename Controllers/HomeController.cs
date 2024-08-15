using Microsoft.AspNetCore.Mvc;
using OnlineFoodOrdering.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineFoodOrdering.Controllers
{
    public class HomeController : Controller
    {
        // In-memory shopping cart to hold cart items
        private static List<CartItem> ShoppingCart = new List<CartItem>();

        // Mock data for demonstration
        private static List<Food> MockFoods = new List<Food>
        {
            new Food { Id = 1, Name = "Margherita Pizza", Price = 400, CategoryId = 1, Description = "Classic pizza with tomato and mozzarella." },
            new Food { Id = 2, Name = "Pepperoni Pizza", Price = 350, CategoryId = 1, Description = "Spicy pepperoni on a classic pizza base." },
            new Food { Id = 3, Name = "Cheeseburger", Price = 150, CategoryId = 2, Description = "Juicy beef patty with cheese." },
            new Food { Id = 4, Name = "Veggie Burger", Price = 120, CategoryId = 2, Description = "Vegetarian burger with fresh vegetables." },
            new Food { Id = 5, Name = "Spaghetti", Price = 170, CategoryId = 3, Description = "Traditional spaghetti with marinara sauce." },
            new Food { Id = 6, Name = "Penne Alfredo", Price = 150, CategoryId = 3, Description = "Creamy Alfredo sauce with penne pasta." },
            new Food { Id = 7, Name = "Vegetable Noodles", Price = 100, CategoryId = 4, Description = "Stir-fried noodles with mixed vegetables." },
            new Food { Id = 8, Name = "Chicken Noodles", Price = 150, CategoryId = 4, Description = "Stir-fried noodles with chicken." },
            new Food { Id = 9, Name = "Plain Pav Bhaji", Price = 80, CategoryId = 5, Description = "Spicy mashed vegetables with pav bread." },
            new Food { Id = 10, Name = "Butter Pav Bhaji", Price = 100, CategoryId = 5, Description = "Spicy mashed vegetables with butter and pav bread." },
            new Food { Id = 11, Name = "Cheese Pav Bhaji", Price = 120, CategoryId = 5, Description = "Spicy mashed vegetables with cheese and pav bread." },
            new Food { Id = 12, Name = "Goli Vada Pav", Price = 60, CategoryId = 6, Description = "Spicy vada pav with goli." },
            new Food { Id = 13, Name = "Cheese Vada Pav", Price = 80, CategoryId = 6, Description = "Vada pav with cheese filling." },
            new Food { Id = 14, Name = "Shezwan Vada Pav", Price = 90, CategoryId = 6, Description = "Vada pav with spicy shezwan sauce." },
            new Food { Id = 15, Name = "Grill Vada Pav", Price = 60, CategoryId = 6, Description = "Grilled vada pav with special spices." },
            new Food { Id = 16, Name = "French Fries", Price = 80, CategoryId = 7, Description = "Crispy french fries." },
            new Food { Id = 17, Name = "Cheese Fries", Price = 100, CategoryId = 7, Description = "French fries with melted cheese." },
            new Food { Id = 18, Name = "Peri Peri Fries", Price = 110, CategoryId = 7, Description = "French fries with peri peri seasoning." },
            new Food { Id = 19, Name = "Mayonnaise Fries", Price = 200, CategoryId = 7, Description = "French fries with mayonnaise dip." },
            new Food { Id = 20, Name = "Cola", Price = 100, CategoryId = 8, Description = "Refreshing cola drink." },
            new Food { Id = 21, Name = "Lemon-Lime", Price = 80, CategoryId = 8, Description = "Citrus flavored lemon-lime soda." },
            new Food { Id = 22, Name = "Orange Soda", Price = 80, CategoryId = 8, Description = "Orange flavored soda." },
            new Food { Id = 23, Name = "Sprite", Price = 100, CategoryId = 8, Description = "Crisp and clear lemon-lime soda." }
            // Add more foods as needed with appropriate CategoryId and Description
        };

        // Index action to load the home page
        public IActionResult Index()
        {
            return View();
        }

        // Menu action to load the menu page with categories and foods
        public IActionResult Menu()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Pizza" },
                new Category { Id = 2, Name = "Burgers" },
                new Category { Id = 3, Name = "Pasta" },
                new Category { Id = 4, Name = "Noodles" },
                new Category { Id = 5, Name = "Pav Bhaji" },
                new Category { Id = 6, Name = "Vada Pav" },
                new Category { Id = 7, Name = "Fries" },
                new Category { Id = 8, Name = "Cold Drinks" }
            };

            ViewBag.Categories = categories;
            ViewBag.Foods = MockFoods;

            return View();
        }

        // AddToCart action to add a food item to the shopping cart
        [HttpPost]
        public IActionResult AddToCart(int foodId)
        {
            var food = GetFoodById(foodId);

            if (food != null)
            {
                var cartItem = ShoppingCart.FirstOrDefault(item => item.FoodId == foodId);

                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    ShoppingCart.Add(new CartItem
                    {
                        FoodId = food.Id,
                        Name = food.Name,
                        Price = food.Price,
                        Quantity = 1
                    });
                }
            }

            return RedirectToAction("Cart");
        }

        // Cart action to load the cart page with current shopping cart items
        public IActionResult Cart()
        {
            return View(ShoppingCart);
        }

        // RemoveFromCart action to remove a food item from the shopping cart
        [HttpPost]
        public IActionResult RemoveFromCart(int foodId)
        {
            var cartItem = ShoppingCart.FirstOrDefault(item => item.FoodId == foodId);

            if (cartItem != null)
            {
                ShoppingCart.Remove(cartItem);
            }

            return RedirectToAction("Cart");
        }

        // Checkout action to load the checkout page with cart items
public IActionResult Checkout()
{
    var total = ShoppingCart.Sum(item => item.Quantity * item.Price);
    ViewBag.Total = total;
    return View(ShoppingCart); // Pass ShoppingCart to the Checkout view
}

        // PlaceOrder action to process the order (placeholder)
        [HttpPost]
public IActionResult PlaceOrder(string name, string address)
{
    // Example: Here you would typically process the order and use the name and address
    // For demonstration, we're simply passing them to the Order Confirmation view

    ViewBag.Name = name;
    ViewBag.Address = address;
    ShoppingCart.Clear();
    return View("OrderConfirmation");

}

         
        // OrderConfirmation action to load the order confirmation page
        public IActionResult OrderConfirmation()
        {
            return View();
        }

        // Helper method to get a food item by its ID
        private Food GetFoodById(int id)
        {
            return MockFoods.FirstOrDefault(f => f.Id == id);
        }
    }
}