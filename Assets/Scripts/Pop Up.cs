

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


public class PopUp : MonoBehaviour
{

    [SerializeField] ButtonCell buttonCellPrefab;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] Transform locationDependantArea;

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
        locationDependantArea.position = cellPosition;
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
