using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneController", menuName = "Scriptable Objects/SceneController")]
public class SceneController : ScriptableObject
{
    public static SceneController instance;
    [SerializeField] private string calendar;
    [SerializeField] private string awake;



    List<string> scenesToUnload;

    public void Initialise()
    {
        instance = this;

        scenesToUnload = new List<string>();
        EventManager.instance.onOpenGame += HandleOpenGame;

    }
    private void onDestroy()
    {
        EventManager.instance.onOpenGame -= HandleOpenGame;

    }

    // --------------------------------------------------------------------------------------
    // Event-dependant functions
    // --------------------------------------------------------------------------------------






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
        Debug.Log("pop" + Time.frameCount);
    }

    public void UnloadScenes()
    {
        if (scenesToUnload != null)
        {
            foreach (string i in scenesToUnload)
            {
                SceneManager.UnloadSceneAsync(i);

            }
            scenesToUnload.Clear();
        }
    }

}