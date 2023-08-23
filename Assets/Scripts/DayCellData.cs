
using UnityEngine;

public class DayCellData : CellData
{


    public bool isPoopImageActive;
    public bool isMedicineImageActive;


    public int day;
    public int month;
    public int year;

    public DayCellData(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
    }
    public DayCellData()
    {

    }
}
