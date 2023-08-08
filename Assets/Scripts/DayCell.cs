using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ksen;
using TMPro;
using UnityEngine.UI;

public class DayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private GameObject body;
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

