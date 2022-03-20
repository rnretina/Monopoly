namespace Monopoly;

public class Player
{
    private int _daysInJail;
    private int _position;
    private int _money;
    private string _name;
    private int _die1, _die2;
    public int DoublesInRow { get; private set; }
    public bool IsInJail { get; private set; }
    public int DodgeJailCards { get; set; }
    public bool IsBankrupt => _money < 0;
    public int Position => _position;
    public int Money { get; set; }


    public Player(string name)
    {
        _name = name;
        _money = 1000;
        DoublesInRow = 0;
        IsInJail = false;
        _position = 0;
        DodgeJailCards = 0;
        _daysInJail = 0;
    }
    public void Move()
    {
        if (IsInJail)
        {
            TryGetOutOfJail();
            return;
        }
        var (sum, isDouble) = GetInfoFromRollingDice();
        DoublesInRow = isDouble ? DoublesInRow + 1 : 0;
        if (DoublesInRow == 3)
        {
            GoToJail();
        }
        _position = (_position + sum) % 40;
        if (isDouble) Move();
    }

    public void ExecuteOrder(Field field, Board board)
    {
        switch (field)
        {
            case Avenue avenue:
            {
                TryBuyProperty(avenue);
                if (avenue.GetOwner() != this && avenue.GetOwner() != null)
                {
                    Pay(avenue.GetOwner()!, avenue);
                }
                break;  
            }
            case Tax tax:
                Pay(tax);
                break;
            case Jail:
                if (!IsInJail) GoToJail();
                else
                {
                    Console.WriteLine($"{_name} stays in jail");
                }
                break;
            case PublicUtility publicUtility:
                TryBuyProperty(publicUtility);
                if (publicUtility.GetOwner() != this && publicUtility.GetOwner() != null) 
                {
                    Pay(publicUtility.GetOwner()!, publicUtility);
                }
                break;
            case Railroad railroad:
                TryBuyProperty(railroad);
                if (railroad.GetOwner() != this && railroad.GetOwner() != null) 
                {
                    Pay(railroad.GetOwner()!, railroad);
                }
                break;
            case Chance:
                TakeChanceCard();
                break;
        }
    }

    private void PublicUtilityMove(PublicUtility publicUtility, Board board)
    {
        if (TryBuyProperty(publicUtility)) return;
        // if (publicUtility.GetOwner() != this && publicUtility.GetOwner() != null)
        // {
        //     if (board.PublicUtilityPos)
        // }
        
        
        
        
    }

    private void AvenueMove(Avenue avenue)
    {
        TryBuyProperty(avenue);
        if (avenue.GetOwner() != this && avenue.GetOwner() != null)
        {
            Pay(avenue.GetOwner()!, avenue);
        }
    }
    private void GoToJail()
    {
        IsInJail = true;
        Console.WriteLine($"{_name} goes to jail");
    }

    private void TakeMoney()
    {
        var random = new Random();
        var money = random.Next(100, 250);
        _money += random.Next(100, 250);
        Console.WriteLine($"{_name} takes ${money} from Bank");
    }

    private bool TryBuyProperty(IProperty property)
    {
        if (_money >= property.GetCost() && property.GetOwner() == null)
        {
            property.AssignOwner(this);
            Console.WriteLine($"{_name} buys {property.GetName()}");
            return true;
        }

        return false;
    }

    private void Pay(Player player, IPayable tax)
    {
        _money -= tax.GetCost();
        player.Money += tax.GetCost();
        Console.WriteLine($"{_name} pays Tax {tax.GetName()} ${tax.GetCost()} to {player}");
    }
    
    private void Pay(IPayable tax)
    {
        _money -= tax.GetCost();
        Console.WriteLine($"{_name} pays {tax.GetName()}");
    }

    private void TakeChanceCard()
    {
        Console.WriteLine($"{_name} takes chance card");
        var random = new Random();
        var value = random.Next(100);
    }

    private void TryGetOutOfJail()
    {
        if (_money >= 100)
        {
            _money -= 100;
            IsInJail = false;
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
                Console.WriteLine($"{_name} was in jail 3 days and now gets out");
                Move();
                _daysInJail = 0;
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



}