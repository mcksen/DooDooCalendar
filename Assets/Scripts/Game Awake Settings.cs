
using UnityEngine;

public class GameAwakeSettings : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;


    private void Awake()
    {



        sceneController.Initialise();

        EventManager.Instance.TriggerOpenGame();

    }
}