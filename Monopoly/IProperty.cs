namespace Monopoly;

public interface IProperty
{
    void AssignOwner(Player player);
    Player? Owner { get; }
    string Name { get; }
    int Cost { get; }
}