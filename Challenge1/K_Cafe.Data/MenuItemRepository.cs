// CRUD
public class MenuItemRepository
{
    private readonly List<MenuItem> _menu = new List<MenuItem>();
    private int _count;

    public bool AddNewItem(MenuItem menuItem)
    {
        int startingCount = _menu.Count;
        _menu.Add(menuItem);

        if (_menu.Count > startingCount)
        {
            AssignMenuNumber(menuItem);
            return true;
        }
        else
        {
            return false;
        }
    }

    // helper method to assign menu number
    private void AssignMenuNumber(MenuItem menuItem)
    {
        _count++;
        menuItem.MealNumber = _count;
    }

    public List<MenuItem> GetAllMeals()
    {
        return _menu;
    }

    public MenuItem GetMenuByMealNumber(int mealNum)
    {
        foreach (var item in _menu)
        {
            if (item.MealNumber == mealNum)
            {
                return item;
            }
        }
        return null;
    }

    public bool UpdateMenuItem(int mealNum, MenuItem updatedMenuItem)
    {
        MenuItem oldMenuItem = GetMenuByMealNumber(mealNum);

        if (oldMenuItem != null)
        {
            oldMenuItem.MealName = updatedMenuItem.MealName;
            oldMenuItem.Description = updatedMenuItem.Description;
            oldMenuItem.ListOfIngredients = updatedMenuItem.ListOfIngredients;
            oldMenuItem.MealPrice = updatedMenuItem.MealPrice;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeleteMenuItem(MenuItem existingMenuItem)
    {
        bool deleteResult = _menu.Remove(existingMenuItem);
        return deleteResult;
    }
}
