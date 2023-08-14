

using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/EventManager")]
public class EventManager : ScriptableSingleton<EventManager>

{


    public delegate void ButtonClickEvent();
    public ButtonClickEvent onForwardClick;
    public ButtonClickEvent onBackwardClick;



    public delegate void CellSelectEvent(Cell cell);
    public CellSelectEvent onCellImageSelect;
    public CellSelectEvent onCellSelect;


    public delegate void SceneEvent();
    public SceneEvent onOpenGame;
    public SceneEvent onLoadedGame;



    public delegate void PopUpEvent();

    public PopUpEvent onDESelectAllCells;
    public PopUpEvent onAddDescriptionPressed;

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
    public void TriggerLoadedGame()
    {
        if (onLoadedGame != null)
        {
            onLoadedGame();
        }

    }
    public void TriggerCellSelect(Cell cell)
    {
        if (onCellSelect != null)
        {
            onCellSelect(cell);
        }

    }

    public void TriggerCellImageSelect(Cell cell)
    {
        if (onCellImageSelect != null)
        {
            onCellImageSelect(cell);
        }

    }
    public void TriggerDESelectAllCells()
    {
        if (onDESelectAllCells != null)
        {
            onDESelectAllCells();
        }

    }

    public void TriggerAddDescriptionPressed()
    {
        if (onAddDescriptionPressed != null)
        {
            onAddDescriptionPressed();
        }

    }

}