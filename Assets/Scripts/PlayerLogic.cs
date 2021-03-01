using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
    Vector2 naam = new Vector2(0, 0);
    float rotation = 0;

    float speed = 10f;
    Vector2 velocity;

    public void WALK(InputAction.CallbackContext context)
    {
        naam = context.ReadValue<Vector2>();
    }

    public void ROTATE(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<float>();
    }

    public void Update()
    {
        Vector3 testVector = transform.forward * naam.y + naam.x * transform.right;
        velocity = new Vector2(speed * Vector3.Normalize(testVector).x * Time.deltaTime,
                               speed * Vector3.Normalize(testVector).z * Time.deltaTime);

        GetComponent<CharacterController>().Move(new Vector3(velocity.x, 0, velocity.y));


        transform.Rotate(new Vector3(0, rotation, 0) * Time.deltaTime * 40f);
    }
}
