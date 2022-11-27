
using System.Collections.Generic;
public class MenuItem
{
    public MenuItem() { }

    public MenuItem(int mealNumber, string mealName, string description, string listOfIngredients, double mealPrice)
    {
        MealNumber = mealNumber;
        MealName = mealName;
        Description = description;
        ListOfIngredients = listOfIngredients;
        MealPrice = mealPrice;
    }


    public int MealNumber { get; set; }
    public string MealName { get; set; }
    public string Description { get; set; }
    public string ListOfIngredients { get; set; }
    public double MealPrice { get; set; }
}
