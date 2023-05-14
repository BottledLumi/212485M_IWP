using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] private float defence;
    [SerializeField] private float attackSpeed;
    [SerializeField] public float movementSpeed;

    private TMP_Text statsText;

    void Start()
    {
        statsText = GameObject.Find("UI").transform.Find("PlayerStats_Text").GetComponent<TMP_Text>();
        UpdateStatsText();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateStatsText()
    {
        if (statsText != null)
        {
            statsText.text = "Stats:" 
                + "\nATK: " + attack.ToString()
                + "\nDEF: " + defence.ToString()
                + "\nATK SPD: " + attackSpeed.ToString()
                + "\nMOVEMENT SPD: " + movementSpeed.ToString();
        }
    }
}
