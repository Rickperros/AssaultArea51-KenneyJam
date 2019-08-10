using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICarFuel : MonoBehaviour
{
    [SerializeField] private Animation ArrowsUp;
    [SerializeField] private Animation ArrowsDown;

    [SerializeField] private Image FuelProgress;

    [SerializeField] private Color CanisterFull;
    [SerializeField] private Color CanisterEmpty;

    //Change
    [SerializeField] private SpriteRenderer AnchorSprite;

    private Vector3 AnchorPosition;
    private Vector3 desiredX;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    private void Update()
    {
        if (AnchorSprite == null)
            return;

        AnchorPosition = AnchorSprite.transform.position + desiredX;

        transform.position = Vector3.SmoothDamp(transform.position, Camera.main.WorldToScreenPoint(AnchorPosition), ref velocity, smoothTime);
    }

    public void Refill()
    {
        ArrowsUp.gameObject.SetActive(true);
        ArrowsDown.gameObject.SetActive(false);
    }

    public void Fill()
    {
        ArrowsUp.gameObject.SetActive(false);
        ArrowsDown.gameObject.SetActive(true);
    }

    /// <summary>
    /// Changes the fillable bar of the action
    /// </summary>
    /// <param name="value">value from 0 to 1</param>
    public void Progress(float value)
    {
        FuelProgress.color = Color.Lerp(CanisterEmpty, CanisterFull, value);
        FuelProgress.fillAmount = value / 1;
    }

}
