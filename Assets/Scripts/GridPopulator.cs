using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridPopulator : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    private List<Cell> populationList = new();


    public List<Cell> Populate(int numberOfCells)
    {

        for (int i = 0; i <= numberOfCells - 1; i++)
        {

            Cell cell = Instantiate(cellPrefab, transform);

            populationList.Add(cell);
        }
        return populationList;

    }
    public void Clear()
    {
        if (populationList != null)
        {
            foreach (Cell i in populationList)
            {
                Destroy(i.gameObject);
            }
            populationList.Clear();


        }
    }


}
