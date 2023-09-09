using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FullSizePhoto : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AspectRatioFitter fitter;





    public void Configure(PhotoCell photoCell)

    {
        byte[] bytes = File.ReadAllBytes(photoCell.PhotoCellData.imagePath);

        Texture2D texture = new Texture2D(Screen.width, Screen.height);

        texture.LoadImage(bytes);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));


        float ratio = (float)image.sprite.texture.width / (float)image.sprite.texture.height;
        fitter.aspectRatio = ratio;

    }
}
