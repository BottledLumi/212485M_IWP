using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Action/ChangeColour Action")]
public class ChangeColourAction : FSMAction
{
    [SerializeField] Color color;
    public override void Execute(BaseStateMachine bsm)
    {
        bsm.GetComponent<SpriteRenderer>().color = color;
    }
}
