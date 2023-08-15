using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image image;
    System.Action callback;

    private int clickCount = 0;

    private void Awake()
    {

    }


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
    public void SetButtonTint()
    {

        if (clickCount == 0)
        {
            image.color = Color.gray;
            clickCount++;

        }
        else if (clickCount == 1)
        {
            image.color = Color.white;
            clickCount = 0;
        }

    }

}
