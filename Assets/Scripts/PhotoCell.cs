using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCell : Cell
{


    [SerializeField] private Image image;
    public Image Image => image;
    [SerializeField] private AspectRatioFitter fitter;


    private PhotoCellData photoCellData = new PhotoCellData();
    public PhotoCellData PhotoCellData => photoCellData;
    public override void Configure(CellData data)
    {
        photoCellData = data as PhotoCellData;

        byte[] bytes = File.ReadAllBytes(photoCellData.imagePath);

        Texture2D texture = new Texture2D(Screen.width, Screen.height);

        texture.LoadImage(bytes);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        float ratio = (float)texture.width / (float)texture.height;
        fitter.aspectRatio = ratio;
    }

    public void TriggerPhotoOpen()
    {
        EventManager.Instance.TriggerOpenPhotoPressed(this);
    }





}
