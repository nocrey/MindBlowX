using System;
using System.Linq;
using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class Pistol : Gun {

    public override void Initialize(Motor _motor)
    {
        
        dmg = 10f;
        range = 100f;
        fireRate = 10f;
        currentAmmo = 40;
        maxAmmo = 40;
        loading = false;
        reloadTime = 3.0f;
        motor = _motor;
        nextTimeToFire = 0f;
        animator = motor.GetComponentInChildren<Animator>();

    }

    public override void Reload()
    {
        currentAmmo = maxAmmo;
        loading = false;
    }

    public override void Shoot()
    {
        currentAmmo--;
        motor.muzzle.Play();
        motor.PlaySound();
        
        animator.SetTrigger("Shoot");
        RaycastHit hit;
        if (Physics.Raycast(motor.cam.transform.position, motor.cam.transform.forward, out hit, range))
        {

            //Target target = hit.transform.GetComponent<Target>();
            Debug.Log(hit.transform.name);
            if(hit.transform.name == "Unit(Clone)" || hit.transform.name == "Unit2")
            {
                Debug.Log("BAM");
                motor.GetComponentInParent<GameManager>().Swap(motor, hit.transform.GetComponentInParent<Motor>());
            }
            /*if (hit.transform.GetComponent<ControllerUnit>() != null)
            {
                if (hit.transform.GetComponent<ControllerUnit>().UnitMind.name == "Enemy")
                {
                    GetComponentInParent<TestStuff>().Swap(mind.id, hit.transform.GetComponent<ControllerUnit>().UnitMind.id);
                }
                if (target != null)
                {
                    target.TakeDamage(dmg);

                }
            }
            /*if (hit.transform.name.ToString() == "Enemy") {
				
				SwapVector = hit.transform.position;
				SwapRot = hit.transform.rotation;
				SwapCam.CopyFrom(hit.transform.GetComponentInChildren<Camera>());

				hit.transform.position = this.transform.position;
				hit.transform.rotation = this.transform.rotation;
				hit.transform.GetComponentInParent<Camera>().CopyFrom(fpsCam);

				this.GetComponent<Camera>().CopyFrom(SwapCam);
				this.transform.position = SwapVector;
				this.transform.rotation = SwapRot;

			}*/
           GameObject impactGameObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
           Destroy(impactGameObj, 1f);

        }
    }

    public override void UpdateGun(Motor motor)
    {
      
        if (!loading && currentAmmo > 0 && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if (!loading && currentAmmo <= 0)
        {
            loading = true;
            motor.Invoke("Reload", reloadTime);
        }
    }

   
}
