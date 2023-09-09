using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    public delegate void InputFiledEvent(string str);
    public static InputFiledEvent onSetNoteText;
    [SerializeField] private TMP_InputField inputField;


    public void Configure(string description)
    {
        inputField.text = description;
    }



    public void TriggerSetNoteText()
    {
        if (onSetNoteText != null)
        {
            onSetNoteText(inputField.text);
        }

    }
}
