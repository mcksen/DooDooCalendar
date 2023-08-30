using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PhotoCell : Cell
{

    [SerializeField] private Image image;

    [SerializeField] private Image defaultImage;
    private PhotoCellData photoCellData = new PhotoCellData();
    public PhotoCellData PhotoCellData => photoCellData;
    public override void Configure(CellData data)
    {
        photoCellData = data as PhotoCellData;
        if (photoCellData.imagePath == "")
        {
            image = defaultImage;
        }
        else
        {
            image = Image.FromFile(photoCellData.imagePath);
        }
    }
    public void TriggerCameraOn()
    {

        if (image == defaultImage)
        {
            EventManager.Instance.onCameraEnablePressed(transform.name);
        }
    }



}
