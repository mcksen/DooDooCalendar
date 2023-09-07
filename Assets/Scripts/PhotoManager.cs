

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhotoManager : MonoBehaviour
{
    public delegate void DeletePhotoEvent(int index);

    public static DeletePhotoEvent onDeletePhotoPressed;

    public delegate void ReturnEvent();
    public static ReturnEvent onReturnPressed;

    [SerializeField] private FullSizePhoto fullSizePhoto;
    [SerializeField] private ListToButtonManager listToButtonManager;
    private List<Cell> photoCells = new();
    private int photoIndex;
    private void Awake()
    {
        ListToButtonManager.onChangeIndex += HandleChangeIndex;
    }


    public void Configure(PhotoCell photoCell, List<Cell> photoCells, int index)
    {
        this.photoCells = photoCells;
        fullSizePhoto.Configure(photoCell);
        listToButtonManager.Configure(photoCells, index);
        photoIndex = index;
    }
    private void OnDestroy()
    {
        ListToButtonManager.onChangeIndex -= HandleChangeIndex;
    }
    private void HandleChangeIndex(int index)
    {
        fullSizePhoto.Configure(photoCells[index] as PhotoCell);
        photoIndex = index;
    }

    public void TriggerDeletePhotoPressed()
    {
        if (onDeletePhotoPressed != null)
        {
            onDeletePhotoPressed(photoIndex);
        }

    }

    public void TriggerReturnPressed()
    {
        if (onDeletePhotoPressed != null)
        {
            onDeletePhotoPressed(photoIndex);
        }

    }

}
