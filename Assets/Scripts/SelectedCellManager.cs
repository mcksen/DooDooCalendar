using System.Collections;
using System.Collections.Generic;
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
    private string photoName;
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
        EventManager.Instance.onCameraEnablePressed += HandleCameraEnablePressed;
        EventManager.Instance.onTakePhotoPressed -= HandleTakePhotoPressed;
    }
    private void HandleTakePhotoPressed()
    {
        string path = selectedDayCell.DaycellData.day + selectedDayCell.DaycellData.month + selectedDayCell.DaycellData.year + photoName + ".png";
        Texture2D tex = new Texture2D(phoneCamera.Background.texture.width, phoneCamera.Background.texture.height);
        // tex.SetPixels(WebCamTexture.GetPixels();
        byte[] bytes = tex.EncodeToPNG();

        if (selectedDayCell.DaycellData.photoPaths.ContainsKey(photoName))
        {
            selectedDayCell.DaycellData.photoPaths[photoName] = path;
        }
        Destroy(phoneCamera.gameObject);
        phoneCamera = null;

        photoName = "";
    }
    private void HandleCameraEnablePressed(string name)
    {
        phoneCamera = Instantiate(phoneCameraPrefab, canvas);
        photoName = name;
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
