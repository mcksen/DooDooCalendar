using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Ksen;
using UnityEngine.UI;

public class WeekDayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;

    [SerializeField] private Image image;



    public override void SetTextValue(string text)
    {
        cellText.text = text;
    }
    public override void SetImageColor(Color color)
    {
        image.color = color;
    }


}
