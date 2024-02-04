using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateButtonText(string newText)
    {
        if (text != null)
        {
            text.text = newText;
        }
    }
}
