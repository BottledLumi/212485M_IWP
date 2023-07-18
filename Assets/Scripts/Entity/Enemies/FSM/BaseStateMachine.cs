using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;

    [SerializeField] BaseState initialState;
    [HideInInspector] public BaseState currentState;

    private void Awake()
    { 
        enemy = gameObject.GetComponent<Enemy>();
        currentState = initialState;
    }

    private void Update()
    {
        currentState.Execute(this);
    }
}
