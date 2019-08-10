using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonInteract : MonoBehaviour
{
    [SerializeField] private KeyCode InputKey;
    [SerializeField] private string Output;
    [SerializeField] private string Instruction;

    [SerializeField] private Text TxtInputKey;
    [SerializeField] private Text TxtOutput;
    [SerializeField] private Text TxtInstruction;

    [SerializeField] private Image ImgHoldProgress;

    private void Start()
    {
        TxtInputKey.text = InputKey.ToString();
        TxtOutput.text = Output;
        TxtInstruction.text = Instruction;
    }

    /// <summary>
    /// Changes the fillable bar of the action
    /// </summary>
    /// <param name="value">value from 0 to 1</param>
    public void Progress (float value)
    {
        ImgHoldProgress.fillAmount = value / 1;
    }

    public void AnimationEventAddDot ()
    {
        TxtOutput.text += ".";
    }

    public void AnimationEventRemoveDots()
    {
        TxtOutput.text = Output;
    }
}
