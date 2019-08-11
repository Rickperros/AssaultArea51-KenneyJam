using UnityEngine;

public class HumanoidFuelDispenser : MonoBehaviour
{

    [Header("Tunning")]
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _maxFuelIncrease;
    [SerializeField] [Range(0f,1f)] private float _lowFuelThreshold = 0.33f;

    [Header("References")]
    [SerializeField] private Canvas _gameHUDCanvas;
    [Header("Prefabs")]
    [SerializeField] private GameObject _lowFuelDisclaimer;
    [SerializeField] private GameObject _PlayerCannister;

    public float _currentFuel = 0f;
    private UIFuelMessage _UIFuelMessage;
    private UICanister _UICanister;
    private bool _reposting = false;
    private bool _interacting = false;

    void Awake()
    {
        _currentFuel = _maxFuel;
        SpriteRenderer l_renderer = this.GetComponent<SpriteRenderer>();

        _UICanister = GameObject.Instantiate(_PlayerCannister, _gameHUDCanvas.transform).GetComponent<UICanister>();
        _UICanister.gameObject.SetActive(false);
        _UICanister.AnchorSprite = l_renderer;

        _UIFuelMessage = GameObject.Instantiate(_lowFuelDisclaimer, _gameHUDCanvas.transform).GetComponent<UIFuelMessage>();
        _UIFuelMessage.AnchorSprite = l_renderer;
        _UIFuelMessage.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (_currentFuel <= 0f)
        {
            _UIFuelMessage.gameObject.SetActive(true);
            _UIFuelMessage.OutOfFuel();
        }
        else if (_currentFuel <= _maxFuel * _lowFuelThreshold)
        {
            _UIFuelMessage.gameObject.SetActive(true);
            _UIFuelMessage.FuelLow();
        }
        else
            _UIFuelMessage.gameObject.SetActive(false);
        
    }

    public void Fill()
    {
        _currentFuel = Mathf.Clamp(_currentFuel + _maxFuelIncrease * Time.deltaTime, 0f, _maxFuel);
        _PlayerCannister.SetActive(true);
        _UICanister.Progress(_currentFuel/_maxFuel);
        _reposting = true;

        if (_currentFuel >= _maxFuel)
            _reposting = false;
    }

    public void Draw(float amount)
    {
        _currentFuel = Mathf.Clamp(_currentFuel -amount, 0f, _maxFuel);
        _UICanister.gameObject.SetActive(true);
        _UICanister.Progress(_currentFuel / _maxFuel);
        _interacting = true;
    }

    public void HidePlayerTank()
    {
        _UICanister.gameObject.SetActive(false);
    }
}
