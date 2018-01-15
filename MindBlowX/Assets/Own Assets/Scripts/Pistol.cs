using System;
using System.Linq;
using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class Pistol : Gun {

    public override void Initialize(Motor _motor)
    {

        currentAmmo = maxAmmo;
        loading = false;
        motor = _motor;
        nextTimeToFire = 0f;
        animator = motor.GetComponentInChildren<Animator>();

    }

   
}
