using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Key Player Stats")]
    public float playerMaxHealth;
    public float playerHealth;
    public int playerLevel;
    public int currentXP;

    public float maxDefence;
    public float currentDefence;

    public float stamina;
    public float maxStamina;
    public float agility;
    public float strength;

    public float playerPosX;
    public float playerPosY;

    public int[] levelUpArray;

    private PlayerManager playerManager;

    [Header("Battle Canvas Texts")]
//   public Text playerBattleHealthText;
//    public Text playerBattleXPText;
 //   public Text playerStaminaText;
//    public Text playerBattleLevelText;

    [Header("Debug Panel Texts")]
    public Text playerLevelText;
    public Text playerHealthText;
    public Text playerXPText;


    // Use this for initialization
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerHealth = playerMaxHealth;

        // Battle Canvas Texts
  //      playerBattleHealthText.GetComponent<Text>();
   //     playerBattleXPText.GetComponent<Text>();
//playerStaminaText.GetComponent<Text>();
   //     playerBattleLevelText.GetComponent<Text>();

        // Debug Panel Texts
        playerHealthText.GetComponent<Text>();
        playerLevelText.GetComponent<Text>();
        playerXPText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {



        playerXPText.text = "Current XP: " + currentXP;
        stamina = maxStamina;

        if (currentXP >= levelUpArray[playerLevel])
        {
            playerLevel++;
        }

        playerHealthText.text = "Player Health: " + playerHealth;

        // Battle Stats
//        playerBattleHealthText.text = "HP: " + playerHealth + "/" + playerMaxHealth;
    //    playerStaminaText.text = "STA: " + stamina + "/" + maxStamina;
    //    playerLevelText.text = "LVL: " + playerLevel;
   //     playerBattleLevelText.text = "Lvl: " + playerLevel;
    //    playerBattleXPText.text = "XP: " + currentXP;

        // Player Position
        playerPosX = this.transform.position.x;
        playerPosY = this.transform.position.y;

    }

    public void AddExperience (int experienceToAdd)
    {
        currentXP += experienceToAdd;
    }

    public void SaveOne()
    {
        SaveLoadManager.SavePlayerOne(this);
    }

    public void SaveTwo()
    {
        SaveLoadManager.SavePlayerTwo(this);
    }

    public void SaveThree()
    {
        SaveLoadManager.SavePlayerThree(this);
    }

    public void LoadOne()
    {
        float[] loadedStats = SaveLoadManager.LoadPlayerStatsOne();

        playerMaxHealth = loadedStats[0];
        playerHealth = loadedStats[1];
        stamina = loadedStats[2];
        maxStamina = loadedStats[3];
        agility = loadedStats[4];
        strength = loadedStats[5];
        maxDefence = loadedStats[6];
        currentDefence = loadedStats[7];
        playerPosX = loadedStats[8];
        playerPosY = loadedStats[9];

        int[] loadedLevels = SaveLoadManager.LoadPlayerLevelsOne();

        playerLevel = loadedLevels[0];
        currentXP = loadedLevels[1];

        transform.position = new Vector3(playerPosX, playerPosY, 0);
    }

    public void LoadTwo()
    {
        float[] loadedStats = SaveLoadManager.LoadPlayerStatsTwo();

        playerMaxHealth = loadedStats[0];
        playerHealth = loadedStats[1];
        stamina = loadedStats[2];
        maxStamina = loadedStats[3];
        agility = loadedStats[4];
        strength = loadedStats[5];
        maxDefence = loadedStats[6];
        currentDefence = loadedStats[7];
        playerPosX = loadedStats[8];
        playerPosY = loadedStats[9];

        int[] loadedLevels = SaveLoadManager.LoadPlayerLevelsTwo();

        playerLevel = loadedLevels[0];
        currentXP = loadedLevels[1];

        transform.position = new Vector3(playerPosX, playerPosY, 0);
    }

    public void LoadThree()
    {
        float[] loadedStats = SaveLoadManager.LoadPlayerStatsThree();

        playerMaxHealth = loadedStats[0];
        playerHealth = loadedStats[1];
        stamina = loadedStats[2];
        maxStamina = loadedStats[3];
        agility = loadedStats[4];
        strength = loadedStats[5];
        maxDefence = loadedStats[6];
        currentDefence = loadedStats[7];
        playerPosX = loadedStats[8];
        playerPosY = loadedStats[9];

        int[] loadedLevels = SaveLoadManager.LoadPlayerLevelsThree();

        playerLevel = loadedLevels[0];
        currentXP = loadedLevels[1];

        transform.position = new Vector3(playerPosX, playerPosY, 0);
    }

}