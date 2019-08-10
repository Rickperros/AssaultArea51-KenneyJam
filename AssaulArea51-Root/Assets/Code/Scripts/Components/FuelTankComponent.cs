using UnityEngine;

public class FuelTankComponent : HoldableHotspot
{
    [Header("Tunning")]
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _maxTimeToRefill;
    [SerializeField] private float _fuelConsumePerSecond;
    [Header("Balancing")]
    [SerializeField] [Range(0f,1f)] private float _lowFuelThreshold = 0.33f;
    [SerializeField] private float _maxFuelIncrease;
    [Header("References")]
    [SerializeField] private CarOscillation _movementController;
    [SerializeField] private Canvas _gameHUDCanvas;
    [SerializeField] private CircleCollider2D _trigger;
    [Header("Prefabs")]
    [SerializeField] private GameObject _lowFuelDisclaimer;
    [SerializeField] private GameObject _fuelTankUI;
    [SerializeField] private GameObject _InteractUI;

    private float _currentFuel = 0f;
    private UIFuelMessage _UIFuelMessage;
    private UICanister _UICanister;
    private UIButtonInteract _UIButtonInteract;
    private bool _reposting = false;
    private bool _interacting = false;


    void Awake()
    {
        _trigger.enabled = false;
        _currentFuel = _maxFuel;
        SpriteRenderer l_renderer = this.GetComponent<SpriteRenderer>();

        if (_movementController == null)
            _movementController = GetComponent<CarOscillation>();

        _UIButtonInteract = GameObject.Instantiate(_InteractUI, _gameHUDCanvas.transform).GetComponent<UIButtonInteract>();
        _UIButtonInteract.gameObject.SetActive(false);

        _UICanister = GameObject.Instantiate(_fuelTankUI, _gameHUDCanvas.transform).GetComponent<UICanister>();
        _UICanister.gameObject.SetActive(false);

        _UIFuelMessage = GameObject.Instantiate(_lowFuelDisclaimer, _gameHUDCanvas.transform).GetComponent<UIFuelMessage>();
        _UIFuelMessage.AnchorSprite = l_renderer;
        _UIFuelMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_currentFuel > 0f && !_reposting)
            _currentFuel -= _fuelConsumePerSecond * Time.deltaTime;

        if (_currentFuel <= 0f)
        {
            _movementController.LeaveScreen();
            _UIFuelMessage.gameObject.SetActive(true);
            _UIButtonInteract.gameObject.SetActive(false);
            _UICanister.gameObject.SetActive(false);
            _UIFuelMessage.OutOfFuel();
            _trigger.enabled = false;
        }
        else if(_currentFuel <= _maxFuel * _lowFuelThreshold)
        {
            _movementController.HoldOnBackGoal();
            _UIFuelMessage.gameObject.SetActive(true);
            _UIButtonInteract.gameObject.SetActive(false);
            _UICanister.gameObject.SetActive(false);
            _UIFuelMessage.FuelLow();
            _trigger.enabled = true;
        }
        else
        {
            if(_interacting && !_reposting)
            EndInteraction();
        }
    }

    public override bool ReadInput()
    {
        _UIFuelMessage.gameObject.SetActive(false);
        _UIButtonInteract.gameObject.SetActive(true);
        _UIButtonInteract.Progress(0f);
        _UICanister.gameObject.SetActive(true);
        _UICanister.Progress(_currentFuel / _maxFuel);
        _requiredTimeRatio = _currentFuel / _maxFuel;
        _currentRequiredInputTime = _requiredTimeRatio * _maxTimeToRefill;
        bool l_readValue = base.ReadInput();
        if(l_readValue)
            _reposting = true;
        else
            _reposting = false;
        return l_readValue;
    }

    public override void StartInteraction()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        _interacting = true;
        _UIButtonInteract.Progress(_currentInteractionTime / _currentRequiredInputTime);
        _currentFuel += _maxFuelIncrease * Time.deltaTime;
        _UICanister.Progress(_currentFuel / _maxFuel);
        if(_currentFuel >= _maxFuel)
        {
            _currentFuel = _maxFuel;
            _reposting = false;
        }
        
    }

    public override void EndInteraction()
    {
        _movementController.SwitchGoals();
        _UIFuelMessage.gameObject.SetActive(false);
        _UIButtonInteract.gameObject.SetActive(false);
        _UICanister.gameObject.SetActive(false);
        _trigger.enabled = false;
        _interacting = false;
    }
}
