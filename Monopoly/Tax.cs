namespace Monopoly;

public class Tax : Field, IPayable 
{
    private int _cost;
    public Tax(string name, int cost)
    {
        Name = name;
        _cost = cost;
    }

    public int GetCost() => _cost;
    public string GetName() => Name;
}