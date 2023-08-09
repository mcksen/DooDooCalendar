using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/EventManager")]
public class EventManager : ScriptableSingleton<EventManager>

{


    public delegate void ButtonClickEvent();
    public ButtonClickEvent onForwardClick;
    public ButtonClickEvent onBackwardClick;



    public delegate void CellSelectEvent();
    public CellSelectEvent onCellSelect;
    public CellSelectEvent onCellDESelect;


    public delegate void SceneEvent();
    public SceneEvent onOpenGame;


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
    public void TriggerCellSelect()
    {
        if (onCellSelect != null)
        {
            onCellSelect();
        }

    }
    public void TriggerCellDESelect()
    {
        if (onCellDESelect != null)
        {
            onCellDESelect();
        }

    }

}