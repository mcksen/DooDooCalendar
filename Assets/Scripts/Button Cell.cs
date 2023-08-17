using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image image;
    System.Action callback;





    public override void Configure(CellData data)
    {
        ButtonCellData bData = data as ButtonCellData;
        callback = bData.action;
        SetTextValue(bData.text);
        if (bData.isImageActive)
        {
            image.color = Color.gray;
        }
        else
        {
            image.color = Color.white;
        }
    }
    public void OnClick()
    {
        callback?.Invoke();
    }
    public void SetButtonTint()
    {

        if (image.color == Color.gray)
        {
            image.color = Color.white;

        }
        else
        {
            image.color = Color.gray;
        }

    }

    public void SetTextValue(string text)
    {
        cellText.text = text;
    }
}
