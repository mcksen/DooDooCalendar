using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ksen;
public class GridPopulator : MonoBehaviour
{
    [SerializeField] Cell cellPrefab;
    List<Cell> populationList;


    public List<Cell> PopulateTheGrid(int numberOfCells)
    {
        populationList = new();
        for (int i = 0; i <= numberOfCells - 1; i++)
        {

            Cell cell = Instantiate(cellPrefab);
            cell.transform.SetParent(transform);
            populationList.Add(cell);
        }
        return populationList;

    }
    public void ClearCalendarGrid()
    {
        if (populationList.Count > 0)
        {
            foreach (Cell i in populationList)
            {
                Destroy(i.gameObject);
            }
            populationList.Clear();


        }
    }


}
