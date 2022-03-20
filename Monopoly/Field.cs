namespace Monopoly;

public class Field
{
    protected string Name { get;  set; }
}

public class Chance : Field
{
    
}

public class Jail : Field
{
    public int JailPosition { get; }

    public Jail()
    {
        JailPosition = 10;
    }
  
}