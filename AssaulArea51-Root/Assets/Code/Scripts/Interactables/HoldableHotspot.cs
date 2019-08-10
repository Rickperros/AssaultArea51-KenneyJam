using UnityEngine;

public class HoldableHotspot : Hotspot
{
    [SerializeField] protected float _maxInteractionRequiredTime = 5f;
    [Range(0f, 1f)] public float _requiredTimeRatio = 1f;

    protected float _currentRequiredInputTime = 0f;
    protected float _currentInteractionTime = 0f;

    /// <summary>
    /// Set Base call always at the end of overrided method
    /// </summary>
    public override bool ReadInput()
    {
        _currentRequiredInputTime = _maxInteractionRequiredTime * _requiredTimeRatio;

        if (base.ReadInput())
        {
            StartInteraction();
            return true;
        }
        return false;
    }

    public virtual void StartInteraction()
    {

    }

    public override void Interact()
    {
        if(Input.GetAxisRaw(_inputInteract) == 0f)
        {
            EndInteraction();
            _currentInteractionTime = 0f;
            return;
        }

        if(_currentInteractionTime >= _currentRequiredInputTime)
        {
            _currentInteractionTime = 0f;
            EndInteraction();
            return;
        }

        _currentInteractionTime += Time.deltaTime;
    }

    public virtual void EndInteraction()
    {

    }
}
