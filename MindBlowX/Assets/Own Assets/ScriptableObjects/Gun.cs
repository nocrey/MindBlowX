using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : ScriptableObject {
    public float dmg = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public int currentAmmo;
    public int maxAmmo;
    public bool loading;
    public bool swap;

    public GameObject Model;
    public Motor motor;
    public GameObject impactEffect;
    public ParticleSystem muzzle;
    public AudioClip shotsound;
    public AudioClip reloadSound;

    public AudioSource source;

    public float reloadTime = 3.0f;

    public float nextTimeToFire = 0f;

    public Animator animator;

    public abstract void Initialize(Motor motor);
    public abstract void UpdateGun(Motor motor);
    public abstract void Shoot();
    public abstract void Reload();

}
