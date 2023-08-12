using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public abstract class Cell : MonoBehaviour
{

    public abstract void SetTextValue(string text);

    public abstract void SetImageColor(Color color);

    public abstract void Select();


}

