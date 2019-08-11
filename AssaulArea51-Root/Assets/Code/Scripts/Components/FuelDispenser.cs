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
}
