
using UnityEngine;

public class GameExitSettings : MonoBehaviour
{
    private void OnApplicationPause()
    {
        Debug.Log("Saved");
        StateSaver.Instance.Save();
    }
}
