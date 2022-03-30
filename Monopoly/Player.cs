using System.Diagnostics;

namespace Monopoly;

public class Player
{
    private Board _board;
    private List<IProperty> _property;
    private int _daysInJail;
    private int _position;
    private readonly string _name;
    private int _die1, _die2;
    private int _doublesInRow;
    private bool _isInJail;
    private int _dodgeJailCards;
    public int Money { get; private set; }
    
    public Player(string name, Board board)
    {
        _board = board;
        _property = new List<IProperty>();
        _name = name;
        Money = 2000;
        _doublesInRow = 0;
        _isInJail = false;
        _position = 0;
        _dodgeJailCards = 0;
        _daysInJail = 0;
    }
    public void Move()
    {
        RollDice();
        if (_isInJail)
        {
            TryGetOutOfJail();
            if (_isInJail) return;
        }

        _doublesInRow = _die1 == _die2 ? _doublesInRow + 1 : 0;
        if (_doublesInRow == 3)
        {
            Console.WriteLine($"{_name} rolled double 3 times in a row and goes to jail");
            GoToJail();
        }
        _position = (_position + _die1 + _die2) % 40;
        if (_position + _die1 + _die2 >= 40)
        {
            Console.WriteLine($"{_name} crossed Start and gets $200");
            Money += 200;
        }
        ExecuteOrder(_board.Fields[_position]);
        if (_die1 == _die2)
        {
            Move();
        }
    }

    private void ExecuteOrder(Field field)
    {
        switch (field)
        {
            case Avenue avenue:
                PropertyMove(avenue);
                break;
            case Railroad railroad:
                PropertyMove(railroad);
                break;
            case Tax tax:
                TryPay(tax);
                break;
            case Jail:
                if (!_isInJail)
                    GoToJail();
                else
                    Console.WriteLine($"{_name} stays in jail");
                break;
            case PublicUtility publicUtility:
                PublicUtilityMove(publicUtility);
                break;
            case Chance:
                TakeChanceCard();
                break;
        }
    }

    private void PublicUtilityMove(PublicUtility publicUtility)
    {
        if (TryBuyProperty(publicUtility)) return;
        if (publicUtility.Owner != this && publicUtility.Owner != null)
        {
            var multiplier = (_board.PublicUtility[0].Owner == _board.PublicUtility[1].Owner) ? 10 : 4;
            var price = multiplier * (_die1 + _die2);
            while (price > Money)
            {
                if (_property.Count == 0)
                {
                    // нечего продать и нечем оплатить
                    Money = -1;
                    return;
                }
                var mostExpensive = GetMostExpensiveProperty();
                Money += mostExpensive.Cost;
                mostExpensive.AssignOwner(null);
                _property.Remove(mostExpensive);
                Console.WriteLine($"{_name} sold {mostExpensive.Name} for ${mostExpensive.Cost}");
            }

            Money -= price;
            publicUtility.Owner.Money += price;
            Console.WriteLine($"{_name} pays ${price} for visiting {publicUtility.Name}");
        }
    }

    private void PropertyMove(IProperty property)
    {
        if (TryBuyProperty(property)) return;
        if (property is Avenue avenue)
        {
            TryUpgradeAvenue(avenue);
        }
        if (property.Owner != null && property.Owner != this)
        {
            TryPayProperty(property);
        }
    }

    private bool TryBuyProperty(IProperty property)
    {
        if (Money >= property.Cost && property.Owner == null)
        {
            Money -= property.Cost;
            property.AssignOwner(this);
            _property.Add(property);
            Console.WriteLine($"{_name} buys {property.Name} for ${property.Cost}");
            return true;
        }

        return false;
    }

    private void TryPayProperty(IProperty property)
    {
        while (property.Cost > Money)
        {
            if (_property.Count == 0)
            {
                //нечего продать и нечем платить
                Money = -1;
                return;
            }
            var mostExpensive = GetMostExpensiveProperty();
            Money += mostExpensive.Cost;
            mostExpensive.AssignOwner(null);
            _property.Remove(mostExpensive);
            Console.WriteLine($"{_name} sold {mostExpensive.Name} for ${mostExpensive.Cost}");
        }
        Money -= property.Cost;
        property.Owner.Money += property.Cost;
        Console.WriteLine($"{_name} pays Tax {property.Name} ${property.Cost} to {property.Owner._name}");
    }
    
