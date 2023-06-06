using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOut : MonoBehaviour
{
    public TMP_Text output;

    public void SetText(string s)
    {
        output.text = s;
    }

    public void ResetText()
    {
        output.text = "lost detection";
    }
}
