using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PersistentDataManager : MonoBehaviour
{
    public int playerCount;
    public int clearedStage;
    public int selectStage;

    public static PersistentDataManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PersistentDataManager>();
            }
            return m_instance;
        }
    }
    private static PersistentDataManager m_instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
