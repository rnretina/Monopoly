namespace Monopoly;

public class Jail : Field
{
    public override void Execute(Player player)
    {
        if (!player.IsInJail)
            LockPlayer(player);
        else
            TryGetOutOfJail(player);
    }

    private void LockPlayer(Player player)
    {
        if (player.DodgeJailCards > 0)
        {
            Console.WriteLine($"{player.Name} has card to dodge jail and uses it!");
            player.DodgeJailCards--;
            return;
        }
        player.IsInJail = true;
        Console.WriteLine($"{player.Name} goes to jail");
    }
    
    private void TryGetOutOfJail(Player player)
    {
        if (player.Money >= 100)
        {
            player.Money -= 100;
            player.IsInJail = false;
            Console.WriteLine($"{player.Name} pays $100 to get out of jail");
        }
        else
        {
            player.RollDice();
            if (player.Die1 == player.Die2)
            {
                Console.WriteLine($"{player.Name} rolled double and gets out of jail");
                player.Position = (player.Position + player.Die1 + player.Die2) % 40;
                player.Board.Fields[player.Position].Execute(player);
                return;
            }
            if (player.DaysInJail == 3)
            {
                Console.WriteLine($"{player.Name} stayed in jail for 3 days and now gets out");
                player.DaysInJail = 0;
                return;
            }

            player.DaysInJail++;
        }
    }
}