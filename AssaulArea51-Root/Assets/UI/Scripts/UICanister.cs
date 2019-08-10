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

    /*
    float test;
    private void Update()
    {
        test += Time.deltaTime / 4;
        if (test > 1)
            test = 0;

        Progress(test);
    }
    */

    public void Refill ()
    {
        ArrowsUp.gameObject.SetActive(true);
        ArrowsDown.gameObject.SetActive(false);
    }

    public void Fill ()
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
