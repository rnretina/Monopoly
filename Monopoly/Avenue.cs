namespace Monopoly;

public class Avenue : Field, IProperty
{
    public enum AvenueColor
    {
        Brown, Blue, Pink, Orange, Red, Yellow, Green, Black,
    }
    
    public Player? Owner { get; private set; }
    public virtual int Cost { get; }
    public new virtual string Name { get;  }
    public int Lvl { get; }
    public AvenueColor Color { get; }
    public string BaseName { get; }
    
    public Avenue(string name, string baseName, AvenueColor color, int cost, int lvl, Player owner)
    {
        Lvl = lvl;
        Name = name;
        BaseName = baseName;
        Color = color;
        Cost = cost;
        Owner = owner;
    }

    public virtual void UpgradeToNextLvl(Player player)
    {
        player.Board.Fields[player.Position] = new DecoratorLvl1(this);
    }
    
    public void AssignOwner(Player player)
    {
        Owner = player;
    }

    public override void Execute(Player player)
    {
        player.TryUpgradeAvenue(this);
        if (player.TryBuyProperty(this)) return;
        if (Owner != player && Owner != null)
        {
           player.TryPayProperty(this);
        }
    }
}