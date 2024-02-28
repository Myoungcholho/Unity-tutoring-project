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
    
    private void FixedUpdate()
    {
        //PiggdBack();
    }
    private void LateUpdate()
    {
        PiggdBack();
    }
    private void PiggdBack()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        Debug.DrawRay(GroundstartPosition, Vector2.right * groundRayDistance, Color.blue);
        RaycastHit2D footRayDetect = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, objectLayer);
        if (footRayDetect)
        {
            if(footRayDetect.collider.gameObject != this.gameObject)
            {
                
                GameObject detectedPlayer = footRayDetect.collider.gameObject;

                if (footRayDetect)
                {

                    if (currentUnderPlayer != detectedPlayer)
                    {
                        isOn = true;
                        lastPosition = footRayDetect.transform.position;
                        currentUnderPlayer = detectedPlayer;
                    }
                    else if (isOn && lastPosition != footRayDetect.transform.position)
                    {
                        Vector3 movedPosition = footRayDetect.transform.position - lastPosition;

                        transform.position += movedPosition;

                        lastPosition = footRayDetect.transform.position;
                    }
                }
            }
            else
            {
                isOn = false;
                currentUnderPlayer = null;
            }
        }
            
    }
}
