using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WINDOWscript : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateButtonText(string newText)
    {
        Debug.Log("UpdateButtonText called with text: " + newText);
        if (text != null)
        {
            text.text = newText;
        }
    }
}