
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[CreateAssetMenu(fileName = "StateSaver", menuName = "Scriptable Objects/StateSaver")]
public class StateSaver : ScriptableSingleton<StateSaver>
{
    private string json;
    private DataToSave data = new();
    public static List<DayCellData> Data => Instance.data.dataToSave;

    private string Path => System.IO.Path.Combine(Application.persistentDataPath, "savedData.tmp");



    private void ConvertToJSSON()

    {

        json = JsonUtility.ToJson(data);


    }

    private DataToSave ConvertFromJSSON()
    {
        return JsonUtility.FromJson<DataToSave>(json);
    }

    public void Save()
    {
        ConvertToJSSON();
        try
        {
            File.WriteAllText(Path, json);
        }
        catch (Exception ex)
        {
            Debug.LogError("Couldn't save: " + ex);
        }



    }
    public void Load()
    {
        data = new DataToSave();
        try
        {
            if (File.Exists(Path))
            {

                json = File.ReadAllText(Path);
                data = ConvertFromJSSON();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Fail to Load" + ex);
        }

    }


}
