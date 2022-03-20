namespace Monopoly;

public class PublicUtility : Field, IProperty, IPayable
{
    private Player _owner;
    public int Cost { get; set; }
    public PublicUtility(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

    public void AssignOwner(Player player)
    {
        _owner = player;
    }
    public int GetCost() => Cost;
    public Player GetOwner() => _owner;
    public string GetName() => Name;
}