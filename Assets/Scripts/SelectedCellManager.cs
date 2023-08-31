using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectedCellManager : MonoBehaviour
{
    private DayCell selectedDayCell;



    private const string poopButtonName = "Poop";
    private const string pillButtonName = "Pill";
    private const string stickerButtonName = "Sticker";
    private const string notesButtonName = "Notes";

    [SerializeField] private PopUp popUpPrefab;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private PhoneCamera phoneCameraPrefab;
    [SerializeField] private DescriptionWindow descriptionWindowPrefab;

    private PopUp pop = null;
    private PhoneCamera phoneCamera;
    private DescriptionWindow descriptionWindow = null;
    private string tempNote;

    private void Awake()
    {
        EventManager.Instance.onCellSelect += HandleSelectCell;
        EventManager.Instance.onDeselectCell += HandleDeselectCell;
        EventManager.Instance.onSetNoteText += HandleSetNoteText;
        EventManager.Instance.onConfirmChanges += HandleConfirmChanges;
        EventManager.Instance.onCancelChanges += HandleCancelChanges;
        EventManager.Instance.onCameraEnablePressed += HandleCameraEnablePressed;
        EventManager.Instance.onTakePhotoPressed += HandleTakePhotoPressed;
    }
    private void OnDestroy()
    {
        EventManager.Instance.onCellSelect -= HandleSelectCell;
        EventManager.Instance.onDeselectCell -= HandleDeselectCell;
        EventManager.Instance.onCancelChanges -= HandleCancelChanges;
        EventManager.Instance.onConfirmChanges -= HandleConfirmChanges;
        EventManager.Instance.onSetNoteText -= HandleSetNoteText;
        EventManager.Instance.onCameraEnablePressed -= HandleCameraEnablePressed;
        EventManager.Instance.onTakePhotoPressed -= HandleTakePhotoPressed;
    }
    private void HandleTakePhotoPressed()
    {
        Guid guid = new System.Guid();
        string path = System.IO.Path.Combine(Application.persistentDataPath + guid.ToString() + ".png");
        selectedDayCell.DaycellData.photoPaths.Add(path);
        Texture2D tex = new Texture2D(phoneCamera.Background.texture.width, phoneCamera.Background.texture.height);
        WebCamTexture webTexture = phoneCamera.Background.texture as WebCamTexture;
        tex.SetPixels(webTexture.GetPixels());
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        EventManager.Instance.TriggerPhotoAdded();
        CloseCameraWindow();


    }
    private void HandleCameraEnablePressed()
    {
        phoneCamera = Instantiate(phoneCameraPrefab, canvas);

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

        }


    }
    private void HandleSetNoteText(string str)
    {
        tempNote = str;
    }
    private void HandleCancelChanges()
    {
        tempNote = "";
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
            HandleDeselectCell();
        }
    }
    private void CloseCameraWindow()
    {
        if (phoneCamera != null)
        {
            phoneCamera.StopCamera();
            Destroy(phoneCamera.gameObject);

            phoneCamera = null;

        }
    }
    private void HandleAddStickerPressed()
    {

        pop.DestroyButons();
        pop.MakeButton(poopButtonName, HandleAddPoopPressed, selectedDayCell.DaycellData.isPoopImageActive);
        pop.MakeButton(pillButtonName, HandleAddMedicinePressed, selectedDayCell.DaycellData.isMedicineImageActive);


    }

    private void HandleDeselectCell()
    {
        selectedDayCell.DeselectImage();
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
