namespace Monopoly;

public class Chance : Field
{
    public override void Execute(Player player)
    {
        Console.WriteLine($"{player.Name} takes chance card");
        var random = new Random();
        var value = random.Next(5);
        switch (value)
        {
            case 0:
                TryPayChanceTax();
                return;
            case 1:
                TakeMoney(player);
                return;
            case 2:
                player.DodgeJailCards++;
                Console.WriteLine($"{player.Name} takes card to avoid jail time!");
                return;
            case 3:
                Console.WriteLine($"{player.Name} rolled jail time :(");
                player.Board.Jail.Execute(player);
                return;
            case 4:
                Console.WriteLine($"{player.Name} rolled additional move!");
                player.Move();
                return;
        }
        
        void TakeMoney(Player player)
        {
            var rnd = new Random();
            var money = rnd.Next(100, 250);
            player.Money += money;
            Console.WriteLine($"{player.Name} takes ${money} from Bank");
        }
        void TryPayChanceTax()
        {
            var random = new Random();
            var val = random.Next(10, 100);
            Console.WriteLine($"Unlucky {player.Name} have to pay tax ${val} :(");
            while (player.Money < val)
            {
                if (player.Property.Count == 0)
                {
                    // нечего продать и нечем оплатить
                    player.Money = -1;
                    return;
                }
                var mostExpensive = player.GetMostExpensiveProperty();
                player.Money += mostExpensive.Cost;
                mostExpensive.AssignOwner(null);
                player.Property.Remove(mostExpensive);
                Console.WriteLine($"{player.Name} sold {mostExpensive.Name} for ${mostExpensive.Cost}");
            }

            player.Money -= val;
        }
    }
}