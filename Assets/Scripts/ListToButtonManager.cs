
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListToButtonManager : MonoBehaviour
{
    public delegate void ButtonEvent();
    public static ButtonEvent onNextPhotoPressed;
    public static ButtonEvent onPreviousPhotoPressed;
    public delegate void ListEvent(int index);
    public static ListEvent onChangeIndex;
    [SerializeField] private Button forward;
    [SerializeField] private Button back;
    List<Cell> list = new();
    private int index;


    private int Index
    {
        get => index;
        set
        {
            index = value;
            SetButtonInteractability();


        }
    }

    private void Awake()
    {
        onNextPhotoPressed += HandleNextPhotoPressed;
        onPreviousPhotoPressed += HandleonPreviousPhotoPressed;
    }

    private void HandleonPreviousPhotoPressed()
    {
        Index -= 1;
        TriggerChangeIndex(Index);
    }

    private void HandleNextPhotoPressed()
    {
        Index += 1;
        TriggerChangeIndex(Index);
    }

    private void OnDestroy()
    {
        onNextPhotoPressed -= HandleNextPhotoPressed;
        onPreviousPhotoPressed += HandleonPreviousPhotoPressed;
    }
    public void Configure(List<Cell> list, int index)
    {
        this.list = list;
        this.Index = index;
        SetButtonInteractability();

    }


    private void SetButtonInteractability()
    {
        if (index == 0)
        {
            back.interactable = false;
        }
        if (index < list.Count && index > 0)
        {
            back.interactable = true;
            forward.interactable = true;
        }
        if (index == list.Count - 1)
        {
            forward.interactable = false;
        }
    }

    public void TriggerNextPhotoPressed()
    {
        if (onNextPhotoPressed != null)
        {
            onNextPhotoPressed();
        }

    }

    public void TriggerPreviousPhotoPressed()
    {
        if (onPreviousPhotoPressed != null)
        {
            onPreviousPhotoPressed();
        }

    }
    public void TriggerChangeIndex(int index)
    {
        if (onChangeIndex != null)
        {
            onChangeIndex(index);
        }

    }
}