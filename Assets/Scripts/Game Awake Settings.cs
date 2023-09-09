
using UnityEngine;

public class GameAwakeSettings : MonoBehaviour
{

    private void Start()
    {
        SceneController.Instance.Subscribe();
        EventManager.Instance.TriggerOpenGame();
        StateSaver.Instance.Load();

    }
}