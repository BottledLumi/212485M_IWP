using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : Enemy // Very scuffed init order and calls for now, fix in the future maybe
{
    private void Start()
    {
        GameStateManager.instance.CallBossEvent(this);
    }
}
