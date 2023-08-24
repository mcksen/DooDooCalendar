
using System;
using UnityEngine;
[Serializable]
public class DayCellData : CellData, IEquatable<DayCellData>
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

    public bool Equals(DayCellData other)
    {
        return other.day == day && other.month == month && other.year == year;

    }
}
