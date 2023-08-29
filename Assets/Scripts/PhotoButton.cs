using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PhotoButton : MonoBehaviour
{

    private Image image;



    public void TriggerCameraOn()
    {
        if (image == null)
        {
            EventManager.Instance.onCameraPressed();
        }
    }
}
