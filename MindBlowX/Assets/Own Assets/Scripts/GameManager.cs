using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] arrayUnits;
    public AudioSource[] arrayStadiumSounds;
    public Brain aux;

    Brain brainShooter, brainHitted;

    int posShooter, posHitted;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        arrayStadiumSounds[0].Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Swap(Motor motorShooter, Motor motorHitted)
    {
        /*

        for (int j = 0; j < arrayUnits.Length; j++)
        {
            if (arrayUnits[j].GetComponent<Motor>() == motorShooter)
            {
                brainShooter = arrayUnits[j].GetComponent<Motor>().brain;
                posShooter = j;
            }
            if (arrayUnits[j].GetComponent<Motor>() == motorHitted)
            {
                brainHitted = arrayUnits[j].GetComponent<Motor>().brain;
                posHitted = j;
            }
        }
        
         motorShooter.brain = motorHitted.brain;

         motorShooter.brain.Initialize(motorShooter);
         motorShooter.cam.enabled = false;

         motorHitted.brain = brainShooter;

         motorHitted.brain.Initialize(motorHitted);
         motorHitted.cam.enabled = true;
         
        arrayUnits[posShooter].GetComponent<Motor>().brain = brainHitted;
        arrayUnits[posShooter].GetComponent<Motor>().brain.Initialize(arrayUnits[posShooter].GetComponent<Motor>());
        arrayUnits[posShooter].GetComponent<Motor>().cam.enabled = false;

        arrayUnits[posHitted].GetComponent<Motor>().brain = brainShooter;
        arrayUnits[posHitted].GetComponent<Motor>().brain.Initialize(arrayUnits[posHitted].GetComponent<Motor>());
        arrayUnits[posHitted].GetComponent<Motor>().gun.Initialize(arrayUnits[posHitted].GetComponent<Motor>());
        arrayUnits[posHitted].GetComponent<Motor>().cam.enabled = true ;
        */
        Brain aux;
        aux = motorShooter.brain;
        motorShooter.brain = motorHitted.brain;
        motorHitted.brain = aux;


        motorHitted.brain.Initialize(motorHitted);
        motorShooter.brain.Initialize(motorShooter);
    }
    public void roarStadium()
    {
        arrayStadiumSounds[1].Play();
    }
    public void playSwitch()
    {
        arrayStadiumSounds[2].Play();
    }
}