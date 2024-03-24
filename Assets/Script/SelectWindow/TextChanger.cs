using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    private Text stageText;

    private void Awake()
    {
        stageText = GetComponent<Text>();
    }
    public void StageTextUpdate(string str)
    {
        stageText.text = str;
    }
}
