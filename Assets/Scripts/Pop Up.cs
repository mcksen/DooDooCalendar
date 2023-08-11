

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GridPopulator popupGripPopulator;
    [SerializeField] private PopUp pop;
    [SerializeField] List<Cell> allCells;
    private PopUPCell addSticker = new();
    private PopUPCell removeSticker = new();
    private PopUPCell addDescription = new();

    private List<Cell> cellsToPopulate;


    private void Awake()
    {
        EventManager.Instance.onStickerAdded += HandleStickerAdded;
        addSticker.SetTextValue(addSticker.name);
    }


    public void Instantiate(Cell cell)
    {
        pop = Instantiate(pop, cell.transform.position, transform.rotation, cell.transform);
        AddCell(addSticker);
        AddCell(addDescription);
    }

    private void HandleStickerAdded()
    {
        RemoveCell(addSticker);
        AddCell(removeSticker);
    }

    private void OnDestroy()
    {
        EventManager.Instance.onStickerAdded -= HandleStickerAdded;
    }







    private void RemoveCell(Cell cell)
    {
        cellsToPopulate.Remove(cell);
    }
    private void AddCell(Cell cell)
    {
        cellsToPopulate.Add(cell);
    }
}
