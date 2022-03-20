namespace Monopoly;

public interface IProperty
{
    void AssignOwner(Player player);
    Player? GetOwner();
    string GetName();
    int GetCost();
}