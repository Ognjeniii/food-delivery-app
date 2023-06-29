using System.Diagnostics.Metrics;

namespace Diplomski.Models
{
    public class Helper
    {
        public static double counter { get; set; } = 0;
        public static List<string> ordersList = new List<string>();
        public static List<string> foodType = new List<string>();
        public static List<string> restaurant = new List<string>();
        public static void AddParams(double bill, string foodName, string typeOfFood, string restaurantName)
        {
            counter += bill;
            counter = Math.Round(counter, 2);
            ordersList.Add(foodName);
            foodType.Add(typeOfFood);
            restaurant.Add(restaurantName);
        }

        public static void ClearOrder()
        {
            Models.Helper.counter = 0;
            Models.Helper.ordersList.Clear();
            Models.Helper.foodType.Clear();
            Models.Helper.restaurant.Clear();
        }
    }
}
