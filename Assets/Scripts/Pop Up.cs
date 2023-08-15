

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEditor.PackageManager.UI;
using UnityEditor;

public class PopUp : MonoBehaviour
{

    [SerializeField] ButtonCell buttonCellPrefab;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] RectTransform locationDependantArea;


    List<ButtonCell> list = new();





    public void Configure(List<Tuple<string, System.Action>> buttonList)
    {
        foreach (Tuple<string, System.Action> i in buttonList)
        {
            ButtonCell button = Instantiate(buttonCellPrefab, grid.transform);
            button.SetTextValue(i.Item1);
            button.Configure(i.Item2);
            list.Add(button);
        }

    }


    public void SetPosition(Vector3 cellPosition)
    {




        Vector3[] array = new Vector3[4];
        locationDependantArea.transform.position = cellPosition;
        locationDependantArea.GetWorldCorners(array);

        float minXbound = array[0].x;
        float maxXbound = array[3].x;
        float minPadding = 50f;

        float halfPopWidth = (maxXbound - minXbound) / 2;
        if (minXbound <= 0)
        {

            locationDependantArea.transform.position = new Vector3(halfPopWidth + minPadding, cellPosition.y, cellPosition.z);

        }
        else if (maxXbound >= Screen.width)
        {
            locationDependantArea.transform.position = new Vector3(Screen.width - (halfPopWidth + minPadding), cellPosition.y, cellPosition.z);

        }



    }
    public void DestroyObjects()
    {
        foreach (ButtonCell button in list)
        {
            Destroy(button.gameObject);

        }
        list.Clear();
    }



}
