using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PhotoCell : Cell
{


    [SerializeField] public Image image;

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

            Texture2D texture = Resources.Load<Texture2D>(photoCellData.imagePath);
            Vector2 imageSize = image.rectTransform.sizeDelta;


            image.sprite = Sprite.Create(texture, new Rect(0, 0, imageSize.x, imageSize.y), new Vector2(0.5f, 0.5f));

        }
    }
    public void TriggerCameraOn()
    {

        if (image == defaultImage)
        {
            EventManager.Instance.TriggerCameraEnablePressed(transform.name);
        }
    }





}
