using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class DescriptionWindow : MonoBehaviour
{

    private const int numberOfPhotos = 3;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GridPopulator gridPopulator;
    private List<Cell> photoCells = new List<Cell>();
    public void Configure(string description, Dictionary<string, string> photoPaths)
    {
        inputField.text = description;
        photoCells = gridPopulator.Populate(numberOfPhotos);
        for (int i = 0; i < photoCells.Count; i++)
        {
            string path = "";
            string name = photoCells[i].name;
            if (photoPaths.ContainsKey(name))
            {
                path = photoPaths[name];
            }
            PhotoCellData data = new PhotoCellData(path);
            photoCells[i].Configure(data);
        }

    }



}
