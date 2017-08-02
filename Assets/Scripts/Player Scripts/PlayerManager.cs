using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    [Header("Inventory")]

    private PlayerStats playerStats;
    public GameObject gameManager;
    public GameObject battleManager;

    // Use this for initialization

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        battleManager.GetComponent<BattleManager>();
        gameManager.GetComponent<GameManager>();
    }

        void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Roaming Enemies"))
        {
            if (gameManager.GetComponent<GameManager>().invokeBattleCount == 0)
            {
                gameManager.GetComponent<GameManager>().roamingEnemyEncounter = true;
                gameManager.GetComponent<GameManager>().Encounter();
                gameManager.GetComponent<GameManager>().invokeBattleCount++;
            }
        }
    }

    void InvokeBattle()
    {
        battleManager.GetComponent<BattleManager>().BattleLoad();
    }

}
