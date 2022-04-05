namespace Monopoly;

public class Board
{
    public PublicUtility[]? PublicUtility { get; private set; }
    public Field[] Fields { get; }
    public Jail Jail { get; private set; }

    public Board()
    {
        Fields = new Field[40];
        Initialize();
    }

    private void Initialize()
    {
        var file = File.ReadAllLines(@"D:\c#projects\Monopoly\Monopoly\settings.txt");
        var i = 0;
        foreach (var element in file)
        {
            var splitString = element.Split();
            Fields[i] = splitString[0] switch
            {
                "-a" => new Avenue(splitString[1], splitString[1], GetColor(splitString[2]), 
                    int.Parse(splitString[3]), 0, null),
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

        Jail = (Jail)Fields[30];
    }

    public void StartGame()
    {
        PublicUtility = new[] {(PublicUtility)Fields[12], (PublicUtility)Fields[28]};
        var player1 = new Player("Player1", this);
        var player2 = new Player("Player2", this);
        while (player1.Money >= 0 && player2.Money >= 0)
        {
            player1.Move();
            //Thread.Sleep(6000);
            Console.WriteLine($"{player1.Name} now has ${player1.Money}");
            player2.Move();
            Console.WriteLine($"{player2.Name} now has ${player2.Money}");
            Console.WriteLine("------------------");
            //Thread.Sleep(6000);
        }
        Console.WriteLine(player1.Money < 0 ? $"{player1.Name} lost" : $"{player2.Name} lost");
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