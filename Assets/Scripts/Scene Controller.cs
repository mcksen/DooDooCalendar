using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneController", menuName = "Scriptable Objects/SceneController")]
public class SceneController : ScriptableObject
{
    public static SceneController instance;

    [SerializeField] private string calendar;
    [SerializeField] private string awake;



    private List<string> scenesToUnload = new();

    public void Initialise()
    {
        instance = this;


        EventManager.instance.onOpenGame += HandleOpenGame;

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