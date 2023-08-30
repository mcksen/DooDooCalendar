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
    [SerializeField] private PhotoCell photoButton;
    [SerializeField] private GridPopulator gridPopulator;
    private List<PhotoCell> photoButtons = new List<PhotoCell>();
    public void Configure(string description)
    {
        inputField.text = description;
        photoButtons = gridPopulator.Populate(numberOfPhotos);

    }



}
