using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Stats : ScriptableObject
{
    public float hp;
    public float movSpeed;
    public float armor;

    // Use this for initialization
    public abstract void Initialize(Motor motor);
    
}
	
