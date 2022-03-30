namespace Monopoly;

public class Avenue : Field, IProperty
{
    public enum AvenueColor
    {
        Brown, Blue, Pink, Orange, Red, Yellow, Green, Black,
    }
    
    public Player? Owner { get; set; }
    public virtual int Cost { get; set; }
    public new virtual string Name { get; set; }
    public int Lvl { get; set; }
    public AvenueColor Color { get; set; }
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
    
    public void AssignOwner(Player player)
    {
        Owner = player;
    }
}