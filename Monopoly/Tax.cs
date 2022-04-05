namespace Monopoly;

public class Tax : Field
{
    public int Cost { get;}
    
    public Tax(string name, int cost)
    {
        Name = name;
        Cost = cost;
    }

    public override void Execute(Player player)
    {
        while (player.Money < Cost)
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

        Console.WriteLine($"{player.Name} pays {Name}");
        player.Money -= Cost;
    }
}