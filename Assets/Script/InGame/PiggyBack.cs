using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiggyBack : MonoBehaviour
{
    public LayerMask objectLayer;

    private GameObject currentUnderPlayer = null;

    private float groundRayDistance = 0.9f;
    private float groundX = -0.44f;
    public float groundY = -0.475f;
    [SerializeField]
    private bool isOn = false;

    private Vector3 lastPosition;
    
    private void FixedUpdate()
    {
        PiggdBack();
    }

    private void PiggdBack()
    {
        Vector3 GroundstartPosition = transform.position + new Vector3(groundX, groundY, 0);

        Debug.DrawRay(GroundstartPosition, Vector2.right * groundRayDistance, Color.red);
        RaycastHit2D footRayDetect = Physics2D.Raycast(GroundstartPosition, Vector2.right, groundRayDistance, objectLayer);
        if (footRayDetect)
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
