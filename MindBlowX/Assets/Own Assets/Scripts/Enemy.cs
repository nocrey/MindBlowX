using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Enemy : Brain
{
    Vector3 velocity;
    // Use this for initialization
    public override void Initialize(Motor _motor)
    {
        motor = _motor;
        motor.cam.enabled = false;
     

    }

    public override void Think(Motor motor)
    {
        /*float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 HorizontalMovement = motor.transform.right * xMovement;
        Vector3 VerticalMovement = motor.transform.forward * zMovement;

        velocity = (HorizontalMovement + VerticalMovement).normalized;

        motor.Move(velocity);

        //Rotation 
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * mouseSensibility;

        motor.Rotate(rotation);

        //Rotation Camera
        float xRotation = Input.GetAxisRaw("Mouse Y");
        Vector3 CameraRotation = new Vector3(xRotation, 0f, 0f) * mouseSensibility;

        motor.RotateCamera(CameraRotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            motor.Jump(motor.jumpForce);
        }
    
    */
        // Update is called once per frame
    }
}
