using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcusNeedBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= 2f)
        {
            Debug.Log(this.gameObject.name);
            this.gameObject.SetActive(true);
        } 
        else
        {
            this.gameObject.SetActive(false);
            Debug.Log("SetActiveFalse");
        }
            
    }
}
