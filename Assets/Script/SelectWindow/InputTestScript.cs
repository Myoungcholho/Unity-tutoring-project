using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StageManager.instance.ShowProgressOnClear(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StageManager.instance.ShowProgressOnClear(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StageManager.instance.ShowProgressOnClear(2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StageManager.instance.ShowProgressOnClear(3);
        }
    }
}
