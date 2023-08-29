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

    public void Configure(string description)
    {
        inputField.text = description;
    }



}
