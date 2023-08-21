
using UnityEngine;

public class WeekCellData : CellData
{
    public string text;
    public Color color;


    public WeekCellData(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
    public WeekCellData() { }

}
