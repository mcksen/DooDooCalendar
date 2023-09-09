
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListTwoButton : MonoBehaviour
{

    public delegate void ListEvent(int index);
    public static ListEvent onChangeIndex;

    [SerializeField] private Button forward;
    [SerializeField] private Button back;
    private List<Cell> list = new();
    private int index;


    private int Index
    {
        get => index;
        set
        {
            index = value;
            SetButtonInteractability();
            TriggerChangeIndex(index);

        }
    }

    public void Configure(List<Cell> list, int index)
    {
        this.list = list;
        this.Index = index;


    }


    private void SetButtonInteractability()
    {
        if (Index == 0)
        {
            back.interactable = false;
        }
        if (Index < list.Count - 1 && Index > 0)
        {
            back.interactable = true;
            forward.interactable = true;
        }
        if (Index == list.Count - 1)
        {
            forward.interactable = false;
        }
    }


    private void TriggerChangeIndex(int index)
    {
        if (onChangeIndex != null)
        {
            onChangeIndex(index);
        }

    }
    public void MoveToPreviousPhoto()
    {
        Index -= 1;

    }

    public void MoveToNextPhoto()
    {
        Index += 1;

    }
}