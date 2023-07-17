using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    public Enemy enemy;

    [SerializeField] BaseState initialState;
    public BaseState currentState;

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
