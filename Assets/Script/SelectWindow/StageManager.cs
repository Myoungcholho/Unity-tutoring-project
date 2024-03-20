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
    public float duration;          //�ʺ� �þ�µ� �ɸ��� �ð�
    public float targetWidth;       //��ǥ�� �ϴ� �ʺ�
    private int stageNum;
    private int levelNum;

    // level �� ���� �Ǹ� ȣ���� �̺�Ʈ �Լ�
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

    // ������Ƽ
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

    // � ������������ TextUI�� ������Ʈ�մϴ�.
    public void StageTextUpdate(string str)
    {
        stageText.text = str;
    }

    // Ŭ���� �� ȣ��Ǹ� ���� �׸��� ����� �����մϴ�.
    public void ShowProgressOnClear(int level)
    {
        if (isStageCleared[level])
            return;
        
        StartCoroutine(ExpandLineOnClear(level));
    }

    // Ŭ���� ������ ���� ���� �׸��� �����մϴ�.
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

    // Ŭ���� ���� �� ��UI �ڵ�
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
        // �ı����� �ʴ� ���� ������Ʈ���� ������ �ְ� �޾ƾ��ϴ� ���̹Ƿ� ���߿� ����
        isStageCleared[level] = true;
    }
}