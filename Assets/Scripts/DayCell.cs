

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class DayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image selectImage;
    [SerializeField] private Image defaultImage;

    [SerializeField] private Image poop;
    [SerializeField] private Image medicine;

    private int clickCount = 0;
    public override void Select()
    {

        if (clickCount == 0)
        {
            selectImage.enabled = true;
            EventManager.Instance.TriggerCellImageSelect(this);
            clickCount++;
        }
        else if (clickCount == 1)
        {
            EventManager.Instance.TriggerCellSelect(this);
        }
    }
    public void DeSelect()
    {
        selectImage.enabled = false;
        clickCount = 0;
    }
    public override void SetTextValue(string text)
    {
        cellText.text = text;
    }
    public override void SetImageColor(Color color)
    {
        defaultImage.color = color;
    }

    public void SetPoopImage()
    {
        SetImageActiveDependancy(poop);
    }
    public void SetMedicineImage()
    {
        SetImageActiveDependancy(medicine);
    }

    private void SetImageActiveDependancy(Image image)
    {
        if (image.enabled)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }



}




