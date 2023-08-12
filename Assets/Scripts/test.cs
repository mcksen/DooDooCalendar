using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ButtonListPopulator : MonoBehaviour
// {
//     [SerializeField] private ButtonCell cellPrefab;
//     [SerializeField] private int count = 90;

//     public void Populate()
//     {
//         // for (int i = 0; i < count; i++)
//         // {
//         //     ButtonCell c = Instantiate(cellPrefab, transform);
//         //     c.Configure(Print);
//         // }

//         ButtonCell dogClickButton = Instantiate(cellPrefab);
//         dogClickButton.Configure("Doggy", DogClicked);
//     }

//     // private void Print(ButtonCell cell)
//     {
//         Debug.LogError(cell.index);
//     }
// }

// public class ButtonCell : MonoBehaviour
// {
//     System.Action<ButtonCell> callback;
//     public int index = Random.Range(0, 1000);
//     public void Configure(string name, System.Action<ButtonCell> action)
//     {
//         callback = action;
//     }

//     public void OnClick()
//     {
//         callback?.Invoke(this);
//     }
// }
