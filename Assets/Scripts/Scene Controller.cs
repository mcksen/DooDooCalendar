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
    }


    public void OnDestroy()
    {
        EventManager.Instance.onOpenGame -= HandleOpenGame;
    }




    // --------------------------------------------------------------------------------------
    // Scene Controller-speciefic functions
    // --------------------------------------------------------------------------------------

    public void HandleOpenGame()
    {
        LoadScene(calendar, true);
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

    [ContextMenu("PR")]
    public void PrintOper()
    {
        Debug.LogError(oper.progress);
    }

    static AsyncOperation oper = null;

    public void UnloadScenes()
    {
        if (scenesToUnload != null)
        {
            foreach (string i in scenesToUnload)
            {
                oper = SceneManager.UnloadSceneAsync(i);

            }
            scenesToUnload.Clear();
        }
    }

}