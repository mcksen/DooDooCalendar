using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Ksen;

public class CalendarUI : MonoBehaviour
{

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
    List<Cell> populationList;
    private void Start()
    {
        EventManager.instance.onForwardClick += HandleForwardClick;
        EventManager.instance.onForwardClick += HandleBackwardClick;
        currentDate = DateTime.Today;
        defaultDate = currentDate;
        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");

        numberOfBlanksBefore = GetBlanksBefore(defaultDate);
        numberOfBlanksAfter = GetBlanksAfter(defaultDate);
        PopulateCalendarGrid();
        PopulateWeekDaysGrid();

    }


    private void HandleForwardClick()
    {
        ClearCalendarGrid();
        defaultDate.AddMonths(1);
        defaultDate = new DateTime(defaultDate.Year, defaultDate.Month, 1);
        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");
        PopulateCalendarGrid();

    }
    private void HandleBackwardClick()
    {
        ClearCalendarGrid();
        defaultDate.AddMonths(-1);
        defaultDate = new DateTime(defaultDate.Year, defaultDate.Month, 1);
        monthText.text = defaultDate.ToString("MMMM");
        yearText.text = defaultDate.ToString("yyyy");
        PopulateCalendarGrid();
    }

    public void PopulateCalendarGrid()
    {

        int totalDays = numberOfBlanksBefore + daysInMonth + numberOfBlanksAfter;
        populationList = calendarGridPopulator.PopulateTheGrid(totalDays);
        for (int i = 0; i <= populationList.Count - 1; i++)
        {


            if (i < numberOfBlanksBefore || i >= numberOfBlanksBefore + daysInMonth)
            {
                populationList[i].SetImageColor(blankColor);
                populationList[i].SetTextValue("");

            }

            else
            {
                populationList[i].SetTextValue(i.ToString());
                if (i == currentDate.Day)
                {
                    populationList[i].SetImageColor(currentDayColor);
                }
                else
                {
                    populationList[i].SetImageColor(daysDefaultColor);
                }

            }

        }


    }

    private void ClearCalendarGrid()
    {
        if (populationList.Count > 0)
        {
            populationList.Clear();
        }
    }

    private int GetBlanksBefore(DateTime defaultDate)
    {

        firstDayOfMonth = new DateTime(defaultDate.Year, defaultDate.Month, 1);
        int dayofWeek = (Int32)firstDayOfMonth.DayOfWeek;
        int number = (dayofWeek + 6) % 7;

        return number;
    }
    private int GetBlanksAfter(DateTime defaultDate)
    {
        daysInMonth = DateTime.DaysInMonth(defaultDate.Year, defaultDate.Month);
        DateTime lastDayOfMonth = new(defaultDate.Year, defaultDate.Month, daysInMonth);
        int dayofWeek = (Int32)lastDayOfMonth.DayOfWeek;
        int number = (7 - dayofWeek) % 7;

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


