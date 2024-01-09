using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OpenScene()
    {
        SceneManager.LoadScene("Stage 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
