using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : MonoBehaviour
{
    private const int DAYS_IN_WEEK = 7;

    [SerializeField] private GridPopulator calendarGridPopulator;
    [SerializeField] private GridPopulator weekDaysGridPopulator;



    [SerializeField] private Color blankColor;
    [SerializeField] private Color daysDefaultColor;
    [SerializeField] private Color currentDayColor;

    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private TextMeshProUGUI yearText;



    private DateTime currentDate;
    private DateTime defaultDate;
    private DateTime firstDayOfMonth;

    private int daysInMonth;
    private int numberOfBlanksBefore;
    private int numberOfBlanksAfter;

    private void Start()
    {
        EventManager.instance.onForwardClick += HandleForwardClick;
        EventManager.instance.onBackwardClick += HandleBackwardClick;
        currentDate = DateTime.Today;
        defaultDate = currentDate;
        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");



        numberOfBlanksBefore = GetBlanksBefore();
        numberOfBlanksAfter = GetBlanksAfter();
        PopulateCalendarGrid();
        PopulateWeekDaysGrid();

    }

    private void OnDestroy()
    {
        EventManager.instance.onForwardClick -= HandleForwardClick;
        EventManager.instance.onBackwardClick -= HandleBackwardClick;
    }


    private void HandleForwardClick()
    {
        calendarGridPopulator.ClearCalendarGrid();


        defaultDate = defaultDate.AddMonths(1);
        defaultDate = new DateTime(defaultDate.Year, defaultDate.Month, 1);

        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");
        numberOfBlanksBefore = GetBlanksBefore();
        numberOfBlanksAfter = GetBlanksAfter();

        PopulateCalendarGrid();

    }
    private void HandleBackwardClick()
    {
        calendarGridPopulator.ClearCalendarGrid();
        defaultDate = defaultDate.AddMonths(-1);


        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");
        numberOfBlanksBefore = GetBlanksBefore();
        numberOfBlanksAfter = GetBlanksAfter();
        PopulateCalendarGrid();
    }

    public void PopulateCalendarGrid()
    {

        int totalDays = numberOfBlanksBefore + daysInMonth + numberOfBlanksAfter;
        List<Cell> populationList = calendarGridPopulator.PopulateTheGrid(totalDays);
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
        int dayofWeek = (Int32)firstDayOfMonth.DayOfWeek;
        int number = (dayofWeek + 6) % DAYS_IN_WEEK;

        return number;
    }
    private int GetBlanksAfter()
    {
        daysInMonth = DateTime.DaysInMonth(defaultDate.Year, defaultDate.Month);
        DateTime lastDayOfMonth = new(defaultDate.Year, defaultDate.Month, daysInMonth);
        int dayofWeek = (Int32)lastDayOfMonth.DayOfWeek;
        int number = (DAYS_IN_WEEK - dayofWeek) % DAYS_IN_WEEK;

        return number;
    }


    private void PopulateWeekDaysGrid()
    {

        List<string> weekDays = new List<string> { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" };
        List<Cell> populationList = weekDaysGridPopulator.PopulateTheGrid(weekDays.Count);
        for (int i = 0; i < weekDays.Count; i++)
        {
            populationList[i].SetTextValue(weekDays[i]);
        }
    }
}


