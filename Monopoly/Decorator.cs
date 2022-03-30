namespace Monopoly;

public class Decorator1 : AvenueDecorator
{
    private Avenue _avenue;
    
    public Decorator1(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl 1";
    public override int Cost => _avenue.Cost * 2;
}

public class Decorator2 : AvenueDecorator
{
    private Avenue _avenue;
    
    public Decorator2(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl 2";
    public override int Cost => _avenue.Cost * 2;
}

public class Decorator3 : AvenueDecorator
{
    private Avenue _avenue;
    
    public Decorator3(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl 3";
    public override int Cost => _avenue.Cost * 2;
}

public class DecoratorHotel : AvenueDecorator
{
    private Avenue _avenue;
    
    public DecoratorHotel(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl Hotel";
    public override int Cost => _avenue.Cost * 2;
}