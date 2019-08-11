using UnityEngine;

public class HumanoidInteract : MonoBehaviour
{
    [SerializeField] private HumanoidFuelDispenser _fuelTank;
    private IInteractable _hotspot;
    private FuelTankComponent _carFuelTank;
    private bool _isInDispenser = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable l_hotspot = other.gameObject.GetComponent<IInteractable>();
        if (l_hotspot != null && other.enabled == true)
        {
            _hotspot = l_hotspot;
            if (_hotspot is FuelTankComponent)
                _carFuelTank = other.gameObject.GetComponent<FuelTankComponent>();
            if (_hotspot is FuelDispenser)
            {
                _isInDispenser = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<IInteractable>() == _hotspot && other.enabled == true)
        {
            if (_carFuelTank != null)
            {
                if (_hotspot.ReadInput() && _fuelTank._currentFuel > 0f)
                {
                    _hotspot.Interact();
                    _fuelTank.Draw(_carFuelTank._maxFuelIncrease * Time.deltaTime);
                }
                else
                {
                    _fuelTank.HidePlayerTank();
                }
            }
            if(_isInDispenser)
            {
                if(_hotspot.ReadInput())
                    _fuelTank.Fill();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IInteractable>() == _hotspot)
        {
            _hotspot = null;
            if (_carFuelTank != null)
            {
                _fuelTank.HidePlayerTank();
                _carFuelTank = null;
            }
            if (_isInDispenser)
                _isInDispenser = false;
        }
    }
}
