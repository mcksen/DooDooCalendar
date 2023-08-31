using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCell : Cell
{


    [SerializeField] private Image image;


    private PhotoCellData photoCellData = new PhotoCellData();
    public PhotoCellData PhotoCellData => photoCellData;
    public override void Configure(CellData data)
    {
        photoCellData = data as PhotoCellData;

        byte[] bytes = File.ReadAllBytes(photoCellData.imagePath);

        Vector2 imageSize = image.rectTransform.sizeDelta;
        Texture2D texture = new Texture2D(image.mainTexture.width, image.mainTexture.height);
        texture.LoadImage(bytes);


        image.sprite = Sprite.Create(texture, new Rect(0, 0, imageSize.x, imageSize.y), new Vector2(0.5f, 0.5f));


    }






}
