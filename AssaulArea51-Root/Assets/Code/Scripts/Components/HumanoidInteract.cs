using UnityEngine;

public class HumanoidInteract : MonoBehaviour
{
    private IInteractable _hotspot;

    void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable l_hotspot = other.gameObject.GetComponent<IInteractable>();
        if (l_hotspot != null)
            _hotspot = l_hotspot;

        Debug.Log(other.name);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<IInteractable>() == _hotspot)
        {
            if (_hotspot.ReadInput())
            {
                _hotspot.Interact();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<IInteractable>() == _hotspot)
        {
            _hotspot = null;
        }
    }
}
