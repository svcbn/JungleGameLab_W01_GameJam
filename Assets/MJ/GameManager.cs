public class GameManager
{
    public Mode Mode { get; private set; }
}

public enum Mode
{
    None,
    Past,
    Current
}