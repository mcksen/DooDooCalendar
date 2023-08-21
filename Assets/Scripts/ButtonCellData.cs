


public class ButtonCellData : CellData
{
    public string text;
    public System.Action action;
    public bool isImageActive;

    public ButtonCellData(string text, System.Action action, bool isImageActive)
    {
        this.text = text;
        this.action = action;
        this.isImageActive = isImageActive;
    }

    public ButtonCellData()
    {

    }
}
