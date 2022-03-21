namespace Monopoly;

public class PublicUtility : Field, IProperty
{
    private Player _owner;
    private readonly int _cost;
    public PublicUtility(string name, int cost)
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