
using System.Linq;
using UnityEngine;

public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            }
            return instance;
        }
    }
}