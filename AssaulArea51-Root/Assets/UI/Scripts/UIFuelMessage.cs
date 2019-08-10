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

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        AnchorPosition = AnchorSprite.transform.position + Vector3.down *2 * AnchorSprite.bounds.max.y;
        transform.position = Vector3.SmoothDamp(transform.position, Camera.main.WorldToScreenPoint(AnchorPosition), ref velocity, smoothTime);
    }

    public void FuelLow ()
    {
        Message = "Fuel Low";
        TxtMessage.text = Message;
    }

    public void OutOfFuel()
    {
        Message = "Out of Fuel";
        TxtMessage.text = Message;
    }
}
