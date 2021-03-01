using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform fpsTransform;
    public Transform thirdPTransform;
    Vector3 currentPosition;
    Quaternion currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        currentRotation = transform.rotation;
    }

    public void FPSMode()
    {
        transform.rotation = fpsTransform.rotation;
        transform.position = fpsTransform.position;
    }

    public void ThirdPersonMode()
    {
        transform.position = thirdPTransform.position;
        transform.rotation = thirdPTransform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
      //  transform.LookAt(player);
    }
}
