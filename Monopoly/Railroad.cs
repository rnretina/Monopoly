using System.Globalization;
using System.Security.Cryptography;

namespace Monopoly;

public class Railroad : Field, IProperty
{
    public int Cost { get; set; }
    public string Name { get; set; }
    public Player? Owner { get; set; }

    public Railroad(string name, int cost)
    {
        Owner = null;
        Name = name;
        Cost = cost;
    }

    public void AssignOwner(Player player)
    {
        Owner = player;
    }
}