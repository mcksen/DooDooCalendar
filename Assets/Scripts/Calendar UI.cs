using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CalendarUI : MonoBehaviour
{
    private const int DAYS_IN_WEEK = 7;

    [SerializeField] private GridPopulator calendarGridPopulator;
    [SerializeField] private GridPopulator weekDaysGridPopulator;
    [SerializeField] private PopUp popUpPrefab;
    [SerializeField] private RectTransform canvas;




    [SerializeField] private Color blankColor;
    [SerializeField] private Color daysDefaultColor;
    [SerializeField] private Color currentDayColor;



    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private TextMeshProUGUI yearText;


    private PopUp pop = null;
    private List<Tuple<string, System.Action>> popList = new();
    private List<Cell> populationList = new();
    private DayCell selectedCell;

    private DateTime currentDate;
    private DateTime defaultDate;
    private DateTime firstDayOfMonth;

    private int daysInMonth;
    private int numberOfBlanksBefore;
    private int numberOfBlanksAfter;

    private void Awake()
    {
        EventManager.Instance.onForwardClick += HandleForwardClick;
        EventManager.Instance.onBackwardClick += HandleBackwardClick;
        EventManager.Instance.onCellImageSelect += HandleCellImageSelect;
        EventManager.Instance.onCellSelect += HandleSelectCell;
        EventManager.Instance.onDESelectAllCells += HandleDESelectAllCells;


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
        EventManager.Instance.onDESelectAllCells -= HandleDESelectAllCells;
        EventManager.Instance.onCellImageSelect -= HandleCellImageSelect;

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
            popList.Clear();
            selectedCell = cell as DayCell;

            pop = Instantiate(popUpPrefab, canvas);
            popList.Add(new Tuple<string, Action>("Sticker", HandleAddStickerPressed));
            popList.Add(new Tuple<string, Action>("Notes", HandleAddDescriptionPressed));
            pop.SetPosition(cell.transform.position);
            pop.Configure(popList);

        }
    }
    private void HandleAddDescriptionPressed()
    {
        EventManager.Instance.TriggerAddDescriptionPressed();
        //Make function to trigger description scene;
    }


    public void HandleAddStickerPressed()
    {
        if (popList != null)
        {
            pop.DestroyObjects();
            popList.Clear();
        }

        popList.Add(new Tuple<string, Action>("Poop", HandleAddPoopPressed));
        popList.Add(new Tuple<string, Action>("Pill", HandleAddMedicinePressed));
        pop.Configure(popList);

    }

    private void HandleDESelectAllCells()
    {
        selectedCell.DeSelect();
        selectedCell = null;
        Destroy(pop.gameObject);
        pop = null;
    }

    private void HandleAddPoopPressed()
    {
        selectedCell.SetPoopImage();

    }

    private void HandleAddMedicinePressed()
    {
        selectedCell.SetMedicineImage();
    }
    // _____________________________________________________________________________________
    //   CLASS - SPECIEFIC FUNCTIONS
    // _____________________________________________________________________________________
    public void PopulateCalendarGrid()
    {
        ClearPopulationList();
        int totalDays = numberOfBlanksBefore + daysInMonth + numberOfBlanksAfter;
        populationList = calendarGridPopulator.Populate(totalDays);
        int day = 1;
        for (int i = 0; i <= populationList.Count - 1; i++)
        {


            if (i < numberOfBlanksBefore || i >= numberOfBlanksBefore + daysInMonth)
            {
                populationList[i].SetImageColor(blankColor);
                populationList[i].SetTextValue("");

            }

            else
            {

                populationList[i].SetTextValue(day.ToString());
                if (day == currentDate.Day && defaultDate.Month == currentDate.Month && defaultDate.Year == currentDate.Year)
                {
                    populationList[i].SetImageColor(currentDayColor);
                }
                else
                {
                    populationList[i].SetImageColor(daysDefaultColor);
                }
                day++;
            }

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

        List<string> weekDays = new List<string> { "M", "T", "W", "T", "F", "S", "S" };
        List<Cell> populationList = weekDaysGridPopulator.Populate(weekDays.Count);
        for (int i = 0; i < weekDays.Count; i++)
        {
            populationList[i].SetTextValue(weekDays[i]);
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


