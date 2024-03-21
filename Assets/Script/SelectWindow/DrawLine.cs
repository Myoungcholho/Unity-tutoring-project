using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public int lineIndex;
    public float duration;          //너비가 늘어나는데 걸리는 시간
    public float targetWidth;       //목표로 하는 너비
    private RectTransform line;
    private StageManager stageManager;
    private void Awake()
    {
        line = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stageManager = StageManager.instance;
        stageManager.onDrawLine += ShowProgressOnClear;
        DecideDrawClearLine();
    }
    private void DecideDrawClearLine()
    {
        if (stageManager.isStageCleared[lineIndex])
        {
            line.sizeDelta = new Vector2(targetWidth, line.sizeDelta.y);
        }
        else
        {
            line.sizeDelta = new Vector2(0, line.sizeDelta.y);
        }
    }

    private void ShowProgressOnClear(int level)
    {
        if (lineIndex != level)
            return;

        StartCoroutine(ExpandLineOnClear(level));
    }

    private IEnumerator ExpandLineOnClear(int level)
    {
        float elapsedTime = 0f;
        float initialWidth = line.sizeDelta.x;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newWidth = Mathf.Lerp(initialWidth, targetWidth, t);
            line.sizeDelta = new Vector2(newWidth, line.sizeDelta.y);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
        line.sizeDelta = new Vector2(targetWidth, line.sizeDelta.y);
    }
}
