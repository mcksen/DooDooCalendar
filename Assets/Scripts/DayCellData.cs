
using UnityEngine;

public class DayCellData : CellData
{
    public string text;
    public Color color;

    public bool isPoopImageActive;
    public bool isMedicineImageActive;


    public DayCellData(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
    public DayCellData()
    {

    }
}
