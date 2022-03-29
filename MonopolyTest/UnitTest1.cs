using Monopoly;
using NUnit.Framework;

namespace MonopolyTest;

public class Tests
{
    private Board _board;
    private Player _player1;
    private Player _player2;
    private Avenue _avenue;
    [SetUp]
    public void Setup()
    {
        var _board = new Board();
        _board.Initialize();
        _player1 = new Player("pl1", _board);
        _player2 = new Player("pl2", _board);
        _avenue = new Avenue("Montana", Avenue.AvenueColor.Blue, 100, 0, _player1);
    }

    [Test]
    public void AssignOwnerNull_Should_RemoveOwner()
    {
        _avenue.AssignOwner(null);
        Assert.IsTrue(_avenue.GetOwner() == null);
    }
    
    [Test]
    public void AssignOwner_Should_ChangeOwner()
    {
        _avenue.AssignOwner(_player2);
        Assert.IsTrue(_avenue.GetOwner() == _player2);
    }
    
    [Test]
    public void SequentialUpgradeAvenue_ShouldNot_ChangeOwner()
    {
        var decoratedAvenue1 = new Decorator1(_avenue);
        Assert.IsTrue(decoratedAvenue1.GetOwner() == _avenue.GetOwner());
        var decoratedAvenue2 = new Decorator2(decoratedAvenue1);
        Assert.IsTrue(decoratedAvenue2.GetOwner() == decoratedAvenue1.GetOwner());
        var decoratedAvenue3 = new Decorator3(decoratedAvenue2);
        Assert.IsTrue(decoratedAvenue3.GetOwner() == decoratedAvenue2.GetOwner());
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.IsTrue(decoratedAvenue4.GetOwner() == decoratedAvenue3.GetOwner());
    }

    [Test]
    public void SequentialUpgradeAvenue_Should_ChangeName()
    {
        var decoratedAvenue1 = new Decorator1(_avenue);
        Assert.AreEqual("Montana Lvl 1",decoratedAvenue1.GetName());
        var decoratedAvenue2 = new Decorator2(decoratedAvenue1);
        Assert.AreEqual("Montana Lvl 2",decoratedAvenue2.GetName());
        var decoratedAvenue3 = new Decorator3(decoratedAvenue2);
        Assert.AreEqual("Montana Lvl 3",decoratedAvenue3.GetName());
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.AreEqual("Montana Lvl Hotel",decoratedAvenue4.GetName());
    }

    [Test]
    public void SequentialUpgradeAvenue_Should_ReturnCorrectCost()
    {
        var decoratedAvenue1 = new Decorator1(_avenue);
        Assert.AreEqual(_avenue.Cost * 2, decoratedAvenue1.GetCost());
        var decoratedAvenue2 = new Decorator2(decoratedAvenue1);
        Assert.AreEqual(decoratedAvenue1.Cost * 3, decoratedAvenue2.GetCost());
        var decoratedAvenue3 = new Decorator3(decoratedAvenue2);
        Assert.AreEqual(decoratedAvenue2.Cost * 4, decoratedAvenue3.GetCost());
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.AreEqual(decoratedAvenue3.Cost * 8, decoratedAvenue4.GetCost());
    }
    
    [Test]
    public void SequentialUpgradeAvenue_Should_ChangeLvl()
    {
        var decoratedAvenue1 = new Decorator1(_avenue);
        Assert.AreEqual(1, decoratedAvenue1.Lvl);
        var decoratedAvenue2 = new Decorator2(decoratedAvenue1);
        Assert.AreEqual(2, decoratedAvenue2.Lvl);
        var decoratedAvenue3 = new Decorator3(decoratedAvenue2);
        Assert.AreEqual(3, decoratedAvenue3.Lvl);
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.AreEqual(4, decoratedAvenue4.Lvl);
    }
}