    private void TryPay(Tax tax)
    {
        while (Money < tax.Cost)
        {
            if (_property.Count == 0)
            {
                // нечего продать и нечем оплатить
                Money = -1;
                return;
            }
            var mostExpensive = GetMostExpensiveProperty();
            Money += mostExpensive.Cost;
            mostExpensive.AssignOwner(null);
            _property.Remove(mostExpensive);
            Console.WriteLine($"{_name} sold {mostExpensive.Name} for ${mostExpensive.Cost}");
        }

        Console.WriteLine($"{_name} pays {tax.Name}");
        Money -= tax.Cost;
    }

    private void TakeChanceCard()
    {
        Console.WriteLine($"{_name} takes chance card");
        var random = new Random();
        var value = random.Next(5);
        switch (value)
        {
            case 0:
                TryPayChanceTax();
                return;
            case 1:
                TakeMoney();
                return;
            case 2:
                _dodgeJailCards++;
                Console.WriteLine($"{_name} takes card to avoid jail time!");
                return;
            case 3:
                Console.WriteLine($"{_name} rolled jail time :(");
                GoToJail();
                return;
            case 4:
                Console.WriteLine($"{_name} rolled additional move!");
                Move();
                return;
        }
        
        void TakeMoney()
        {
            var rnd = new Random();
            var money = rnd.Next(100, 250);
            Money += money;
            Console.WriteLine($"{_name} takes ${money} from Bank");
        }
        void TryPayChanceTax()
        {
            var random = new Random();
            var val = random.Next(10, 100);
            Console.WriteLine($"Unlucky {_name} have to pay tax ${val} :(");
            while (Money < val)
            {
                if (_property.Count == 0)
                {
                    // нечего продать и нечем оплатить
                    Money = -1;
                    return;
                }
                var mostExpensive = GetMostExpensiveProperty();
                Money += mostExpensive.Cost;
                mostExpensive.AssignOwner(null);
                _property.Remove(mostExpensive);
                Console.WriteLine($"{_name} sold {mostExpensive.Name} for ${mostExpensive.Cost}");
            }

            Money -= val;
        }
    }
    
    private void GoToJail()
    {
        if (_dodgeJailCards > 0)
        {
            Console.WriteLine($"{_name} has card to dodge jail and uses it!");
            _dodgeJailCards--;
            return;
        }
        _isInJail = true;
        Console.WriteLine($"{_name} goes to jail");
    }
    
    private void TryUpgradeAvenue(Avenue avenue)
    {
        if (!CanUpgradeAvenue(avenue))
            return;
        _property.Remove(avenue);
        _board.Fields[_position] = avenue.Lvl switch
        {
            0 => new Decorator1(avenue),
            1 => new Decorator2(avenue),
            2 => new Decorator3(avenue),
            3 => new DecoratorHotel(avenue),
            4 => avenue,
            _ => throw new ArgumentOutOfRangeException()
        };
        _property.Add((Avenue)_board.Fields[_position]);
        Money -= avenue.Cost;
        Console.WriteLine($"{_name} upgrades {avenue.Name} to Lvl {((Avenue)_board.Fields[_position]).Lvl} for ${avenue.Cost}");
    }

    private void TryGetOutOfJail()
    {
        if (Money >= 100)
        {
            Money -= 100;
            _isInJail = false;
            Console.WriteLine($"{_name} pays $100 to get out of jail");
        }
        else
        {
            if (_die1 == _die2)
            {
                Console.WriteLine($"{_name} rolled double and gets out of jail");
                _position = (_position + _die1 + _die2) % 40;
                ExecuteOrder(_board.Fields[_position]);
                return;
            }
            if (_daysInJail == 3)
            {
                Console.WriteLine($"{_name} stayed in jail for 3 days and now gets out");
                Move();
                _daysInJail = 0;
                return;
            }

            _daysInJail++;
        }
    }
    
    private void RollDice()
    {
        var random = new Random();
        _die1 = random.Next(1, 7);
        _die2 = random.Next(1, 7);
        Console.WriteLine($"{_name} rolled {_die1}-{_die2}");
    }

    private IProperty GetMostExpensiveProperty()
    {
        var max = _property[0];
        foreach (var item in _property)
        {
            if (item.Cost > max.Cost)
                max = item;
        }

        return max;
    }
    
    private bool CanUpgradeAvenue(Avenue avenue)
    {
        var suitableProperty = _property.Where(property => property is Avenue av
                                                           && av.Lvl >= avenue.Lvl && av.Color == avenue.Color).ToArray();
        return suitableProperty.Length == 3 && Money >= avenue.Cost;
    }
}