using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket;
 
/// <summary>
/// Shopping basket logic
/// </summary>
public class ShoppingBasketService
{
    private const double MaxWeightKg = 20.0;

    /// <summary>
    /// Picks the heaviest items that fit within the limit.
    /// </summary>
    public List<Item> FillBasketHeaviestFirst(IEnumerable<Item> shoppingList)
    {
        if (shoppingList == null)
        {
            throw new ArgumentNullException(nameof(shoppingList));
        }

        var basket = new List<Item>();
        double currentWeight = 0;

        var orderedItems = shoppingList.OrderByDescending(i => i.Weight);

        foreach (var item in orderedItems)
        {
            if (currentWeight + item.Weight <= MaxWeightKg)
            {
                basket.Add(item);
                currentWeight += item.Weight;
            }
        }

        return basket;
    }
}
