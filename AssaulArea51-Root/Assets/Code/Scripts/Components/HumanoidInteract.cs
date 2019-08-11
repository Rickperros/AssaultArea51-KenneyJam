using UnityEngine;

public class HumanoidInteract : MonoBehaviour
{
    [SerializeField] private HumanoidFuelDispenser _fuelTank;
    private IInteractable _hotspot;
    private FuelTankComponent _carFuelTank;

    void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable l_hotspot = other.gameObject.GetComponent<IInteractable>();
        if (l_hotspot != null)
        {
            _hotspot = l_hotspot;
            if (_hotspot is FuelTankComponent)
                _carFuelTank = other.gameObject.GetComponent<FuelTankComponent>();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<IInteractable>() == _hotspot)
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
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IInteractable>() == _hotspot)
        {
            _hotspot = null;
            _fuelTank.HidePlayerTank();
        }
    }
}
