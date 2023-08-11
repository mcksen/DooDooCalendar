using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUPCell : Cell
{
    [SerializeField] private TextMeshProUGUI cellText;




    public override void SetTextValue(string text)
    {
        cellText.text = text;
    }

    public override void SetImageColor(Color color) { }
    public override void Select() { }

}
