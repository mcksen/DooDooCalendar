using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CalendarUI : MonoBehaviour
{
    [SerializeField] private GameObject calendarGrid;
    [SerializeField] private GameObject dayPrefab;
    [SerializeField] private GameObject weekDaysGrid;
    [SerializeField] private GameObject weekDaysPrefab;

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
        PopulateWeekDaysGrid();
        PopulateCalendarGrid();
    }



    public void PopulateCalendarGrid()
    {

        int totalDays = numberOfBlanksBefore + daysInMonth + numberOfBlanksAfter;
        List<GameObject> populationList = GridPopulator.instance.PopulateTheGrid(totalDays, calendarGrid, dayPrefab);
        for (int i = 0; i <= populationList.Count - 1; i++)
        {


            if (i < numberOfBlanksBefore || i >= numberOfBlanksBefore + daysInMonth)
            {
                populationList[i].GetComponent<Image>().color = blankColor;
                GridPopulator.instance.SetTextValue("", populationList[i]);

            }

            else
            {
                GridPopulator.instance.SetTextValue("i.ToString()", populationList[i]);
                if (i == currentDate.Day)
                {
                    populationList[i].GetComponent<Image>().color = currentDayColor;
                }
                else
                {
                    populationList[i].GetComponent<Image>().color = daysDefaultColor;
                }

            }
            populationList.Add(dayPrefab);
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
        int weekLength = 7;
        List<string> weekDays = new List<string> { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" };
        List<GameObject> populationList = GridPopulator.instance.PopulateTheGrid(weekLength, weekDaysGrid, weekDaysPrefab);
        for (int i = 0; i < weekDays.Count; i++)
        {
            GridPopulator.instance.SetTextValue(weekDays[i], populationList[i]);
        }
    }
}


