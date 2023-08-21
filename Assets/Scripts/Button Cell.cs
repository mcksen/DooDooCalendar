
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image image;
    private System.Action callback;
    private ButtonCellData buttoncellData = new();

    public override void Configure(CellData data)
    {
        buttoncellData = data as ButtonCellData;
        callback = buttoncellData.action;
        SetTextValue(buttoncellData.text);

        if (buttoncellData.isImageActive)
        {
            image.color = Color.gray;
        }
        else
        {
            image.color = Color.white;
        }
    }
    public void OnClick()
    {
        callback?.Invoke();
    }
    public void SetButtonTint()
    {

        if (image.color == Color.gray)
        {
            image.color = Color.white;

        }
        else
        {
            image.color = Color.gray;
        }

    }

    private void SetTextValue(string text)
    {
        cellText.text = text;
    }
}
