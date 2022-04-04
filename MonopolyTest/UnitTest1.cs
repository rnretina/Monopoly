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
        _player1 = new Player("pl1", _board);
        _player2 = new Player("pl2", _board);
        _avenue = new Avenue("Montana", "Montana", Avenue.AvenueColor.Blue, 100, 0, _player1);
    }

    [Test]
    public void AssignOwnerNull_Should_RemoveOwner()
    {
        _avenue.AssignOwner(null);
        Assert.IsTrue(_avenue.Owner == null);
    }
    
    [Test]
    public void AssignOwner_Should_ChangeOwner()
    {
        _avenue.AssignOwner(_player2);
        Assert.IsTrue(_avenue.Owner == _player2);
    }
    
    [Test]
    public void SequentialUpgradeAvenue_ShouldNot_ChangeOwner()
    {
        var decoratedAvenue1 = new DecoratorLvl1(_avenue);
        Assert.IsTrue(decoratedAvenue1.Owner == _avenue.Owner);
        var decoratedAvenue2 = new DecoratorLvl2(decoratedAvenue1);
        Assert.IsTrue(decoratedAvenue2.Owner == decoratedAvenue1.Owner);
        var decoratedAvenue3 = new DecoratorLvl3(decoratedAvenue2);
        Assert.IsTrue(decoratedAvenue3.Owner == decoratedAvenue2.Owner);
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.IsTrue(decoratedAvenue4.Owner == decoratedAvenue3.Owner);
    }

    [Test]
    public void SequentialUpgradeAvenue_Should_ChangeName()
    {
        var decoratedAvenue1 = new DecoratorLvl1(_avenue);
        Assert.AreEqual("Montana Lvl 1",decoratedAvenue1.Name);
        var decoratedAvenue2 = new DecoratorLvl2(decoratedAvenue1);
        Assert.AreEqual("Montana Lvl 2",decoratedAvenue2.Name);
        var decoratedAvenue3 = new DecoratorLvl3(decoratedAvenue2);
        Assert.AreEqual("Montana Lvl 3",decoratedAvenue3.Name);
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.AreEqual("Montana Lvl Hotel",decoratedAvenue4.Name);
    }

    [Test]
    public void SequentialUpgradeAvenue_Should_ReturnCorrectCost()
    {
        var decoratedAvenue1 = new DecoratorLvl1(_avenue);
        Assert.AreEqual(_avenue.Cost * 2, decoratedAvenue1.Cost);
        var decoratedAvenue2 = new DecoratorLvl2(decoratedAvenue1);
        Assert.AreEqual(decoratedAvenue1.Cost * 2, decoratedAvenue2.Cost);
        var decoratedAvenue3 = new DecoratorLvl3(decoratedAvenue2);
        Assert.AreEqual(decoratedAvenue2.Cost * 2, decoratedAvenue3.Cost);
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.AreEqual(decoratedAvenue3.Cost * 2, decoratedAvenue4.Cost);
    }
    
    [Test]
    public void SequentialUpgradeAvenue_Should_ChangeLvl()
    {
        var decoratedAvenue1 = new DecoratorLvl1(_avenue);
        Assert.AreEqual(1, decoratedAvenue1.Lvl);
        var decoratedAvenue2 = new DecoratorLvl2(decoratedAvenue1);
        Assert.AreEqual(2, decoratedAvenue2.Lvl);
        var decoratedAvenue3 = new DecoratorLvl3(decoratedAvenue2);
        Assert.AreEqual(3, decoratedAvenue3.Lvl);
        var decoratedAvenue4 = new DecoratorHotel(decoratedAvenue3);
        Assert.AreEqual(4, decoratedAvenue4.Lvl);
    }
}
