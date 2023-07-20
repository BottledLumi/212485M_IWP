using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;

    [HideInInspector] public GameObject player;

    [SerializeField] BaseState initialState;
    [HideInInspector] public BaseState currentState;

    private void Awake()
    { 
        enemy = gameObject.GetComponent<Enemy>();

        player = GameObject.FindGameObjectWithTag("Player");

        currentState = initialState;
    }

    private void Update()
    {
        currentState.Execute(this);
    }
}
