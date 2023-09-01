using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCell : Cell
{


    [SerializeField] private Image image;
    [SerializeField] private AspectRatioFitter fitter;
    [SerializeField] private RectTransform rectTransform;

    private PhotoCellData photoCellData = new PhotoCellData();
    public PhotoCellData PhotoCellData => photoCellData;
    public override void Configure(CellData data)
    {
        photoCellData = data as PhotoCellData;

        byte[] bytes = File.ReadAllBytes(photoCellData.imagePath);


        Vector3[] photoCellArray = new Vector3[4];
        rectTransform.GetWorldCorners(photoCellArray);
        float width = Mathf.Abs(photoCellArray[3].x - photoCellArray[0].x);
        float height = Mathf.Abs(photoCellArray[1].y - photoCellArray[0].y);
        Texture2D texture = new Texture2D(Screen.width, Screen.height);

        texture.LoadImage(bytes);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        float ratio = (float)width / (float)height;
        fitter.aspectRatio = ratio;
    }






}
