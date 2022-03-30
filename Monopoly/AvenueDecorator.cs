namespace Monopoly;

public abstract class AvenueDecorator : Avenue
{
    private Avenue _avenue;
    
    protected AvenueDecorator(Avenue avenue) : base(avenue.Name, avenue.BaseName,
        avenue.Color, avenue.Cost,  avenue.Lvl + 1, avenue.Owner)
    {
        _avenue = avenue;
    }
}