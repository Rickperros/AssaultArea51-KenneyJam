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

    private float smoothTime = 0.05F;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        AnchorPosition = AnchorSprite.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, Camera.main.WorldToScreenPoint(AnchorPosition) + Vector3.up * 150, ref velocity, smoothTime);
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
