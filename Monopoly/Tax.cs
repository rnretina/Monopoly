namespace Monopoly;

public class Tax : Field
{
    public int Cost { get;}
    
    public Tax(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }
}