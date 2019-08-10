using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform _player;
    [HideInInspector] public bool _isPaused { get; private set;}
    [HideInInspector] public bool _isPlaying { get; private set;}
    [HideInInspector] public bool _isGameOver { get; private set;}

    private static GameManager _instance;
    private EGameState _currentGameState = EGameState.GAME_OVER;  


    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (_player.Equals(null))
            _player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public static GameManager Instance()
    {
        if (!_instance)
            _instance = new GameManager();

        return _instance;
    }

    public void ChangeGameState(EGameState newGameState)
    {
        if (newGameState == _currentGameState)
            return;

        switch(_currentGameState)
        {
            case EGameState.PAUSED:
                _isPaused = false;
                break;
            case EGameState.PLAYING:
                _isPlaying = false;
                break;
            case EGameState.GAME_OVER:
                _isGameOver = false;
                break;
        }

        _currentGameState = newGameState;

        switch (_currentGameState)
        {
            case EGameState.PAUSED:
                _isPaused = true;
                break;
            case EGameState.PLAYING:
                _isPlaying = true;
                break;
            case EGameState.GAME_OVER:
                _isGameOver = true;
                break;
        }
    }
}
