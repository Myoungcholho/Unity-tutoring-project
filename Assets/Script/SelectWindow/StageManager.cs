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
        temp = new bool[num+1];
    }
}
[System.Serializable]
public class IsStageSelected
{
    public bool[] temp;
    public bool this[int index]
    {
        get
        {
            return temp[index];
        }
        set
        {
            temp[index] = value;
        }
    }
    public IsStageSelected(int num)
    {
        temp = new bool[num+1];
        temp[0] = true;
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
    public int stageCount;          //�������� ���� �ν����� â���� ����
    public int screenMode           //���� ȭ�� 0 : �������� / 1 : ����
    {
        get;
        set;
    }
    public Action<int> onDrawLine;
    private int stageNum;
    private int levelNum;

    // level �� ���� �Ǹ� ȣ���� �̺�Ʈ �Լ�
    [SerializeField]
    public IsStageCleared isStageCleared;
    [SerializeField]
    public IsStageSelected isStageSelected;


    private void Awake()
    {
        isStageCleared = new IsStageCleared(stageCount);
        isStageSelected = new IsStageSelected(stageCount);
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
    // ������ Line���� ���� �׸���� ���� + �ش� �������� clear ó��
    public void ShowProgressOnClear(int level)
    {
        onDrawLine?.Invoke(level);
        // duration �� clearó��
        isStageCleared[level] = true;
        isStageSelected[level + 1] = true;
    }
}