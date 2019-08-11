using UnityEngine;

public class FuelDispenser : HoldableHotspot
{
    [Header("References")]
    [SerializeField] private Canvas _gameHUDCanvas;
    [Header("Prefabs")]
    [SerializeField] private GameObject _InteractUI;

    private UIButtonInteract _UIButtonInteract;

    void Awake()
    {
        SpriteRenderer l_renderer = this.GetComponent<SpriteRenderer>();

        _UIButtonInteract = GameObject.Instantiate(_InteractUI, _gameHUDCanvas.transform).GetComponent<UIButtonInteract>();
        _UIButtonInteract.gameObject.SetActive(false);
    }

    public override bool ReadInput()
    {
        _UIButtonInteract.gameObject.SetActive(true);
        return base.ReadInput();
    }

    public override void StartInteraction()
    {
        base.StartInteraction();

        _UIButtonInteract.Act();
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
        Debug.Log("Dispenser out");
        _UIButtonInteract.Release();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _UIButtonInteract.Enter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _UIButtonInteract.Exit();
            _UIButtonInteract.gameObject.SetActive(false);
        }
    }
       
}
