

using System;
using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/EventManager")]
public class EventManager : ScriptableSingleton<EventManager>

{

    public delegate void ApplicationEvent();
    public ApplicationEvent onExitGame;

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

    public PopUpEvent onDeselectCell;



    public delegate void DesctiptionWindowEvent();

    public DesctiptionWindowEvent onConfirmChanges;
    public DesctiptionWindowEvent onCancelChanges;


    public delegate void InputFiledEvent(string str);
    public InputFiledEvent onSetNoteText;


    public void TriggerExitGame()
    {
        if (onExitGame != null)
        {
            onExitGame();
        }

    }
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
    public void TriggerDeselectCell()
    {
        if (onDeselectCell != null)
        {
            onDeselectCell();
        }

    }


    public void TriggerConfirmChanges()
    {
        if (onConfirmChanges != null)
        {
            onConfirmChanges();
        }
    }
    public void TriggerCancelChanges()
    {
        if (onCancelChanges != null)
        {
            onCancelChanges();
        }
    }
    public void TriggerSetNoteText(string str)
    {
        if (onSetNoteText != null)
        {
            onSetNoteText(str);
        }

    }
}