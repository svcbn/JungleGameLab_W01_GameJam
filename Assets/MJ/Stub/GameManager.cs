using UnityEngine;

public enum GameState
{
    None,
    Past,
    Current
}

public class GameManager : MonoBehaviour
{
    public GameState GameState { get; private set; }

    public void ChangeMode()
    {
        
    }
}

