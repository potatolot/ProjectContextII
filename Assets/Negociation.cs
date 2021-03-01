using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negociation : MonoBehaviour
{
    public CameraLogic mainCam;
    Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = transform.position;
            other.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
            mainCam.FPSMode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
            mainCam.ThirdPersonMode();
       
    }
}
