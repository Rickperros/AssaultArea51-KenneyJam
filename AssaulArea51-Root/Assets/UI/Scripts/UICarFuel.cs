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
    private Transform anchorTransform;
    private Vector3 AnchorPosition;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        anchorTransform = GameManager.Instance()._player;
    }

    private void Update()
    {
        AnchorPosition = anchorTransform.transform.position;
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

    public void Stop()
    {
        ArrowsUp.gameObject.SetActive(false);
        ArrowsDown.gameObject.SetActive(false);
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
