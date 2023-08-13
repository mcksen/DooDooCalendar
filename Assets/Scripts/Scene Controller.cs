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


    public void OnDestroy()
    {
        EventManager.Instance.onOpenGame -= HandleOpenGame;
        EventManager.Instance.onLoadedGame -= HandleLoadedGame;

    }



    // --------------------------------------------------------------------------------------
    // Scene Controller-speciefic functions
    // --------------------------------------------------------------------------------------

    public void HandleOpenGame()
    {
        LoadScene(calendar, true);

    }

    public void HandleLoadedGame()
    {

        scenesToUnload.Add(awake);
        UnloadScenes();

    }


    public void LoadScene(string name, bool isPermanent)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        if (!isPermanent)
        {
            scenesToUnload.Add(name);
        }

    }



    public void UnloadScenes()
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