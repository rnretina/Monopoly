namespace Monopoly;

public class DecoratorLvl1 : AvenueDecorator
{
    private readonly Avenue _avenue;
    
    public DecoratorLvl1(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl 1";
    public override int Cost => _avenue.Cost * 2;
}

public class DecoratorLvl2 : AvenueDecorator
{
    private readonly Avenue _avenue;
    
    public DecoratorLvl2(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl 2";
    public override int Cost => _avenue.Cost * 2;
}

public class DecoratorLvl3 : AvenueDecorator
{
    private readonly Avenue _avenue;
    
    public DecoratorLvl3(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl 3";
    public override int Cost => _avenue.Cost * 2;
}

public class DecoratorHotel : AvenueDecorator
{
    private readonly Avenue _avenue;
    
    public DecoratorHotel(Avenue avenue) : base(avenue)
    {
        _avenue = avenue;
    }
    
    public override string Name => _avenue.BaseName + " Lvl Hotel";
    public override int Cost => _avenue.Cost * 2;
}