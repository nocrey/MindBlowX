using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Brain : ScriptableObject {
    public int id;
    public Motor motor;
    public float mouseSensibility = 4f;

    public abstract void Initialize(Motor motor);
    public abstract void Think(Motor motor);

}
