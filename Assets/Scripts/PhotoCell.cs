using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PhotoCell : Cell
{

    [SerializeField] private Image image;

    [SerializeField] private Image defaultImage;

    public void Configure()
    {
        image = defaultImage;
    }
    public void TriggerCameraOn()
    {

        if (image == defaultImage)
        {
            EventManager.Instance.onCameraEnablePressed(transform.name);
        }
    }



}
