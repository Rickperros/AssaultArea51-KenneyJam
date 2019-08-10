using UnityEngine;

public abstract class Hotspot : MonoBehaviour, IInteractable
{
    [Header("Input")]
    [SerializeField] protected string _inputInteract = "Fill with proper input axis";

    public virtual void Interact()
    {
        Debug.LogError("No Action Defined for HOTSPOT: "+gameObject.name);
    }

    public virtual bool ReadInput()
    {
        return Input.GetAxisRaw(_inputInteract) != 0f;
    }
}
