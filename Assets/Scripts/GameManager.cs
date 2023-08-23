using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        EventManager.Instance.onExitGame += HandleExitGame;
    }
    private void OnDestroy()
    {
        EventManager.Instance.onExitGame -= HandleExitGame;
    }

    private void HandleExitGame()
    {
        StateSaver.Instance.Save();
        Application.Quit();
    }
}
