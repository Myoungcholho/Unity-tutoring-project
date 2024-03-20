using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[System.Serializable]
public class IsStageCleared
{
    public bool[] temp;
    public event Action<int,bool> OnCleared;
    public bool this[int index]
    {
        get
        {
            return temp[index];
        }
        set
        {
            temp[index] = value;
            OnCleared?.Invoke(index + 1,value);
        }
    }
    public IsStageCleared(int num)
    {
        temp = new bool[num];
    }
}

public class StageManager : MonoBehaviour
{
    private static StageManager m_instance;
    public static StageManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<StageManager>();
            }
            return m_instance;
        }
    }
    public Text stageText;
    public RectTransform[] line;
    public int stageCount;
    public float duration;          //너비가 늘어나는데 걸리는 시간
    public float targetWidth;       //목표로 하는 너비
    private int stageNum;
    private int levelNum;

    // level 이 셋팅 되면 호출할 이벤트 함수
    [SerializeField]
    public IsStageCleared isStageCleared;

    private void Awake()
    {
        isStageCleared = new IsStageCleared(stageCount);
    }

    private void Start()
    {
        DecideDrawClearLine();
        ShowProgressOnClear(0);
    }

    // 프로퍼티
    public int StageNum
    {
        get
        {
            return stageNum;
        }
        set
        {
            stageNum = value;
            Debug.Log("StageNum Setting :" + stageNum);
        }
    }
    public int LevelNum 
    { 
        get
        {
            return levelNum;
        }
        set
        {
            levelNum = value;
            Debug.Log("Level Setting :" + levelNum);
        }
    }

    // 어떤 스테이지인지 TextUI를 업데이트합니다.
    public void StageTextUpdate(string str)
    {
        stageText.text = str;
    }

    // 클리어 시 호출되며 선을 그리는 기능을 실행합니다.
    public void ShowProgressOnClear(int level)
    {
        if (isStageCleared[level])
            return;
        
        StartCoroutine(ExpandLineOnClear(level));
    }

    // 클리어 유무에 따라 선을 그릴지 결정합니다.
    private void DecideDrawClearLine()
    {
        for(int i=0; i< line.Length; i++)
        {
            if (isStageCleared[i])
            {
                line[i].sizeDelta = new Vector2(targetWidth, line[i].sizeDelta.y);
            }
            else
            {
                line[i].sizeDelta = new Vector2(0, line[i].sizeDelta.y);
            }
        }
    }

    // 클리어 성공 시 선UI 코드
    private IEnumerator ExpandLineOnClear(int level)
    {
        float elapsedTime = 0f;
        float initialWidth = line[level].sizeDelta.x;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newWidth = Mathf.Lerp(initialWidth, targetWidth, t);
            line[level].sizeDelta = new Vector2(newWidth, line[level].sizeDelta.y);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
        line[level].sizeDelta = new Vector2(targetWidth, line[level].sizeDelta.y);
        // 파괴되지 않는 게임 오브젝트에서 정보를 주고 받아야하는 것이므로 나중에 수정
        isStageCleared[level] = true;
    }
}