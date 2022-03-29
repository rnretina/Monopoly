namespace Monopoly;

public abstract class AvenueDecorator : Avenue
{
    private Avenue _avenue;
    
    protected AvenueDecorator(Avenue avenue) : base(avenue.Name, avenue.Color, avenue.GetCost(), avenue.Lvl + 1, avenue.GetOwner())
    {
        _avenue = avenue;
    }
}