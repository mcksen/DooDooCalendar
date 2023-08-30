
using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DayCellData : CellData, IEquatable<DayCellData>
{


    public bool isPoopImageActive;
    public bool isMedicineImageActive;

    public string description = "";
    public Dictionary<string, string> photoPaths = new Dictionary<string, string>
        {
            { "Photo1", "" },
            { "Photo2", "" },
            { "Photo3", "" }
        };
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
