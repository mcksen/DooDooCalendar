using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CalendarUI : MonoBehaviour
{

    private delegate void ButtonClickEvent();
    private ButtonClickEvent onForwardClick;
    private ButtonClickEvent onBackwardClick;


    private const int DAYS_IN_WEEK = 7;
    private readonly List<string> weekDays = new() { "M", "T", "W", "T", "F", "S", "S" };


    [SerializeField] private GridPopulator calendarGridPopulator;
    [SerializeField] private GridPopulator weekDaysGridPopulator;
    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private TextMeshProUGUI yearText;




    private List<Cell> populationList = new();





    private DateTime currentDate;
    private DateTime defaultDate;
    private DateTime firstDayOfMonth;

    private int daysInMonth;
    private int numberOfBlanksBefore;
    private int numberOfBlanksAfter;

    private void Awake()
    {

        onForwardClick += HandleForwardClick;
        onBackwardClick += HandleBackwardClick;



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
        onForwardClick -= HandleForwardClick;
        onBackwardClick -= HandleBackwardClick;


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


    public void TriggerForwardClick()
    {
        if (onForwardClick != null)
        {
            onForwardClick();
        }

    }

    public void TriggerBackwardClick()
    {
        if (onBackwardClick != null)
        {
            onBackwardClick();
        }

    }
}


