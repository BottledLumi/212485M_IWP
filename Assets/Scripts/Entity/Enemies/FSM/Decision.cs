using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Decision : ScriptableObject
{
    public abstract bool Decide();
}
