namespace Monopoly;

public class Player
{
    private Board _board;
    private List<IProperty> _property;
    private int _daysInJail;
    private int _position;
    private int _money;
    private readonly string _name;
    private int _die1, _die2;
    private int _doublesInRow;
    private bool _isInJail;
    private int _dodgeJailCards;
    public bool IsBankrupt => _money < 0;
    public int Money => _money;
    
    public Player(string? name, Board board)
    {
        _board = board;
        _property = new List<IProperty>();
        _name = name;
        _money = 1000;
        _doublesInRow = 0;
        _isInJail = false;
        _position = 0;
        _dodgeJailCards = 0;
        _daysInJail = 0;
    }
    public void Move()
    {
        if (_isInJail)
        {
            TryGetOutOfJail();
            return;
        }
        var (sum, isDouble) = GetInfoFromRollingDice();
        _doublesInRow = isDouble ? _doublesInRow + 1 : 0;
        if (_doublesInRow == 3)
        {
            Console.WriteLine($"{_name} rolled double 3 times in a row and goes to jail");
            GoToJail();
        }
        _position = (_position + sum) % 40;
        if (_position + sum >= 40)
        {
            Console.WriteLine($"{_name} crossed Start and gets $200");
            _money += 200;
        }
        if (isDouble)
        {
            ExecuteOrder(_board.Fields[_position]);
            Move();
        }
        ExecuteOrder(_board.Fields[_position]);
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
                {
                    GoToJail();
                }
                else
                {
                    Console.WriteLine($"{_name} stays in jail");
                }
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
        if (publicUtility.GetOwner() != this && publicUtility.GetOwner() != null)
        {
            var multiplier = (_board.PublicUtility[0].GetOwner() == _board.PublicUtility[1].GetOwner()) ? 10 : 4;
            var price = multiplier * (_die1 + _die2);
            while (price > _money)
            {
                if (_property.Count == 0)
                {
                    // нечего продать и нечем оплатить
                    _money = -1;
                    return;
                }
                var mostExpensive = GetMostExpensiveProperty();
                _money += mostExpensive.GetCost();
                mostExpensive.AssignOwner(null);
                _property.Remove(mostExpensive);
                Console.WriteLine($"{_name} sold {mostExpensive.GetName()} for ${mostExpensive.GetCost()}");
            }

            _money -= price;
            publicUtility.GetOwner()._money += price;
            Console.WriteLine($"{_name} pays ${price} for visiting {publicUtility.GetName()}");
        }
    }

    private void PropertyMove(IProperty property)
    {
        if (TryBuyProperty(property)) return;
        if (property.GetOwner() != this && property.GetOwner() != null)
        {
            TryPayProperty(property);
        }
    }

    private bool TryBuyProperty(IProperty property)
    {
        if (_money >= property.GetCost() && property.GetOwner() == null)
        {
            _money -= property.GetCost();
            property.AssignOwner(this);
            _property.Add(property);
            Console.WriteLine($"{_name} buys {property.GetName()} for ${property.GetCost()}");
            return true;
        }

        return false;
    }

    private void TryPayProperty(IProperty property)
    {

        while (property.GetCost() > _money)
        {
            if (_property.Count == 0)
            {
                //нечего продать и нечем платить
                _money = -1;
                return;
            }
            var mostExpensive = GetMostExpensiveProperty();
            _money += mostExpensive.GetCost();
            mostExpensive.AssignOwner(null);
            _property.Remove(mostExpensive);
            Console.WriteLine($"{_name} sold {mostExpensive.GetName()} for ${mostExpensive.GetCost()}");
        }
        _money -= property.GetCost();
        property.GetOwner()._money += property.GetCost();
        Console.WriteLine($"{_name} pays Tax {property.GetName()} ${property.GetCost()} to {property.GetOwner()._name}");
    }
    
    private void TryPay(Tax tax)
    {
        if (_money >= tax.GetCost())
        {
            _money -= tax.GetCost();
            Console.WriteLine($"{_name} pays {tax.GetName()} for ${tax.GetCost()}");
        }
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
            _money += money;
            Console.WriteLine($"{_name} takes ${money} from Bank");
        }
        void TryPayChanceTax()
        {
            var random = new Random();
            var val = random.Next(10, 100);
            Console.WriteLine($"Unlucky {_name} have to pay tax ${val} :(");
            while (_money < val)
            {
                if (_property.Count == 0)
                {
                    // нечего продать и нечем оплатить
                    _money = -1;
                    return;
                }
                var mostExpensive = GetMostExpensiveProperty();
                _money += mostExpensive.GetCost();
                mostExpensive.AssignOwner(null);
                _property.Remove(mostExpensive);
                Console.WriteLine($"{_name} sold {mostExpensive.GetName()} for ${mostExpensive.GetCost()}");
            }

            _money -= val;
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

    private void TryGetOutOfJail()
    {
        if (_money >= 100)
        {
            _money -= 100;
            _isInJail = false;
            Console.WriteLine($"{_name} pays $100 to get out of jail");
        }
        else
        {
            var (sum, isDouble) = GetInfoFromRollingDice();
            if (isDouble)
            {
                Console.WriteLine($"{_name} rolled double and gets out of jail");
                _position = (_position + sum) % 40;
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
    
    private (int sum, bool isDouble) GetInfoFromRollingDice()
    {
        var random = new Random();
        _die1 = random.Next(1, 7);
        _die2 = random.Next(1, 7);
        Console.WriteLine($"{_name} rolled {_die1}-{_die2}");
        return (_die1 + _die2, _die1 == _die2);
    }

    private IProperty GetMostExpensiveProperty()
    {
        var max = _property[0];
        foreach (var item in _property)
        {
            if (item.GetCost() > max.GetCost())
                max = item;
        }

        return max;
    }
}