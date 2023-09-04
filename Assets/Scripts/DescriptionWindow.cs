

using System;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;


public class DescriptionWindow : MonoBehaviour
{


    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GridPopulator gridPopulator;
    [SerializeField] private PhotoManager photoManagerPrefab;

    private List<Cell> photoCells = new List<Cell>();
    private List<string> photoPaths = new();
    private PhotoManager photoManager;
    private int index;

    private void Awake()
    {
        EventManager.Instance.onPhotoAdded += HandlePhotoAdded;
        EventManager.Instance.onOpenPhotoPressed += HandlePhotoOpenPressed;
        EventManager.Instance.onPreviousPhotoPressed += HandlePreviousPhotoPressed;
        EventManager.Instance.onReturnPressed += HandleReturnPressed;
        EventManager.Instance.onDeletePhotoPressed += HandleDeletePhotoPressed;

    }
    private void OnDestroy()
    {
        EventManager.Instance.onPhotoAdded -= HandlePhotoAdded;
        EventManager.Instance.onOpenPhotoPressed -= HandlePhotoOpenPressed;
        EventManager.Instance.onNextPhotoPressed -= HandleNextPhotoPressed;
        EventManager.Instance.onPreviousPhotoPressed -= HandlePreviousPhotoPressed;
        EventManager.Instance.onReturnPressed -= HandleReturnPressed;
        EventManager.Instance.onDeletePhotoPressed -= HandleDeletePhotoPressed;
    }

    private void HandleDeletePhotoPressed()
    {
        photoPaths.Remove(photoPaths[index]);
        Destroy(photoManager.gameObject);
        photoManager = null;
        Clear();
        Populate();

    }

    private void HandleReturnPressed()
    {
        Destroy(photoManager.gameObject);
        photoManager = null;
        Clear();
        Populate();
    }

    public void Configure(string description, List<string> photoPaths)
    {
        inputField.text = description;
        this.photoPaths = photoPaths;
        Populate();

    }

    private void HandlePhotoAdded()
    {
        Clear();
        Populate();
    }

    private void HandlePhotoOpenPressed(PhotoCell photoCell)
    {
        photoManager = Instantiate(photoManagerPrefab, transform.parent);
        photoManager.Configure(photoCell);
        for (int i = 0; i < photoCells.Count; i++)
        {
            if (photoCells[i] == photoCell)
            {
                index = i;
            }
        }
    }
    private void HandlePreviousPhotoPressed()
    {
        index -= 1;
        photoManager.Configure(photoCells[index] as PhotoCell);
    }
    private void HandleNextPhotoPressed()
    {
        index += 1;
        photoManager.Configure(photoCells[index] as PhotoCell);
    }
    private void Populate()
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

}
