using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowController : MonoBehaviour
{
    /*
    public Text StudentName;
    public Text Height;
    public Text Weight;
    public Text DateOfBirth;
    */

    public Text Name;
    public Text Date;
    public Text Score;

    public void SetAllFields(string name, string date, string score)
    {
        Name.text = name;
        Date.text = date;
        Score.text = score;
    }
}
