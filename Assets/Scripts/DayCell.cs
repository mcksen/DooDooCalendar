using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image selectImage;
    [SerializeField] private Image defaultImage;
    [SerializeField] private Button button;


    private void Awake()
    {
        EventManager.Instance.onCellSelect += HandleSelectCell;
    }

    private void HandleSelectCell()
    {
        if (!selectImage.enabled)
        {

            selectImage.enabled = true;
        }
        else
        {
            selectImage.enabled = false;
            EventManager.Instance.TriggerCellDESelect();
        }

    }

    public override void SetTextValue(string text)
    {
        cellText.text = text;
    }
    public override void SetImageColor(Color color)
    {
        defaultImage.color = color;
    }


}

