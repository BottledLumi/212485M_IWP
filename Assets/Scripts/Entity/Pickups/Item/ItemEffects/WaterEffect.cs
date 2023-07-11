using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterEffect : ItemEffect
{
    float baseMsMultiplier = 0.8f;

    public float MovementSpeedMultiplier()
    {
        return Mathf.Pow(baseMsMultiplier, Value);
    }
}
