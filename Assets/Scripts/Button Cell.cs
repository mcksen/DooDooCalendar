
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;
    [SerializeField] private Image image;
    private System.Action callback;
    private ButtonCellData bData = new();

    public override void Configure(CellData data)
    {
        bData = data as ButtonCellData;
        callback = bData.action;
        SetTextValue(bData.text);
        if (bData.isImageActive)
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
