using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridPopulator : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    private List<Cell> populationList;


    public List<Cell> Populate(int numberOfCells)
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
    public void Clear()
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
