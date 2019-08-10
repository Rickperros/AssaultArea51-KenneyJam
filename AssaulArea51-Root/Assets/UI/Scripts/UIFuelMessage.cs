using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFuelMessage : MonoBehaviour
{
    public string Message;

    public Text TxtMessage;
    public SpriteRenderer AnchorSprite;

    private Vector3 AnchorPosition;

    private void Update()
    {

        if (AnchorSprite == null)
            return;

        AnchorPosition = AnchorSprite.transform.position + Vector3.down * AnchorSprite.bounds.max.y;
        transform.position = Camera.main.WorldToScreenPoint(AnchorPosition);

    }

    public void FuelLow ()
    {
        Message = "Fuel Low";
        TxtMessage.text = Message;
    }

    public void OutOfFuel()
    {
        Message = "Out of FUel";
        TxtMessage.text = Message;
    }
}
