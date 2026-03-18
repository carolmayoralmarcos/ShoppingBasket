using System.Collections.Generic;
using System.Linq;
using ShoppingBasket;
using Xunit;

namespace ShoppingBasket.Tests;

public class ShoppingBasketServiceTests
{
    [Fact]
    public void Should_Select_Heaviest_Items_First()
    {
        var service = new ShoppingBasketService();

        var shoppingList = new List<Item>
        {
            new("Apples", 5),
            new("Milk", 3),
            new("Rice", 10),
            new("Cheese", 4)
        };

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        Assert.Equal("Rice", basket[0].Name);
    }

    [Fact]
    public void Should_Not_Exceed_20kg()
    {
        var service = new ShoppingBasketService();

        var shoppingList = new List<Item>
        {
            new("Item1", 12),
            new("Item2", 9),
            new("Item3", 8)
        };

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        double totalWeight = basket.Sum(i => i.Weight);

        Assert.True(totalWeight <= 20);
    }

    [Fact]
    public void Should_Return_Empty_When_All_Items_Too_Heavy()
    {
        var service = new ShoppingBasketService();

        var shoppingList = new List<Item>
        {
            new("Heavy1", 25),
            new("Heavy2", 30)
        };

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        Assert.Empty(basket);
    }

    [Fact]
    public void Should_Return_Empty_When_ShoppingList_Is_Empty()
    {
        var service = new ShoppingBasketService();
        var shoppingList = new List<Item>();

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        Assert.Empty(basket);
    }

    [Fact]
    public void Should_Fill_Exactly_20kg_When_Possible()
    {
        var service = new ShoppingBasketService();

        var shoppingList = new List<Item>
        {
            new("Flour", 10),
            new("Milk", 5),
            new("Eggs", 3),
            new("Sugar", 2)
        };

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        var totalWeight = basket.Sum(i => i.Weight);

        Assert.Equal(20, totalWeight);
    }

    [Fact]
    public void Should_Skip_Too_Heavy_Items()
    {
        var service = new ShoppingBasketService();

        var shoppingList = new List<Item>
        {
            new("Big", 19),
            new("TooBig", 2),
            new("Small", 1)
        };

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        Assert.Equal(2, basket.Count);
        Assert.Equal("Big", basket[0].Name);
        Assert.Equal("Small", basket[1].Name);
        Assert.Equal(20, basket.Sum(i => i.Weight));
    }

    [Fact]
    public void Should_Allow_Item_With_Exactly_20kg()
    {
        var service = new ShoppingBasketService();

        var shoppingList = new List<Item>
        {
            new("Heavier", 20),
            new("Lighter", 1)
        };

        var basket = service.FillBasketHeaviestFirst(shoppingList);

        Assert.Single(basket);
        Assert.Equal("Heavier", basket[0].Name);
    }

    [Fact]
    public void Should_Throw_When_ShoppingList_Is_Null()
    {
        var service = new ShoppingBasketService();

        Assert.Throws<ArgumentNullException>(() =>
            service.FillBasketHeaviestFirst(null!));
    }

}
