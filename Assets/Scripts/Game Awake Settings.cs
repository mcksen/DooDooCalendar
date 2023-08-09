using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAwakeSettings : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private EventManager eventManager;

    private void Awake()
    {


        eventManager.Initialise();
        sceneController.Initialise();

        EventManager.instance.TriggerOpenGame();

    }
}