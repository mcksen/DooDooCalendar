

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class DayCell : Cell
{
    public delegate void CellSelectEvent(Cell cell);
    public static CellSelectEvent onCellImageSelect;
    public static CellSelectEvent onCellSelect;


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
    private void Awake()
    {
        onCellImageSelect += HandleCellImageSelect;
        PopUp.quitPopUp += DeselectImage;
    }
    private void OnDestroy()
    {
        onCellImageSelect -= HandleCellImageSelect;
        PopUp.quitPopUp -= DeselectImage;
    }
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

    public void Select()
    {
        if (selectImage.enabled == false)
        {

            TriggerCellImageSelect(this);
        }
        else
        {
            TriggerCellSelect(this);
        }
    }
    private void HandleCellImageSelect(Cell cell)
    {
        if (cell as DayCell == this)
        {
            selectImage.enabled = true;

        }
        else
        {
            DeselectImage();
        }
    }
    public void DeselectImage()
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
        if (!daycellData.isPoopImageActive && !daycellData.isMedicineImageActive && daycellData.description == "")
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



    private void TriggerCellImageSelect(Cell cell)
    {
        if (onCellImageSelect != null)
        {
            onCellImageSelect(cell);
        }
    }
    private void TriggerCellSelect(Cell cell)
    {
        if (onCellSelect != null)
        {
            onCellSelect(cell);
        }

    }

}




