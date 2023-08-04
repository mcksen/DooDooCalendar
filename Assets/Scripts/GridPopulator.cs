using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPopulator : MonoBehaviour
{
    public static GridPopulator instance;
    void Awake()
    {
        instance = this;
    }


    public List<GameObject> PopulateTheGrid(int numberOfCells, GameObject parent, GameObject objToPopulate)
    {
        List<GameObject> populationList = new List<GameObject>();
        for (int i = 0; i <= numberOfCells - 1; i++)
        {

            GameObject obj = GameObject.Instantiate(objToPopulate);
            obj.transform.SetParent(parent.transform);
            populationList.Add(obj);
        }
        return populationList;

    }



    public void SetTextValue(string text, GameObject obj)
    {

        obj.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
