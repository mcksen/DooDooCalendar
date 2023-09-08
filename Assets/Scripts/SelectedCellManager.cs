using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectedCellManager : MonoBehaviour
{
    public delegate void PhotoPathsListEvent(List<string> photoPaths);
    public static PhotoPathsListEvent onPhotoAdded;
    public static PhotoPathsListEvent onPhotoDeleted;


    private const string poopButtonName = "Poop";
    private const string pillButtonName = "Pill";
    private const string stickerButtonName = "Sticker";
    private const string notesButtonName = "Notes";

    [SerializeField] private PopUp popUpPrefab;
    [SerializeField] private RectTransform canvas;

    [SerializeField] private SelectedCellContentWindow selectedCellContentWindowPrefab;
    private SelectedCellContentWindow selectedCellContentWindow = null;
    private List<string> tempPhotoPaths;
    private DayCell selectedDayCell;
    private PopUp pop = null;

    private string tempNote;

    private void Awake()
    {
        DayCell.onCellSelect += HandleSelectCell;
        PopUp.quitPopUp += HandleQuitPopUp;
        InputField.onSetNoteText += HandleSetNoteText;
        SelectedCellContentWindow.onConfirmChanges += HandleConfirmChanges;
        SelectedCellContentWindow.onCancelChanges += HandleCancelChanges;
        PhotoManager.onDeletePhotoPressed += TryDeletePhoto;
        PhoneCamera.onTryAddPhoto += TryAddPhoto;

    }
    private void OnDestroy()
    {
        DayCell.onCellSelect -= HandleSelectCell;
        PopUp.quitPopUp -= HandleQuitPopUp;
        SelectedCellContentWindow.onCancelChanges -= HandleCancelChanges;
        SelectedCellContentWindow.onConfirmChanges -= HandleConfirmChanges;
        InputField.onSetNoteText -= HandleSetNoteText;
        PhotoManager.onDeletePhotoPressed -= TryDeletePhoto;
        PhoneCamera.onTryAddPhoto -= TryAddPhoto;
    }



    //______________________________________________________________________________________
    //PHOTO EVENTS
    //______________________________________________________________________________________
    private void TryDeletePhoto(int index)
    {
        selectedDayCell.DaycellData.photoPaths.Remove(selectedDayCell.DaycellData.photoPaths[index]);
        TriggerPhotoDeleted(selectedDayCell.DaycellData.photoPaths);
    }

    private void TryAddPhoto(string path)
    {

        selectedDayCell.DaycellData.photoPaths.Add(path);
        TriggerPhotoAdded(selectedDayCell.DaycellData.photoPaths);
    }


    private void TriggerPhotoAdded(List<string> photoPaths)
    {
        if (onPhotoAdded != null)
        {
            onPhotoAdded(photoPaths);
        }
    }

    private void TriggerPhotoDeleted(List<string> photoPaths)
    {
        if (onPhotoDeleted != null)
        {
            onPhotoDeleted(photoPaths);
        }
    }
    //______________________________________________________________________________________
    //SELECTED CELL && POP UP EVENTS 
    //______________________________________________________________________________________
    private void TryAddCellData()
    {
        if (!StateSaver.Data.Contains(selectedDayCell.DaycellData))
        {
            StateSaver.Data.Add(selectedDayCell.DaycellData);

        }


    }


    private void HandleSelectCell(Cell cell)
    {

        if (pop == null)
        {
            selectedDayCell = cell as DayCell;

            pop = Instantiate(popUpPrefab, canvas);
            pop.MakeButton(stickerButtonName, HandleAddStickerPressed, false);
            pop.MakeButton(notesButtonName, HandleAddContentPressed, false);
            pop.SetPosition(cell.transform.position);
        }

    }
    private void HandleQuitPopUp()
    {

        selectedDayCell = null;
        Destroy(pop.gameObject);
        pop = null;
    }

    //______________________________________________________________________________________
    //SELECTED CELL STICKER EVENTS
    //______________________________________________________________________________________
    private void HandleAddStickerPressed()
    {

        pop.DestroyButons();
        pop.MakeButton(poopButtonName, HandleAddPoopPressed, selectedDayCell.DaycellData.isPoopImageActive);
        pop.MakeButton(pillButtonName, HandleAddMedicinePressed, selectedDayCell.DaycellData.isMedicineImageActive);


    }

    private void HandleAddPoopPressed()
    {
        selectedDayCell.SetPoopImage();
        TryAddCellData();
    }

    private void HandleAddMedicinePressed()
    {
        selectedDayCell.SetMedicineImage();
        TryAddCellData();
    }

    //______________________________________________________________________________________
    //      CONTENT && DESCRIPTION EVENTS
    //______________________________________________________________________________________
    private void HandleAddContentPressed()
    {

        if (selectedCellContentWindow == null)
        {
            selectedCellContentWindow = Instantiate(selectedCellContentWindowPrefab, canvas);
            selectedCellContentWindow.Configure(selectedDayCell.DaycellData.description, selectedDayCell.DaycellData.photoPaths);
            tempPhotoPaths = new List<string>(selectedDayCell.DaycellData.photoPaths);
        }


    }
    private void HandleSetNoteText(string str)
    {
        tempNote = str;
    }
    private void HandleCancelChanges()
    {
        tempNote = "";
        selectedDayCell.DaycellData.photoPaths = tempPhotoPaths;
        CloseContentWindow();
    }
    private void HandleConfirmChanges()
    {
        selectedDayCell.DaycellData.description = tempNote;
        TryAddCellData();
        CloseContentWindow();
    }

    private void CloseContentWindow()
    {
        if (selectedCellContentWindow != null)
        {
            Destroy(selectedCellContentWindow.gameObject);
            selectedCellContentWindow = null;
            pop.TriggerQuitPopUp();
        }
    }
}
