


using System.Collections.Generic;
using UnityEngine;


public class PhotoManager : MonoBehaviour
{
    public delegate void DeletePhotoEvent(int index);
    public static DeletePhotoEvent onDeletePhotoPressed;

    public delegate void ReturnEvent();
    public static ReturnEvent onReturnPressed;

    [SerializeField] private FullSizePhoto fullSizePhoto;
    [SerializeField] private ListTwoButton listTwoButton;
    private List<Cell> photoCells = new();
    private int photoIndex;


    private void Awake()
    {
        ListTwoButton.onChangeIndex += HandleChangeIndex;
    }


    public void Configure(PhotoCell photoCell, List<Cell> photoCells, int index)
    {
        this.photoCells = photoCells;
        fullSizePhoto.Configure(photoCell);
        listTwoButton.Configure(photoCells, index);
        photoIndex = index;
    }
    private void OnDestroy()
    {
        ListTwoButton.onChangeIndex -= HandleChangeIndex;
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
        if (onReturnPressed != null)
        {
            onReturnPressed();
        }

    }

}
