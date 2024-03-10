using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRe : MonoBehaviour
{
    
    public Action buttonPressed; 
    public Action buttonReleased;

    public GameObject blockingDoor;

    private GameObject mushRoomObject;
    
    private Vector3 buttonStartPosition;
    private Vector3 buttonEndPosition;
    private GameObject pressingObject;

    public bool isPressed = false;
    
    void Start()
    {
        mushRoomObject = transform.GetChild(0).gameObject;
        buttonStartPosition = mushRoomObject.transform.position;
        buttonEndPosition = buttonStartPosition + new Vector3(0, -0.2f, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(pressingObject != collision.gameObject)
            {
                if(!isPressed)
                {

                    pressingObject = collision.gameObject; //���� ��ư�� ���� ������Ʈ ����

                    mushRoomObject.transform.position = buttonEndPosition; //��ư ���� ǥ��
                    
                    isPressed = true;
                   
                    buttonPressed?.Invoke();
                }
                
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MovableWall")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(collision.gameObject == pressingObject) //���� ��ư�� ���� ������Ʈ�� ��ư���� ���� �� 
            {
                mushRoomObject.transform.position = buttonStartPosition;
        
                pressingObject = null;
                isPressed = false;
                
                buttonReleased?.Invoke();
            }
            
        }
    }


}
