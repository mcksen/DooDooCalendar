using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CalendarUI : MonoBehaviour
{
    private const int DAYS_IN_WEEK = 7;
    private readonly List<string> weekDays = new() { "M", "T", "W", "T", "F", "S", "S" };
    private const string poopButtonName = "Poop";
    private const string pillButtonName = "Pill";
    private const string stickerButtonName = "Sticker";
    private const string notesButtonName = "Notes";

    [SerializeField] private GridPopulator calendarGridPopulator;
    [SerializeField] private GridPopulator weekDaysGridPopulator;
    [SerializeField] private PopUp popUpPrefab;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private PhoneCamera phoneCameraPrefab;
    [SerializeField] private DescriptionWindow descriptionWindowPrefab;


    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private TextMeshProUGUI yearText;


    private PopUp pop = null;
    private PhoneCamera phoneCamera;
    private DescriptionWindow descriptionWindow = null;
    private List<Cell> populationList = new();
    private List<CellData> cellDatas = new();


    private DayCell selectedDayCell;

    private DateTime currentDate;
    private DateTime defaultDate;
    private DateTime firstDayOfMonth;

    private int daysInMonth;
    private int numberOfBlanksBefore;
    private int numberOfBlanksAfter;
    private string tempNote;
    private void Awake()
    {

        EventManager.Instance.onForwardClick += HandleForwardClick;
        EventManager.Instance.onBackwardClick += HandleBackwardClick;
        EventManager.Instance.onCellImageSelect += HandleCellImageSelect;
        EventManager.Instance.onCellSelect += HandleSelectCell;
        EventManager.Instance.onDeselectCell += HandleDeselectCell;
        EventManager.Instance.onSetNoteText += HandleSetNoteText;
        EventManager.Instance.onConfirmChanges += HandleConfirmChanges;
        EventManager.Instance.onCancelChanges += HandleCancelChanges;
        EventManager.Instance.onCameraPressed += HandleCameraPressed;


    }



    private void Start()
    {

        EventManager.Instance.TriggerLoadedGame();
        currentDate = DateTime.Today;
        defaultDate = currentDate;
        SetHeadingText();
        SetNumberOfBlanks();
        PopulateCalendarGrid();
        PopulateWeekDaysGrid();

    }

    private void OnDestroy()
    {
        EventManager.Instance.onForwardClick -= HandleForwardClick;
        EventManager.Instance.onBackwardClick -= HandleBackwardClick;
        EventManager.Instance.onCellSelect -= HandleSelectCell;
        EventManager.Instance.onDeselectCell -= HandleDeselectCell;
        EventManager.Instance.onCellImageSelect -= HandleCellImageSelect;
        EventManager.Instance.onCancelChanges -= HandleCancelChanges;
        EventManager.Instance.onConfirmChanges -= HandleConfirmChanges;
        EventManager.Instance.onSetNoteText -= HandleSetNoteText;
        EventManager.Instance.onCameraPressed += HandleCameraPressed;

    }

    private void HandleCameraPressed()
    {
        phoneCamera = Instantiate(phoneCameraPrefab, canvas);
    }



    // _____________________________________________________________________________________
    //   EVENT - DEPENDANT FUNCTIONS
    // _____________________________________________________________________________________
    private void HandleForwardClick()
    {
        calendarGridPopulator.Clear();


        defaultDate = defaultDate.AddMonths(1);
        defaultDate = new DateTime(defaultDate.Year, defaultDate.Month, 1);

        SetHeadingText();
        SetNumberOfBlanks();
        PopulateCalendarGrid();

    }
    private void HandleBackwardClick()

    {
        calendarGridPopulator.Clear();
        defaultDate = defaultDate.AddMonths(-1);


        SetHeadingText();
        SetNumberOfBlanks();
        PopulateCalendarGrid();
    }
    private void HandleCellImageSelect(Cell cell)
    {
        foreach (DayCell c in populationList)
        {
            if (c != cell)
            {
                c.DeSelect();
            }
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
            descriptionWindow.Configure(selectedDayCell.DaycellData.description);

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
        selectedDayCell.DeSelect();
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
    // _____________________________________________________________________________________
    //   CLASS - SPECIEFIC FUNCTIONS
    // _____________________________________________________________________________________
    private void PopulateCalendarGrid()
    {
        ClearPopulationList();
        int totalDays = numberOfBlanksBefore + daysInMonth + numberOfBlanksAfter;
        populationList = calendarGridPopulator.Populate(totalDays);

        DateTime date = DateTime.MinValue;
        for (int i = 0; i <= populationList.Count - 1; i++)
        {


            if (i >= numberOfBlanksBefore && i < numberOfBlanksBefore + daysInMonth)
            {
                if (date.Date == DateTime.MinValue)
                {
                    date = firstDayOfMonth;
                }
                else
                {
                    date = date.AddDays(1);

                }

            }
            else
            {
                date = DateTime.MinValue;
            }
            DayCellData data = new DayCellData(date.Day, date.Month, date.Year);

            foreach (DayCellData daycellData in StateSaver.Data)
            {
                if (daycellData.Equals(data))
                {
                    data = daycellData;
                }
            }

            populationList[i].Configure(data);

        }


    }

    private int GetBlanksBefore()
    {

        firstDayOfMonth = new DateTime(defaultDate.Year, defaultDate.Month, 1);
        int dayofWeek = (int)firstDayOfMonth.DayOfWeek;
        int number = (dayofWeek + (DAYS_IN_WEEK - 1)) % DAYS_IN_WEEK;

        return number;
    }
    private int GetBlanksAfter()
    {
        daysInMonth = DateTime.DaysInMonth(defaultDate.Year, defaultDate.Month);
        DateTime lastDayOfMonth = new(defaultDate.Year, defaultDate.Month, daysInMonth);
        int dayofWeek = (int)lastDayOfMonth.DayOfWeek;
        int number = (DAYS_IN_WEEK - dayofWeek) % DAYS_IN_WEEK;

        return number;
    }


    private void PopulateWeekDaysGrid()
    {


        List<Cell> populationList = weekDaysGridPopulator.Populate(weekDays.Count);
        for (int i = 0; i < weekDays.Count; i++)
        {
            WeekCellData d = new WeekCellData();
            d.text = weekDays[i];

            populationList[i].Configure(d);
        }
    }


    private void SetHeadingText()
    {
        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");
    }
    private void SetNumberOfBlanks()
    {
        numberOfBlanksBefore = GetBlanksBefore();
        numberOfBlanksAfter = GetBlanksAfter();
    }



    private void ClearPopulationList()
    {

        foreach (Cell c in populationList)
        {
            Destroy(c.gameObject);

        }
        populationList.Clear();

    }
}


