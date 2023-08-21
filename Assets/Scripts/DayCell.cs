

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





    private DayCellData daycellData = new DayCellData();
    public DayCellData DaycellData => daycellData;

    public override void Configure(CellData data)
    {
        daycellData = data as DayCellData;
        SetTextValue(daycellData.text);
        SetImageColor(daycellData.color);
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

        daycellData.isPoopImageActive = SetImageActiveDependancy(poop);
    }
    public void SetMedicineImage()
    {
        daycellData.isMedicineImageActive = SetImageActiveDependancy(medicine);
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




