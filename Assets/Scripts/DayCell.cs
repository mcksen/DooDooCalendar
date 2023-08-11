

using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DayCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image selectImage;
    [SerializeField] private Image defaultImage;


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
            EventManager.Instance.TriggerCellDESelect(this);
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


}

