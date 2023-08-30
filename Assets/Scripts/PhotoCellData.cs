using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCellData : CellData
{
    public string imagePath;

    public PhotoCellData(string imagePath)
    {
        this.imagePath = imagePath;
    }

    public PhotoCellData() { }

}
