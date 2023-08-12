

using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image selectImage;
    [SerializeField] private Image defaultImage;

    [SerializeField] private Image poop;
    [SerializeField] private Image medicine;


    public override void Select()
    {
        if (!selectImage.enabled)
        {
            selectImage.enabled = true;
            EventManager.Instance.TriggerCellSelect(this);
        }
        else
        {
            selectImage.enabled = false;
            // EventManager.Instance.TriggerCellDESelect(this);
        }
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

