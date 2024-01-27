using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameManager : MonoBehaviour
{
    public GameObject Option2Canvas;

    private void OnEnable()
    {
        InitInputManager.instance.enterPress += EnterCanvars;
    }
    void Start()
    {

    }

    private void Update()
    {
        
    }

    void EnterCanvars()
    {
        Option2Canvas.SetActive(true);
    }

    private void OnDisable()
    {
        InitInputManager.instance.enterPress -= EnterCanvars;
    }
}
