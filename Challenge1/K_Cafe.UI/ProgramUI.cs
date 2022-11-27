using System.Collections.Generic;

public class ProgramUI
{
    MenuItemRepository _menuRepo = new MenuItemRepository();
    public void Run()
    {
        SeedMenu();
        RunApplication();
    }
    bool keepRunning = true;
    public void RunApplication()
    {
        while (keepRunning)
        {
            Console.Clear();
            System.Console.WriteLine("Welcome to Komodo Cafe!\n" +
            "-----------------------");

            System.Console.WriteLine("Please select from the following options:\n" +
            "1. Create new Menu Item\n" +
            "2. View All Menu Items\n" +
            "3. View Menu by Meal Number\n" +
            "4. Update Menu Item\n" +
            "5. Delete Menu Item\n" +
            "6. Exit Application");

            string menuInput = Console.ReadLine();
            switch (menuInput)
            {
                case "1":
                    CreateNewMenuItem();
                    break;
                case "2":
                    ViewAllMenuItems();
                    break;
                case "3":
                    ViewMenuItemByMealNumber();
                    break;
                case "4":
                    UpdateMenuItem();
                    break;
                case "5":
                    DeleteMenuItem();
                    break;
                case "6":
                    ExitApplication();
                    break;
                default:
                    System.Console.WriteLine("Sorry, input not recognized. Press enter to return home.");
                    break;
            }

            System.Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }

    private void CreateNewMenuItem()
    {
        Console.Clear();
        MenuItem newMenuItem = new MenuItem();

        System.Console.WriteLine("Please enter the new Menu Name: ");
        newMenuItem.MealName = Console.ReadLine();

        System.Console.WriteLine("Please enter a description for the menu item.");
        newMenuItem.Description = Console.ReadLine();

        System.Console.WriteLine("Please enter the ingredients used in the menu item.");
        newMenuItem.ListOfIngredients = Console.ReadLine();

        System.Console.WriteLine("Please enter a Price for the menu item.");
        try
        {
            newMenuItem.MealPrice = Double.Parse(Console.ReadLine());
        }
        catch
        {
            System.Console.WriteLine("Sorry, input not recognized. Please try again.");
        }

        bool itemAdded = _menuRepo.AddNewItem(newMenuItem);
        if (itemAdded)
        {
            Console.Clear();
            System.Console.WriteLine($"{newMenuItem.MealName} was successfully added!");
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine($"Failed to add new menu item.");
        }
    }

    // private void CreateNewMenuItem()
    // {
    //     Console.Clear();

    //     MenuItem newItem = new AddNewMenuItemInformation();

    //     if (_menuRepo.AddNewItem(newItem))
    //     {
    //         System.Console.WriteLine($"{newItem.MealName} was successfully added to the Menu!");
    //     }
    //     else
    //     {
    //         System.Console.WriteLine("Sorry, the item was not added to the Menu.");
    //     }
    //     Console.ReadKey();
    // }

    // private MenuItem AddNewMenuItemInformation()
    // {
    //     MenuItem menuItem = new MenuItem();

    //     System.Console.WriteLine("Please enter the new Menu Name: ");
    //     menuItem.MealName = Console.ReadLine();

    //     System.Console.WriteLine("Please enter a description for the menu item.");
    //     menuItem.Description = Console.ReadLine();

    //     System.Console.WriteLine("Please enter the ingredients used in the menu item.");
    //     menuItem.ListOfIngredients = Console.ReadLine();

    //     System.Console.WriteLine("Please enter a Price for the menu item.");
    //     menuItem.MealPrice = Double.Parse(Console.ReadLine());

    //     return menuItem;
    // }

    private void ViewAllMenuItems()
    {
        Console.Clear();

        List<MenuItem> menuItems = _menuRepo.GetAllMeals();

        if (menuItems.Count > 0)
        {
            foreach (var items in menuItems)
            {
                ShowMenuItem(items);
            }
        }
        else
        {
            System.Console.WriteLine("Sorry, there are no menu items.");
        }
        Console.ReadKey();
    }

    private void ShowMenuItem(MenuItem menuItem)
    {
        System.Console.WriteLine($"Number {menuItem.MealNumber}: {menuItem.MealName}\n" +
        $"{menuItem.Description}\n" +
        $"Ingredients: {menuItem.ListOfIngredients}\n" +
        $"Price: ${menuItem.MealPrice}\n" +
        "----------------");
    }

    private void ViewMenuItemByMealNumber()
    {
        Console.Clear();
        System.Console.WriteLine("Please enter a Meal Number to search:");
        int userInput = int.Parse(Console.ReadLine());

        MenuItem menuItem = _menuRepo.GetMenuByMealNumber(userInput);

        if (menuItem != null)
        {
            ShowMenuItem(menuItem);
        }
        else
        {
            System.Console.WriteLine("Sorry, there is no meal associated with that input.");
        }
        Console.ReadKey();
    }

    private void UpdateMenuItem()
    {
        Console.Clear();

        System.Console.WriteLine("Please enter a Meal Number to update: ");

        try
        {
            int userInputUpdate = int.Parse(Console.ReadLine());
            MenuItem itemToUpdate = _menuRepo.GetMenuByMealNumber(userInputUpdate);
            Console.Clear();
            if (itemToUpdate != null)
            {
                MenuItem updatedMenuItem = new MenuItem();

                System.Console.WriteLine("Please enter Name for the new item.");
                updatedMenuItem.MealName = Console.ReadLine();

                System.Console.WriteLine("Please enter a description for the new item");
                updatedMenuItem.Description = Console.ReadLine();

                System.Console.WriteLine("Please enter a list of ingredients for the new item. Separate each item by a comma.");
                updatedMenuItem.ListOfIngredients = Console.ReadLine();

                System.Console.WriteLine("Please enter a price for the new item.");
                updatedMenuItem.MealPrice = Double.Parse(Console.ReadLine());

                bool updateResult = _menuRepo.UpdateMenuItem(userInputUpdate, updatedMenuItem);

                if (updateResult)
                {
                    Console.Clear();
                    System.Console.WriteLine("The menu item was successfully updated.");
                }
                else
                {
                    System.Console.WriteLine("Sorry, the menu item was unable to be updated.");
                }
            }
            else
            {
                System.Console.WriteLine("Sorry, there is no menu item with that number.");
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Sorry, there is no menu item with that number.");
        }
    }

    private void DeleteMenuItem()
    {
        Console.Clear();
        System.Console.WriteLine("Which menu item would you like to remove?");

        List<MenuItem> menuList = _menuRepo.GetAllMeals();

        if (menuList.Count > 0)
        {
            int count = 0;

            foreach (MenuItem menuItem in menuList)
            {
                count++;
                System.Console.WriteLine($"{count}.{menuItem.MealName}");
            }

            int targetMenuNumber = int.Parse(Console.ReadLine());
            int targetIndex = targetMenuNumber - 1;

            if (targetIndex >= 0 && targetIndex < menuList.Count)
            {
                MenuItem desiredMenuItemCount = menuList[targetIndex];

                if (_menuRepo.DeleteMenuItem(desiredMenuItemCount))
                {
                    System.Console.WriteLine($"{desiredMenuItemCount.MealName} was successfully deleted.");
                }
                else
                {
                    System.Console.WriteLine($"{desiredMenuItemCount.MealName} was unable to be deleted.");
                }
            }
            else
            {
                System.Console.WriteLine("Sorry, invalid menu number selection. Please try again.");
            }
        }
        else
        {
            System.Console.WriteLine("There is no menu item to delete.");
        }
        Console.ReadKey();
    }

    private void ExitApplication()
    {
        keepRunning = false;
    }

    private void SeedMenu()
    {
        MenuItem chickenSandwich = new MenuItem(_menuRepo.GetAllMeals().Count + 1, "Chicken Sandwich and Fries", "A breaded chicken sandwich with a side a shoestring fries.", "Bread, Chicken, Pickles, and Fries", 9.99d);
        MenuItem chickenWrap = new MenuItem(_menuRepo.GetAllMeals().Count + 1, "Buffalo Chicken Wrap and Fries", "Breaded Buffalo Chicken wrapped in a flour tortilla with a side a shoestring fries.", "Flour Tortilla, Buffalo Chicken, Lettuce, Ranch, Cheese, and Fries", 10.99d);
        MenuItem wings = new MenuItem(_menuRepo.GetAllMeals().Count + 1, "Pepperoni Pizza", "A New York Style Pepperoni Pizza.", "Dough, Mariana Sauce, Pepperoni, Mozzerella Cheese", 14.99d);
        MenuItem pepperoniPizza = new MenuItem(_menuRepo.GetAllMeals().Count + 1, "Chicken Wings", "10 bone-in wings tossed in our award winning sauce", "Chicken Wings and buffalo sauce", 8.99d);
        MenuItem chickenSalad = new MenuItem(_menuRepo.GetAllMeals().Count + 1, "Chicken Salad", "A salad topped with fried or grilled chicken, eggs, tomatoes, cheese, and our house dressing.", "Iceberg lettuce, chicken, egg, tomato, dressing", 10.99d);

        _menuRepo.AddNewItem(chickenSandwich);
        _menuRepo.AddNewItem(chickenWrap);
        _menuRepo.AddNewItem(wings);
        _menuRepo.AddNewItem(pepperoniPizza);
        _menuRepo.AddNewItem(chickenSalad);
    }
}
