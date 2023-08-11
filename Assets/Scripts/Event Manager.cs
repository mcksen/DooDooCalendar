

using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/EventManager")]
public class EventManager : ScriptableSingleton<EventManager>

{


    public delegate void ButtonClickEvent();
    public ButtonClickEvent onForwardClick;
    public ButtonClickEvent onBackwardClick;



    public delegate void CellSelectEvent(Cell cell);
    public CellSelectEvent onCellSelect;
    public CellSelectEvent onCellDESelect;


    public delegate void SceneEvent();
    public SceneEvent onOpenGame;



    public delegate void PopUpEvent();
    public PopUpEvent onStickerAdded;

    public void TriggerForwardClick()
    {
        if (onForwardClick != null)
        {
            onForwardClick();
        }

    }

    public void TriggerBackwardClick()
    {
        if (onBackwardClick != null)
        {
            onBackwardClick();
        }

    }
    public void TriggerOpenGame()
    {
        if (onOpenGame != null)
        {
            onOpenGame();
        }

    }
    public void TriggerCellSelect(Cell cell)
    {
        if (onCellSelect != null)
        {
            onCellSelect(cell);
        }

    }
    public void TriggerCellDESelect(Cell cell)
    {
        if (onCellDESelect != null)
        {
            onCellDESelect(cell);
        }

    }
    public void TriggerStickerAdded()
    {
        if (onStickerAdded != null)
        {
            onStickerAdded();
        }

    }

}