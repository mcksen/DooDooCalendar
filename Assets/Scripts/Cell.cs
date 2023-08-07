using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Ksen
{
    public abstract class Cell : MonoBehaviour
    {


        public abstract void SetTextValue(string text);


        public abstract void SetImageColor(Color color);


    }
}
