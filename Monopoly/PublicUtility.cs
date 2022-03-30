namespace Monopoly;

public class PublicUtility : Field, IProperty
{
    public Player? Owner { get; set; }
    public int Cost { get; set; }
    public PublicUtility(string name, int cost)
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