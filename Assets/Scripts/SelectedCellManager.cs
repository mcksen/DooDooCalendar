using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectedCellManager : MonoBehaviour
{
    public delegate void PhotoPathsListEvent(List<string> photoPaths);
    public static PhotoPathsListEvent onPhotoAdded;
    public static PhotoPathsListEvent onPhotoDeleted;
    private DayCell selectedDayCell;

    private const string poopButtonName = "Poop";
    private const string pillButtonName = "Pill";
    private const string stickerButtonName = "Sticker";
    private const string notesButtonName = "Notes";

    [SerializeField] private PopUp popUpPrefab;
    [SerializeField] private RectTransform canvas;

    [SerializeField] private DescriptionWindow descriptionWindowPrefab;

    private PopUp pop = null;

    private DescriptionWindow descriptionWindow = null;
    private string tempNote;
    private List<string> tempPhotoPaths;

    private void Awake()
    {
        DayCell.onCellSelect += HandleSelectCell;
        PopUp.quitPopUp += HandleQuitPopUp;
        InputField.onSetNoteText += HandleSetNoteText;
        DescriptionWindow.onConfirmChanges += HandleConfirmChanges;
        DescriptionWindow.onCancelChanges += HandleCancelChanges;
        PhotoManager.onDeletePhotoPressed += TryDeletePhoto;
        PhoneCamera.onTryAddPhoto += TryAddPhoto;

    }
    private void OnDestroy()
    {
        DayCell.onCellSelect -= HandleSelectCell;
        PopUp.quitPopUp -= HandleQuitPopUp;
        DescriptionWindow.onCancelChanges -= HandleCancelChanges;
        DescriptionWindow.onConfirmChanges -= HandleConfirmChanges;
        InputField.onSetNoteText -= HandleSetNoteText;
        PhotoManager.onDeletePhotoPressed -= TryDeletePhoto;
        PhoneCamera.onTryAddPhoto -= TryAddPhoto;
    }

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

    private void HandleSelectCell(Cell cell)
    {

        if (pop == null)
        {
            selectedDayCell = cell as DayCell;

            pop = Instantiate(popUpPrefab, canvas);
            pop.MakeButton(stickerButtonName, HandleAddStickerPressed, false);
            pop.MakeButton(notesButtonName, HandleAddDescriptionPressed, false);
            pop.SetPosition(cell.transform.position);
        }
    }
    private void HandleAddDescriptionPressed()
    {

        if (descriptionWindow == null)
        {
            descriptionWindow = Instantiate(descriptionWindowPrefab, canvas);
            descriptionWindow.Configure(selectedDayCell.DaycellData.description, selectedDayCell.DaycellData.photoPaths);
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
        CloseDescriptionWindow();
    }
    private void HandleConfirmChanges()
    {
        selectedDayCell.DaycellData.description = tempNote;
        TryAddCellData();
        CloseDescriptionWindow();
    }

    private void CloseDescriptionWindow()
    {
        if (descriptionWindow != null)
        {
            Destroy(descriptionWindow.gameObject);
            descriptionWindow = null;
            pop.TriggerQuitPopUp();
        }
    }

    private void HandleAddStickerPressed()
    {

        pop.DestroyButons();
        pop.MakeButton(poopButtonName, HandleAddPoopPressed, selectedDayCell.DaycellData.isPoopImageActive);
        pop.MakeButton(pillButtonName, HandleAddMedicinePressed, selectedDayCell.DaycellData.isMedicineImageActive);


    }

    private void HandleQuitPopUp()
    {

        selectedDayCell = null;
        Destroy(pop.gameObject);
        pop = null;
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

    private void TryAddCellData()
    {
        if (!StateSaver.Data.Contains(selectedDayCell.DaycellData))
        {
            StateSaver.Data.Add(selectedDayCell.DaycellData);

        }


    }

}
