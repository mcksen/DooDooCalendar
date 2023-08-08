using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/EventManager")]
public class EventManager : ScriptableObject

{
    public static EventManager instance;

    public delegate void ButtonClickEvents();
    public ButtonClickEvents onForwardClick;
    public ButtonClickEvents onBackwardClick;


    public delegate void SceneEvent();
    public SceneEvent onOpenGame;

    public void Initialise()
    {
        instance = this;
    }

    public void HandleForwardClick()
    {
        if (onForwardClick != null)
        {
            onForwardClick();
        }

    }

    public void HandleBackwardClick()
    {
        if (onBackwardClick != null)
        {
            onBackwardClick();
        }

    }
    public void HandleOpenGame()
    {
        if (onOpenGame != null)
        {
            onOpenGame();
        }

    }


}