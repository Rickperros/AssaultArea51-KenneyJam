using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanister : MonoBehaviour
{

    [SerializeField] private Animation ArrowsUp;
    [SerializeField] private Animation ArrowsDown;

    [SerializeField] private Image FuelProgress;

    [SerializeField] private Color CanisterFull;
    [SerializeField] private Color CanisterEmpty;

    //Change
    public SpriteRenderer AnchorSprite;

    private Vector3 anchorPosition;
    private Vector3 desiredX;
    private bool isUsingCanister;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        anchorTransform = GameManager.Instance()._player;
    }

    private void Update()
    {

        if (Input.GetAxisRaw("Horizontal") > 0)
            desiredX = Vector3.left;
        if (Input.GetAxisRaw("Horizontal") < 0)
            desiredX = Vector3.right;

        if (isUsingCanister)
            desiredX = Vector3.left;

        anchorPosition = anchorTransform.position + desiredX;
        transform.position = Vector3.SmoothDamp(transform.position, Camera.main.WorldToScreenPoint(anchorPosition), ref velocity, smoothTime);
    }

    public void Refill ()
    {
        isUsingCanister = true;
        ArrowsUp.gameObject.SetActive(true);
        ArrowsDown.gameObject.SetActive(false);
    }

    public void Fill ()
    {
        isUsingCanister = true;
        ArrowsUp.gameObject.SetActive(false);
        ArrowsDown.gameObject.SetActive(true);
    }

    public void Stop()
    {
        isUsingCanister = false;
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
