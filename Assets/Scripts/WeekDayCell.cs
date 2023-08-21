

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeekDayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;

    [SerializeField] private Image image;
    private WeekCellData weekcellData = new();


    public override void Configure(CellData data)
    {
        weekcellData = data as WeekCellData;
        SetTextValue(weekcellData.text);
    }

    private void SetTextValue(string text)
    {
        cellText.text = text;
    }





}
