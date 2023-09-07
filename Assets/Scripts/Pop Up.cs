

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;



public class PopUp : MonoBehaviour
{
    public delegate void PopUpEvent();
    public static PopUpEvent quitPopUp;

    [SerializeField] private ButtonCell buttonCellPrefab;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private RectTransform locationDependantArea;
    [SerializeField] private RectTransform tail;

    private const float minPadding = 50f;


    private List<ButtonCell> list = new();








    public void MakeButton(string text, System.Action action, bool isImageActive)
    {
        ButtonCellData buttoncellData = new ButtonCellData();
        buttoncellData.text = text;
        buttoncellData.action = action;
        buttoncellData.isImageActive = isImageActive;


        ButtonCell button = Instantiate(buttonCellPrefab, grid.transform);
        button.Configure(buttoncellData);
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
    public void DestroyButons()
    {
        foreach (ButtonCell button in list)
        {
            Destroy(button.gameObject);

        }
        list.Clear();
    }

    public void TriggerQuitPopUp()
    {
        if (quitPopUp != null)
        {
            quitPopUp();
        }

    }

}
