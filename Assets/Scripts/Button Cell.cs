using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cellText;
    System.Action callback;




    public void Configure(System.Action action)
    {

        callback = action;
    }
    public void OnClick()
    {
        callback?.Invoke();
    }
    public void SetTextValue(string text)
    {
        cellText.text = text;
    }


}
