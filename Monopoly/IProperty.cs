namespace Monopoly;

public interface IProperty
{
    void AssignOwner(Player player);
    Player? Owner { get; set; }
    string Name { get; set; }
    int Cost { get; set; }
}