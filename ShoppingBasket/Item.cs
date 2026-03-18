using System;

namespace ShoppingBasket;

public class Item
{
    public string Name { get; }
    public double Weight { get; }

    public Item(string name, double weight)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        if (weight <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than zero.");
        }

        Name = name.Trim();
        Weight = weight;
    }
}

