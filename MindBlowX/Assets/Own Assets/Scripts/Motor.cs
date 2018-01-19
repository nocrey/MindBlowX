using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour {
    public Brain brain;
    public Gun gun;
    public Camera cam;
    public Stats stats;

    public GameObject grenadePrefab;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Quaternion cameraRotationTarget;
    public float speed = 8f;

    private Rigidbody rb;
    private SphereCollider col;

    public float jumpForce = 3f;
    public bool zooming = false;

    public ParticleSystem muzzle;
    public AudioSource aSource;
    public AudioListener aListener;
    public GameObject deathExplosion;

    public float minRotation = -80f;
    public float maxRotation = 80f;

    public float dashForce = 20f;
    public GameObject weakPoint;

    public float hp;
    public float armor;

    public bool mindAim = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();
        aListener = GetComponentInChildren<AudioListener>();
        
        brain.Initialize(this);
        gun.Initialize(this);
        stats.Initialize(this);
        
        
        //gun.Model.transform.localScale = -new Vector3(0.5f, 0.5f, 0.5f);
        //Object.Instantiate(gun.Model, new Vector3(transform.position.x+0.5f, transform.position.y-0.2f, transform.position.z +0.8f), Quaternion.Euler (0,0,180), cam.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        brain.Think(this);
        //gun.UpdateGun(this);
        PerformMovement();
        PerformRotation();
        updateCam();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            if (zooming == true)
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime * (speed / 2f));
            }
            else
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime * speed);
            }
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        

        if (cam != null)
        {
            

            //CODE WITHOUT SMOOTH
            cam.transform.localRotation = cameraRotationTarget;

            //CODE WITH SMOOTH

            //cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, cameraRotationTarget,(15f * Time.deltaTime));
            //float x = Mathf.Clamp(cam.transform.rotation.x, -90, 90);
            //cam.transform.eulerAngles = new Vector3(x, 0, 0);
        }
    }

    public void Rotate(Vector3 _Rotation)
    {
        rotation = _Rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotationTarget = cam.transform.localRotation;
        cameraRotationTarget *= Quaternion.Euler(-_cameraRotation.x, 0f, 0f);

        cameraRotationTarget = ClampRotationAroundXAxis(cameraRotationTarget);
        
    }

    public void Jump(float _jumpForce)
    {
        jumpForce = _jumpForce;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Reload()
    {
        
        gun.loading = false;
        gun.currentAmmo = gun.maxAmmo;
    }
   
    public void PlaySound()
    {
        aSource.volume = 1;
        aSource.pitch = (Random.Range(0.6f, 1.2f));
        aSource.PlayOneShot(gun.shotsound, 1f);
    }

    public void zoomCamera()
    {
      if (cam.fieldOfView >= 20)
      {
            cam.fieldOfView = cam.fieldOfView - 2;
      }
    }

    private void updateCam()
    {
        if (cam.fieldOfView < 60 && zooming == false)
        {
            cam.fieldOfView = cam.fieldOfView+2;
        }
       
    }


    public void ShootGun()
    {

        if (gun.loading == false && gun.currentAmmo > 0 && Time.time >= gun.nextTimeToFire) {
            gun.nextTimeToFire = Time.time + 1f / gun.fireRate;
            gun.currentAmmo--;
            muzzle.Play();
            PlaySound();
            gun.animator.SetTrigger("Shoot");
            RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, gun.range))
                {

                    //Target target = hit.transform.GetComponent<Target>();
                if (hit.collider.name == "weakPoint")
                {
                    hit.collider.GetComponentInParent<Motor>().receiveDMG(20000);
                }

                if (hit.collider.name == "Body")
                {
                    hit.collider.GetComponentInParent<Motor>().receiveDMG(gun.dmg);
                }
                GameObject impactGameObj = Instantiate(gun.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGameObj, 1f);
            }
            if (gun.currentAmmo == 0)
            {
                gun.animator.SetTrigger("NoAmmo");
            }
        }
    }


    public void mindShot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, gun.range))
        {

            //Target target = hit.transform.GetComponent<Target>();
            if (hit.collider.name == "mindSpot")
            {
                this.GetComponentInParent<GameManager>().Swap(this, hit.transform.GetComponentInParent<Motor>());
                this.GetComponentInParent<GameManager>().playSwitch();
                Invoke("swapMindAim", 1f);
            }

        }
    }
    void swapMindAim()
    {
        mindAim = false;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minRotation, maxRotation);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
    public void dash()
    {
        rb.AddForce(cam.transform.forward * dashForce, ForceMode.Impulse);
    }

    public void receiveDMG(float dmg)
    {
        hp = hp-dmg;
        if (hp <= 0)
        {
            GameObject deathGameObj = Instantiate(deathExplosion, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(deathGameObj, 3f);
            GetComponentInParent<GameManager>().roarStadium();
            Destroy(this.gameObject);
            
        }
    }

    public void throwGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, new Vector3(cam.transform.position.x-2, cam.transform.position.y+2, cam.transform.position.z), cam.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(cam.transform.forward * 40f, ForceMode.VelocityChange);
        Destroy(grenade, 3f);
    }
}
