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
    private DateTime firstDayOfMonth;
    private int daysInMonth;
    private int numberOfBlanksBefore;
    private int numberOfBlanksAfter;
    private void Start()
    {

        currentDate = DateTime.Today;

        monthText.text = currentDate.ToString("MMMM");

        yearText.text = currentDate.ToString("yyyy");
        firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        numberOfBlanksBefore = GetBlanksBefore();
        numberOfBlanksAfter = GetBlanksAfter();
        PopulateCalendarGrid();
        PopulateWeekDaysGrid();
    }



    public void PopulateCalendarGrid()
    {

        int totalDays = numberOfBlanksBefore + daysInMonth + numberOfBlanksAfter;
        List<Cell> populationList = calendarGridPopulator.PopulateTheGrid(totalDays);
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

    private int GetBlanksBefore()
    {
        int dayofWeek = (Int32)firstDayOfMonth.DayOfWeek;
        int number = (dayofWeek + 6) % 7;

        return number;
    }
    private int GetBlanksAfter()
    {
        DateTime lastDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, daysInMonth);
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


