namespace Monopoly;

public class Railroad : Field, IProperty
{
    public int Cost { get; }
    public string Name { get;  }
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

    public override void Execute(Player player)
    {
        if (player.TryBuyProperty(this)) return;
        if (Owner != player && Owner != null)
        {
            player.TryPayProperty(this);
        }
    }
}