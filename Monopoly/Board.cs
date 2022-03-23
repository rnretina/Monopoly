using System.Runtime.ExceptionServices;

namespace Monopoly;

public class Board
{
    private Field[] _board;
    public int JailPos { get; }
    public PublicUtility[] PublicUtility { get; set; }
    public Field[] Fields => _board;

    public Board()
    {
        _board = new Field[40];
        JailPos = 10;
    }

    private void Initialize()
    {
        var file = File.ReadAllLines(@"D:\c#projects\Monopoly\Monopoly\settings.txt");
        var i = 0;
        foreach (var element in file)
        {
            var splitString = element.Split();
            _board[i] = splitString[0] switch
            {
                "-a" => new Avenue(splitString[1], GetColor(splitString[2]), int.Parse(splitString[3])),
                "-t" => new Tax(splitString[1], int.Parse(splitString[2])),
                "-r" => new Railroad(splitString[1], int.Parse(splitString[2])),
                "-pu" => new PublicUtility(splitString[1], int.Parse(splitString[2])),
                "-f" => new Field(),
                "-j" => new Jail(),
                "-c" => new Chance(),
                _ => throw new InvalidOperationException("unknown key")
            };
            i++;
        }
    }

    public void StartGame()
    {
        Initialize();
        PublicUtility = new[] {(PublicUtility)_board[12], (PublicUtility)_board[28]};
        var firstName = "Player1";
        var secondName = "Player2";
        var player1 = new Player(firstName, this);
        var player2 = new Player(secondName, this);
        while (!(player1.IsBankrupt || player2.IsBankrupt))
        {
            player1.Move();
            //Thread.Sleep(6000);
            Console.WriteLine($"{firstName} now has ${player1.Money}");
            player2.Move();
            Console.WriteLine($"{secondName} now has ${player2.Money}");
            Console.WriteLine("------------------");
            //Thread.Sleep(6000);
        }

        Console.WriteLine(player1.IsBankrupt ? "player 1 lost" : "player 2 lost");
        
    }

    private Avenue.AvenueColor GetColor(string s)
    {
        return s switch
        {
            "Brown" => Avenue.AvenueColor.Brown,
            "Blue" => Avenue.AvenueColor.Blue,
            "Pink" => Avenue.AvenueColor.Pink,
            "Orange" => Avenue.AvenueColor.Orange,
            "Red" => Avenue.AvenueColor.Red,
            "Yellow" => Avenue.AvenueColor.Yellow,
            "Green" => Avenue.AvenueColor.Green,
            "Black" => Avenue.AvenueColor.Black,
            _ => throw new InvalidOperationException("unknown color")
        };
    }
}