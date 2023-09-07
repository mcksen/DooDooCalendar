


using System;
using System.Collections.Generic;
using UnityEngine;

public class PhotoGallery : MonoBehaviour
{


    [SerializeField] private PhoneCamera phoneCameraPrefab;
    [SerializeField] private GridPopulator gridPopulator;
    [SerializeField] private PhotoManager photoManagerPrefab;

    private PhoneCamera phoneCamera;

    private List<Cell> photoCells = new List<Cell>();
    private PhotoManager photoManager;

    private void Awake()
    {
        PhoneCamera.onTryAddPhoto += HandleTryAddPhoto;
        PhotoCell.onOpenPhotoPressed += HandleOnOpenPhotoPressed;
        SelectedCellManager.onPhotoAdded += HandlePhotoAdded;
        SelectedCellManager.onPhotoDeleted += HandlePhotoDeleted;
        PhotoManager.onReturnPressed += HandleReturnPressed;
    }
    public void Configure(List<string> photoPaths)
    {

        Populate(photoPaths);
    }
    private void OnDestroy()
    {
        PhoneCamera.onTryAddPhoto -= HandleTryAddPhoto;
        PhotoCell.onOpenPhotoPressed -= HandleOnOpenPhotoPressed;
        SelectedCellManager.onPhotoAdded -= HandlePhotoAdded;
        SelectedCellManager.onPhotoDeleted -= HandlePhotoDeleted;
        PhotoManager.onReturnPressed -= HandleReturnPressed;
    }

    private void HandleOnOpenPhotoPressed(PhotoCell photoCell)
    {
        photoManager = Instantiate(photoManagerPrefab, transform.parent);
        int index = 0;
        for (int i = 0; i < photoCells.Count; i++)
        {
            if (photoCells[i] == photoCell)
            {
                index = i;
            }
        }
        photoManager.Configure(photoCell, photoCells, index);
    }

    private void HandleTryAddPhoto(string path)
    {
        CloseCameraWindow();
    }
    private void HandlePhotoAdded(List<string> photoPaths)
    {
        Clear();
        Populate(photoPaths);
    }
    public void EnableCamera()
    {
        phoneCamera = Instantiate(phoneCameraPrefab, transform.parent);

    }

    private void CloseCameraWindow()
    {
        if (phoneCamera != null)
        {

            Destroy(phoneCamera.gameObject);

            phoneCamera = null;

        }
    }
    private void Populate(List<string> photoPaths)
    {
        photoCells = gridPopulator.Populate(photoPaths.Count);
        for (int i = 0; i < photoCells.Count; i++)
        {
            string path = photoPaths[i];

            PhotoCellData data = new PhotoCellData(path);
            photoCells[i].Configure(data);
        }

    }
    private void Clear()
    {
        if (photoCells != null)
        {
            foreach (PhotoCell photoCell in photoCells)
            {
                Destroy(photoCell.gameObject);
            }
            photoCells.Clear();


        }
    }
    private void HandleReturnPressed()
    {
        Destroy(photoManager.gameObject);
        photoManager = null;

    }
    private void HandlePhotoDeleted(List<string> photoPaths)
    {

        Destroy(photoManager.gameObject);
        photoManager = null;
        Clear();
        Populate(photoPaths);

    }
}
