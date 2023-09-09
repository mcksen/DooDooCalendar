
using System.Collections.Generic;
using UnityEngine;



public class SelectedCellContentWindow : MonoBehaviour
{
    public delegate void DesctiptionWindowEvent();


    public static DesctiptionWindowEvent onConfirmChanges;
    public static DesctiptionWindowEvent onCancelChanges;





    [SerializeField] private InputField inputField;
    [SerializeField] private PhotoGallery photoGallery;




    private PhotoManager photoManager;






    public void Configure(string description, List<string> photoPaths)
    {

        inputField.Configure(description);
        photoGallery.Configure(photoPaths);

    }



    public void TriggerConfirmChanges()
    {
        if (onConfirmChanges != null)
        {
            onConfirmChanges();
        }
    }
    public void TriggerCancelChanges()
    {
        if (onCancelChanges != null)
        {
            onCancelChanges();
        }
    }
}