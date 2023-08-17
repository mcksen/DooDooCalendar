

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEditor.PackageManager.UI;
using UnityEditor;
using UnityEngine.AI;

public class PopUp : MonoBehaviour
{

    [SerializeField] ButtonCell buttonCellPrefab;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] RectTransform locationDependantArea;
    [SerializeField] RectTransform tail;


    List<ButtonCell> list = new();








    public void MakeButton(string text, System.Action action, bool isImageActive)
    {
        ButtonCellData bData = new ButtonCellData();
        bData.text = text;
        bData.action = action;
        bData.isImageActive = isImageActive;


        ButtonCell button = Instantiate(buttonCellPrefab, grid.transform);
        button.Configure(bData);
        list.Add(button);



    }


    public void SetPosition(Vector3 cellPosition)
    {
        Vector3[] popupArray = new Vector3[4];
        locationDependantArea.position = cellPosition;
        locationDependantArea.GetWorldCorners(popupArray);
        tail.position = new Vector3(locationDependantArea.position.x, tail.position.y, tail.position.z);

        float minXbound = popupArray[0].x;
        float maxXbound = popupArray[3].x;
        float minPadding = 50f;
        float halfPopWidth = (maxXbound - minXbound) / 2;

        if (minXbound <= 0)
        {
            locationDependantArea.position = new Vector3(halfPopWidth + minPadding, cellPosition.y, cellPosition.z);
            locationDependantArea.GetWorldCorners(popupArray);
        }
        else if (maxXbound >= Screen.width)
        {
            locationDependantArea.position = new Vector3(Screen.width - (halfPopWidth + minPadding), cellPosition.y, cellPosition.z);
            locationDependantArea.GetWorldCorners(popupArray);
        }
        SetTailPosition(cellPosition, popupArray);


    }



    private void SetTailPosition(Vector3 cellPosition, Vector3[] containerArray)
    {

        tail.position = new Vector3(cellPosition.x, tail.position.y, tail.position.z);
        Vector3[] tailArray = new Vector3[4];
        tail.GetWorldCorners(tailArray);
        float halfTailWidth = (tailArray[3].x - tailArray[0].x) / 2;
        if (tailArray[0].x < containerArray[0].x)
        {
            tail.position = new Vector3(cellPosition.x + halfTailWidth, tail.position.y, tail.position.z);
        }
        else if (tailArray[3].x > containerArray[3].x)
        {
            tail.position = new Vector3(cellPosition.x - halfTailWidth, tail.position.y, tail.position.z);
        }

    }
    public void DestroyButonCells()
    {
        foreach (ButtonCell button in list)
        {
            Destroy(button.gameObject);

        }
        list.Clear();
    }



}
