using System.Runtime.Serialization;
namespace K_Cafe.Tests;
using Xunit;


public class K_Cafe_Repository_Tests
{
    private MenuItemRepository _globalRepo;
    private MenuItem _itemA;
    private MenuItem _itemB;
    private MenuItem _itemC;

    public K_Cafe_Repository_Tests()
    {
        _globalRepo = new MenuItemRepository();
        _itemA = new MenuItem(1, "hamburger", "basic hamburger", "beef and bun", 5.00d);
        _itemB = new MenuItem(2, "pizza", "cheese pizza", "marinara sauce, cheese, and dough", 8.00d);
        _itemC = new MenuItem(3, "wings", "chicken wings", "chicken wing and buffalo sauce", 7.50d);

        _globalRepo.AddNewItem(_itemA);
        _globalRepo.AddNewItem(_itemB);
        _globalRepo.AddNewItem(_itemC);
    }

    [Fact]
    public void AddToMenu_ShouldReturnBoolean()
    {
        MenuItem item = new MenuItem();
        MenuItemRepository repository = new MenuItemRepository();

        bool addResult = repository.AddNewItem(item);

        Assert.True(addResult);
    }

    [Fact]
    public void Get_DatabaseInfo_Should_Return_CorrectCollection()
    {
        MenuItem content = new MenuItem();
        MenuItemRepository repository = new MenuItemRepository();

        repository.AddNewItem(content);

        List<MenuItem> contents = repository.GetAllMeals();

        bool databaseHasContents = contents.Contains(content);

        Assert.True(databaseHasContents);
    }

    [Fact]
    public void Get_MenuItem_By_Meal_Number_Returns_MenuItem()
    {
        MenuItem searchResult = _globalRepo.GetMenuByMealNumber(1);

        Assert.Equal(searchResult, _itemA);
    }

    [Fact]
    public void UpdateExistingMenuItem_Should_Return_True()
    {
        MenuItem updatedContent = new MenuItem(
            1, "cheeseburger", "Burger with cheese", "burger, cheese, bun", 6.00d);

        bool updateResult = _globalRepo.UpdateMenuItem(1, updatedContent);

        Assert.True(updateResult);

        MenuItem modifiedContent = _globalRepo.GetMenuByMealNumber(1);
        System.Console.WriteLine(modifiedContent.Description);
    }

    [Fact]
    public void Delete_Existing_Content_Should_Return_True()
    {
        var item = _globalRepo.GetMenuByMealNumber(1);

        bool removeResult = _globalRepo.DeleteMenuItem(item);

        Assert.True(removeResult);
        Assert.Equal(2, _globalRepo.GetAllMeals().Count);
    }
}