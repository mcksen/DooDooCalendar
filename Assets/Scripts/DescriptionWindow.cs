using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class DescriptionWindow : MonoBehaviour
{


    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GridPopulator gridPopulator;

    private List<Cell> photoCells = new List<Cell>();
    private List<string> photoPaths = new();

    private void Awake()
    {
        EventManager.Instance.onPhotoAdded += HandlePhotoAdded;
    }
    private void OnDestroy()
    {
        EventManager.Instance.onPhotoAdded -= HandlePhotoAdded;
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
