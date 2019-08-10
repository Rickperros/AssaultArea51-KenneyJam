using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    [SerializeField] private Image ImgProgress;
    [SerializeField] private Transform TCars;

    [SerializeField] private Transform Start;
    [SerializeField] private Transform Goal;

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

    /// <summary>
    /// Changes the fillable bar of the action
    /// </summary>
    /// <param name="value">value from 0 to 1</param>
    public void Progress (float value)
    {
        TCars.transform.position = new Vector3(Mathf.Lerp(Start.transform.position.x + 50, Goal.transform.position.x - 50, value), TCars.transform.position.y);
        ImgProgress.fillAmount = value / 1;
    }
}
