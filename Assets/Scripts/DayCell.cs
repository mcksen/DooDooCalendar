

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
    private DayCellData dData = new DayCellData();
    public DayCellData DData => dData;

    public override void Configure(CellData data)
    {
        dData = data as DayCellData;
        SetTextValue(dData.text);
        SetImageColor(dData.color);
    }

    private void Select()
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
    private void SetTextValue(string text)
    {
        cellText.text = text;
    }
    private void SetImageColor(Color color)
    {
        defaultImage.color = color;
    }

    public void SetPoopImage()
    {

        dData.isPoopImageActive = SetImageActiveDependancy(poop);
    }
    public void SetMedicineImage()
    {
        dData.isMedicineImageActive = SetImageActiveDependancy(medicine);
    }

    private bool SetImageActiveDependancy(Image image)
    {
        if (image.enabled)
        {
            image.enabled = false;
            return image.enabled;

        }
        else
        {
            image.enabled = true;
            return image.enabled;
        }
    }



}




