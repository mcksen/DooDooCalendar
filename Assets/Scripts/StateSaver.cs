using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[CreateAssetMenu(fileName = "StateSaver", menuName = "Scriptable Objects/StateSaver")]
public class StateSaver : ScriptableSingleton<StateSaver>
{
    private string json;
    public DataToSave data = new();
    public static List<DayCellData> Data => Instance.data.dataToSave;

    private string path = "/tmp/savedData.tmp";



    private void ConvertToJSSON()

    {

        json = JsonUtility.ToJson(data);
        Debug.Log("Serialized JSON: " + json);

    }

    private DataToSave ConvertFromJSSON()
    {
        return JsonUtility.FromJson<DataToSave>(json);
    }

    public void Save()
    {
        ConvertToJSSON();
        File.WriteAllText(path, json);
    }
    public void Load()
    {
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = ConvertFromJSSON();
        }
    }


}
