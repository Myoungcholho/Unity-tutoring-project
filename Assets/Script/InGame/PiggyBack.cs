using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiggyBack : MonoBehaviour
{
    public LayerMask objectLayer;

    private GameObject currentUnderPlayer = null;

    public float groundRayDistance = 0.56f; //0.9f 0.64
    public float groundX = -0.28f; //-0.44f -0.32
    public float groundY = -0.5f; //-0.475

    [SerializeField]
    private bool isOn = false;

    private Vector3 lastPosition;

    private RaycastHit2D footRayDetect;
    private GameObject detectedPlayer;
    private Vector3 movedPosition;

    private void Update()
    {
        Ray();
        
    }
    private void FixedUpdate()
    {
        PiggdBack();
    }

    
    private void PiggdBack()
    {
        if (footRayDetect && footRayDetect.collider.gameObject != this.gameObject)
        {
            detectedPlayer = footRayDetect.collider.gameObject;

            if (currentUnderPlayer != detectedPlayer)
            {
                //감지 된 오브젝트가 새로운 오브젝트일 경우 등록
                isOn = true;
                lastPosition = detectedPlayer.transform.position;
                currentUnderPlayer = detectedPlayer;
            }
            else if (isOn && lastPosition != detectedPlayer.transform.position)
            {
                //등록 된 오브젝트일 경우 따라가기 실행
                movedPosition = detectedPlayer.transform.position - lastPosition;
                //transform.position += movedPosition;
                transform.position += new Vector3(movedPosition.x, 0);
                Debug.Log(movedPosition);
                lastPosition = detectedPlayer.transform.position;
            }
        }
        else
        {
            // 감지된 오브젝트가 없을 때 
            isOn = false;
            currentUnderPlayer = null;
        }

    }

    private void Ray()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        Debug.DrawRay(GroundstartPosition + new Vector3(0, -0.1f, 0), Vector2.right * groundRayDistance, Color.blue);
        footRayDetect = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, objectLayer);
    }
}
