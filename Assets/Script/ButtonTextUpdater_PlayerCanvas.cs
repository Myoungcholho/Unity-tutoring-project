using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� 2��,3��,4�� ���� Cavnas
public class ButtonTextUpdater_PlayerCanvas : MonoBehaviour
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
