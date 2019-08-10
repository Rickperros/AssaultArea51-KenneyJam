using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonInteract : MonoBehaviour
{
    [Header ("Variables")]
    [Tooltip ("Key to execute aciton")]
    [SerializeField] private KeyCode InputKey;
    [Tooltip ("Output teext of the action")]
    [SerializeField] private string Output;
    [Tooltip ("One click or Hold")]
    [SerializeField] private string Instruction;

    [Header ("References")]
    [SerializeField] private GameObject HolderButton;
    [SerializeField] private Text TxtInputKey;
    [SerializeField] private Text TxtOutput;
    [SerializeField] private Text TxtInstruction;
    [SerializeField] private Image ImgHoldProgress;


    /// <summary>
    /// Enter the HOTSPOT
    /// </summary>
    public void Enter ()
    {
        HolderButton.SetActive(true);
        TxtInstruction.gameObject.SetActive(true);
        Setup();
    }

    /// <summary>
    /// Exit the HOTSPOT
    /// </summary>
    public void Exit ()
    {
        HolderButton.SetActive(false);
        TxtInstruction.gameObject.SetActive(false);
        TxtOutput.gameObject.SetActive(false);
    }

    /// <summary>
    /// Press Input Key
    /// </summary>
    public void Act()
    {
        TxtOutput.gameObject.SetActive(true);
        TxtInstruction.gameObject.SetActive(false);
    }

    private void Setup()
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

    #region Animation Events

    public void AnimationEventAddDot ()
    {
        TxtOutput.text += ".";
    }

    public void AnimationEventRemoveDots()
    {
        TxtOutput.text = Output;
    }

    #endregion
}
