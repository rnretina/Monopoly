namespace Monopoly;

public class Player
{
    private int _doublesInRow;
    public int Money { get; set; }
    public bool IsInJail { get; set; }
    public string Name { get; }
    public int DodgeJailCards { get; set; }
    public List<IProperty> Property;
    public  Board Board { get; set; }
    public int Die1 { get; set; }
    public int Die2 { get; set; }
    public int Position { get; set; }
    public int DaysInJail { get; set; }
    
    public Player(string name, Board board)
    {
        Board = board;
        Property = new List<IProperty>();
        Name = name;
        Money = 2000000;
        _doublesInRow = 0;
        IsInJail = false;
        Position = 0;
        DodgeJailCards = 0;
        DaysInJail = 0;
    }
    public void Move()
    {
        if (IsInJail)
        {
            Board.Fields[Position].Execute(this);
            return;
        }
        RollDice();
        _doublesInRow = Die1 == Die2 ? _doublesInRow + 1 : 0;
        if (_doublesInRow == 3)
        {
            Console.WriteLine($"{Name} rolled double 3 times in a row and goes to jail");
            Board.Jail.Execute(this);
        }
        Position = (Position + Die1 + Die2) % 40;
        if (Position + Die1 + Die2 >= 40)
        {
            Console.WriteLine($"{Name} crossed Start and gets $200");
            Money += 200;
        }
        Board.Fields[Position].Execute(this);
        if (Die1 == Die2)
        {
            Move();
        }
    }

    public bool TryBuyProperty(IProperty property)
    {
        if (Money >= property.Cost && property.Owner == null)
        {
            Money -= property.Cost;
            property.AssignOwner(this);
            Property.Add(property);
            Console.WriteLine($"{Name} buys {property.Name} for ${property.Cost}");
            return true;
        }

        return false;
    }

    public void TryPayProperty(IProperty property)
    {
        while (property.Cost > Money)
        {
            if (Property.Count == 0)
            {
                //нечего продать и нечем платить
                Money = -1;
                return;
            }
            var mostExpensive = GetMostExpensiveProperty();
            Money += mostExpensive.Cost;
            mostExpensive.AssignOwner(null);
            Property.Remove(mostExpensive);
            Console.WriteLine($"{Name} sold {mostExpensive.Name} for ${mostExpensive.Cost}");
        }
        Money -= property.Cost;
        property.Owner.Money += property.Cost;
        Console.WriteLine($"{Name} pays Tax {property.Name} ${property.Cost} to {property.Owner.Name}");
    }
    
    public void TryUpgradeAvenue(Avenue avenue)
    {
        if (!CanUpgradeAvenue(avenue))
            return;
        Property.Remove(avenue); 
        var av = Board.Fields[Position] as Avenue; 
        av.UpgradeToNextLvl(this);
        Property.Add((Avenue)Board.Fields[Position]);
        Money -= avenue.Cost;
        Console.WriteLine($"{Name} upgrades {avenue.Name} to Lvl {((Avenue)Board.Fields[Position]).Lvl} for ${avenue.Cost}");
    }
    public void RollDice()
    {
        var random = new Random();
        Die1 = random.Next(1, 7);
        Die2 = random.Next(1, 7);
        Console.WriteLine($"{Name} rolled {Die1}-{Die2}");
    }

    public IProperty GetMostExpensiveProperty()
    {
        var max = Property[0];
        foreach (var item in Property)
        {
            if (item.Cost > max.Cost)
                max = item;
        }

        return max;
    }
    
    private bool CanUpgradeAvenue(Avenue avenue)
    {
        var suitableProperty = Property.Where(property => property is Avenue av
                                                           && av.Lvl >= avenue.Lvl && av.Color == avenue.Color).ToArray();
        return suitableProperty.Length == 3 && Money >= avenue.Cost;
    }
}