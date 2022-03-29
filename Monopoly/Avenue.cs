namespace Monopoly;

public class Avenue : Field, IProperty
{
    public enum AvenueColor
    {
        Brown, Blue, Pink, Orange, Red, Yellow, Green, Black,
    }
    
    private Player? _owner;
    public int Lvl { get; set; }
    public AvenueColor Color { get; set; }
    public int Cost {get; set;}

    public Avenue(string name, AvenueColor color, int cost, int lvl, Player owner)
    {
        Lvl = lvl;
        Name = name;
        Color = color;
        Cost = cost;
        _owner = owner;
    }
    
    public void AssignOwner(Player player)
    {
        _owner = player;
    }

    public Player? GetOwner() => _owner;
    public int GetCost() => Cost;
    public virtual string GetName() => Name;
}