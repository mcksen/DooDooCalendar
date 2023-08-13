

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PopUp : MonoBehaviour
{

    [SerializeField] ButtonCell buttonCellPrefab;
    [SerializeField] GameObject horizontalLayoutGroup;

    List<ButtonCell> list = new();


    public void Configure(List<Tuple<string, System.Action>> buttonList)
    {
        foreach (Tuple<string, System.Action> i in buttonList)
        {
            ButtonCell button = Instantiate(buttonCellPrefab, horizontalLayoutGroup.transform);
            button.SetTextValue(i.Item1);
            button.Configure(i.Item2);
            list.Add(button);
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
