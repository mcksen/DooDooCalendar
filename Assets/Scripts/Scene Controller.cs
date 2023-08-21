using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneController", menuName = "Scriptable Objects/SceneController")]
public class SceneController : ScriptableSingleton<SceneController>
{


    [SerializeField] private string calendar;
    [SerializeField] private string awake;



    private List<string> scenesToUnload = new();

    public void Subscribe()
    {
        EventManager.Instance.onOpenGame += HandleOpenGame;
        EventManager.Instance.onLoadedGame += HandleLoadedGame;
    }


    private void OnDestroy()
    {
        EventManager.Instance.onOpenGame -= HandleOpenGame;
        EventManager.Instance.onLoadedGame -= HandleLoadedGame;

    }



    // --------------------------------------------------------------------------------------
    // Scene Controller-speciefic functions
    // --------------------------------------------------------------------------------------

    private void HandleOpenGame()
    {
        LoadScene(calendar, true);

    }

    private void HandleLoadedGame()
    {

        scenesToUnload.Add(awake);
        UnloadScenes();

    }


    private void LoadScene(string name, bool isPermanent)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        if (!isPermanent)
        {
            scenesToUnload.Add(name);
        }

    }



    private void UnloadScenes()
    {
        if (scenesToUnload != null)
        {
            foreach (string sceneName in scenesToUnload)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
            scenesToUnload.Clear();
        }
    }

}