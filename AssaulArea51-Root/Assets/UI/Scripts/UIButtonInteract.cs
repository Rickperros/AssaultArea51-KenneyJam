﻿using System.Collections;
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
    public SpriteRenderer AnchorSprite;


    private Vector3 AnchorPosition;

    private float timer;
    private bool onHolding;

    private void Update()
    {
        if (AnchorSprite == null)
            return;

        AnchorPosition = AnchorSprite.transform.position + Vector3.down * AnchorSprite.bounds.max.y;
        transform.position = Camera.main.WorldToScreenPoint(AnchorPosition);

        if (!onHolding)

        timer += Time.deltaTime / 2;
        if (timer > 1)
            timer = 0;

        Progress(timer);
    }

    /// <summary>
    /// Enter the HOTSPOT
    /// </summary>
    public void Enter ()
    {
        HolderButton.SetActive(true);
        TxtInstruction.gameObject.SetActive(true);
        ImgHoldProgress.gameObject.SetActive(false);
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
        ImgHoldProgress.gameObject.SetActive(true);
        onHolding = true;
    }

    /// <summary>
    /// Release Input Key
    /// </summary>
    public void Release ()
    {
        ImgHoldProgress.gameObject.SetActive(false);
        TxtInstruction.gameObject.SetActive(true);
        TxtOutput.gameObject.SetActive(false);
        onHolding = false;
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
