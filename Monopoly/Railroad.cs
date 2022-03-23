using System.Globalization;
using System.Security.Cryptography;

namespace Monopoly;

public class Railroad : Field, IProperty
{
    private int _cost;
    private Player _owner;

    public Railroad(string name, int cost)
    {
        Name = name;
        _cost = cost;
    }

    public void AssignOwner(Player player)
    {
        _owner = player;
    }

    public int GetCost() => _cost;
    public Player GetOwner() => _owner;
    public string GetName() => Name;
}