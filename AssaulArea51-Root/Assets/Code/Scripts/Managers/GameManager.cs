using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Transform _player;
    public GameObject _levelProgressBar;
    public Canvas _gameHUD;
    public int _minCarsToSucces = 3;
    public int _carsInLevel = 0;
    public float _levelDuration = 300f;
    [HideInInspector] public bool _isPaused { get; private set;}
    [HideInInspector] public bool _isPlaying { get; private set;}
    [HideInInspector] public bool _isGameOver { get; private set;}

    private UIProgressBar _UIProgressBar;
    private static GameManager _instance;
    public EGameState _currentGameState = EGameState.GAME_OVER;
    private float _currentplayedTime = 0f;

    private void Awake()
    {
       
        if (_instance == null)
            _instance = this;

        if (_instance != null && _instance != this)
            Destroy(gameObject);


        if (_player == null ||_player.Equals(null))
            _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        _UIProgressBar = GameObject.Instantiate(_levelProgressBar, _gameHUD.transform).GetComponent<UIProgressBar>();
        _UIProgressBar.gameObject.SetActive(true);
        _UIProgressBar.Progress(0);

        ChangeGameState(EGameState.PLAYING);
    }

    void Update()
    {
        if(_isPlaying)
        {
            if (_carsInLevel < _minCarsToSucces)
                Debug.Log("FAILURE!");
            _currentplayedTime += Time.deltaTime;
            if (_currentplayedTime > _levelDuration)
                Debug.Log("Succes!");

            _UIProgressBar.Progress(_currentplayedTime/_levelDuration);
        }
    }
    public void RemoveCar()
    {
        _carsInLevel -= 1;
    }

    public static GameManager Instance()
    {
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
