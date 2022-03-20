namespace Monopoly;

public class Avenue : Field, IProperty, IPayable
{
    public enum AvenueColor
    {
        Brown, Blue, Pink, Orange, Red, Yellow, Green, Black
    }

    private int _cost;
    private Player _owner;
    public AvenueColor Color { get; }

    public Avenue(string name, AvenueColor color, int cost)
    {
        Name = name;
        Color = color;
        _cost = cost;
    }

    public void AssignOwner(Player player)
    {
        _owner = player;
    }

    public Player GetOwner() => _owner;
    public int GetCost() => _cost;
    public string GetName() => Name;

}