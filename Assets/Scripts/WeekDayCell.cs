

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeekDayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;

    [SerializeField] private Image image;
    WeekCellData wData = new();


    public override void Configure(CellData data)
    {

        wData = data as WeekCellData;
        SetTextValue(wData.text);


    }

    public void SetTextValue(string text)
    {
        cellText.text = text;
    }





}
