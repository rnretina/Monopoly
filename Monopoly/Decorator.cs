namespace Monopoly;

public class Decorator1 : AvenueDecorator
{
    private Avenue _avenue;
    
    public Decorator1(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string GetName() => _avenue.Name + " Lvl 1";
    public new virtual int GetCost() => _avenue.GetCost() * 2;
}

public class Decorator2 : AvenueDecorator
{
    private Avenue _avenue;
    
    public Decorator2(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string GetName() => _avenue.Name + " Lvl 2";
    public new virtual int GetCost() => _avenue.GetCost() * 3;
}

public class Decorator3 : AvenueDecorator
{
    private Avenue _avenue;
    
    public Decorator3(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string GetName() => _avenue.Name + " Lvl 3";
    public new virtual int GetCost() => _avenue.GetCost() * 4;
}

public class DecoratorHotel : AvenueDecorator
{
    private Avenue _avenue;
    
    public DecoratorHotel(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string GetName() => _avenue.Name + " Lvl Hotel";
    public new virtual int GetCost() => _avenue.GetCost() * 8;
}