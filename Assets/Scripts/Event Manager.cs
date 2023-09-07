

using System;
using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/EventManager")]
public class EventManager : ScriptableSingleton<EventManager>

{

    public delegate void ApplicationEvent();
    public ApplicationEvent onExitGame;




    public delegate void SceneEvent();
    public SceneEvent onOpenGame;
    public SceneEvent onLoadedGame;





    public void TriggerExitGame()
    {
        if (onExitGame != null)
        {
            onExitGame();
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















}