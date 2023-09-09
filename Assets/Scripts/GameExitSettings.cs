
using UnityEngine;

public class GameExitSettings : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        StateSaver.Instance.Save();
    }
}
