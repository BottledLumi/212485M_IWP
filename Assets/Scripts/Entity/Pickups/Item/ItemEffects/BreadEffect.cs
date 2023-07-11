using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadEffect : ItemEffect
{
    float baseExtraDamage = 3f;

    public float ExtraDamage()
    {
        return Value * baseExtraDamage;
    }
}
