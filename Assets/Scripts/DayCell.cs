

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class DayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image selectImage;
    [SerializeField] private Image defaultImage;

    [SerializeField] private Image poop;
    [SerializeField] private Image medicine;

    [SerializeField] private Color blankColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color currentDayColor;

    private DateTime date;


    private DayCellData daycellData = new DayCellData();
    public DayCellData DaycellData => daycellData;

    public override void Configure(CellData data)
    {
        daycellData = data as DayCellData;
        date = new DateTime(daycellData.year, daycellData.month, daycellData.day);
        SetTextValue();
        SetImageColor();
        if (daycellData.isMedicineImageActive)
        {
            SetMedicineImage();
        }
        if (daycellData.isPoopImageActive)
        {
            SetPoopImage();
        }
    }

    private void Select()
    {
        if (selectImage.enabled == false)
        {

            selectImage.enabled = true;
            EventManager.Instance.TriggerCellImageSelect(this);
        }
        else
        {
            EventManager.Instance.TriggerCellSelect(this);
        }
    }
    public void DeSelect()
    {
        selectImage.enabled = false;

    }
    private void SetTextValue()
    {
        if (date == DateTime.MinValue)
        {
            cellText.text = "";
        }
        else
        {
            cellText.text = date.Day.ToString();
        }
    }
    private void SetImageColor()
    {
        defaultImage.color = defaultColor;

        if (date == DateTime.Today)
        {
            defaultImage.color = currentDayColor;
        }
        else if (date == DateTime.MinValue)
        {
            defaultImage.color = blankColor;
        }

    }

    public void SetPoopImage()
    {
        daycellData.isPoopImageActive = SetImageActiveDependancy(poop);
        TryRemoveCellData();
    }
    public void SetMedicineImage()
    {
        daycellData.isMedicineImageActive = SetImageActiveDependancy(medicine);
        TryRemoveCellData();
    }


    private void TryRemoveCellData()
    {
        if (!daycellData.isPoopImageActive && !daycellData.isMedicineImageActive)
        {
            StateSaver.Data.Remove(DaycellData);
        }
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




