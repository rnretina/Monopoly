namespace Monopoly;

public class PublicUtility : Field, IProperty
{
    public Player? Owner { get; private set; }
    public int Cost { get; set; }
    public PublicUtility(string name, int cost)
    {
        Owner = null;
        Name = name;
        Cost = cost;
    }

    public void AssignOwner(Player player)
    {
        Owner = player;
    }

    public override void Execute(Player player)
    {
        if (player.TryBuyProperty(this)) return;
        if (Owner != player && Owner != null)
        {
            var multiplier = (player.Board.PublicUtility[0].Owner == player.Board.PublicUtility[1].Owner) ? 10 : 4;
            var price = multiplier * (player.Die1 + player.Die2);
            while (price > player.Money)
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

            player.Money -= price;
            Owner.Money += price;
            Console.WriteLine($"{player.Name} pays ${price} for visiting {Name}");
        }
    }
}