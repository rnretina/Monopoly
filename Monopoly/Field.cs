namespace Monopoly;

public class Field
{
    public string Name { get;  set; }
    public virtual void Execute(Player player) => Console.WriteLine($"free field");
}