using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class UnitStats : Stats {
   
    // Use this for initialization
    public override void Initialize (Motor _motor) {
        hp = 10;
        movSpeed = 8f;
        armor = 0f;
      
        _motor.hp = hp;
        _motor.armor = armor;
        _motor.speed = movSpeed;
	}
	
}

