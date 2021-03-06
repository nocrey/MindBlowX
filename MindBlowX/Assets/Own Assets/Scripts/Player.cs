﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Player : Brain {
    Vector3 velocity;
    // Use this for initialization
    public override void Initialize(Motor _motor)
    {
        motor=_motor;
        motor.cam.enabled = true;
        motor.aListener.enabled = true;

    }

    public override void Think(Motor motor)
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
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
        

        if (Input.GetKey("e"))
        {
            motor.zoomCamera();
            motor.zooming = true;
            motor.mindAim = true;
            if (Input.GetButton("Fire1"))
            {
                Debug.Log("MINDSHOT");
                motor.mindShot();
            }
        }
        if (Input.GetButton("Fire1") && motor.mindAim == false)
        {
            motor.ShootGun();
        }
        if (Input.GetKeyUp("e"))
        {
            motor.mindAim = false;
            motor.zooming = false;
        }


        if (Input.GetKeyDown("r"))
        {
            if (motor.gun.loading == false)
            {
                motor.gun.animator.SetTrigger("Reload");
                motor.gun.loading = true;


                motor.aSource.volume = 2;
                motor.aSource.pitch = 0.5f;
                motor.aSource.PlayOneShot(motor.gun.reloadSound, 1f);
                motor.Invoke("Reload", motor.gun.reloadTime);
            }
        }

        if (Input.GetKey("q"))
        {
            motor.zoomCamera();
            motor.zooming = true;

        }
        if (Input.GetKeyDown("f"))
        {
            motor.throwGrenade();

        }
        if (Input.GetKeyUp("q"))
        {
            motor.zooming = false;
        }
        if (Input.GetKeyDown("t"))
        {
            motor.dash();
        }
    }

    // Update is called once per frame
    
}
