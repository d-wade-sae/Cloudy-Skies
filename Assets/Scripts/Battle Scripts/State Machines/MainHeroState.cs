using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHeroState : MonoBehaviour {

    private BattleManager battleManager;
    public MainHero hero;
    public bool isBattlePlayer = false;
    private bool inBattle = false;
    public GameObject playerWorld;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    private float currentCooldown = 0.0f;
    private float maxCooldown = 1f;
    // public Image progressBar;

    // Indicator for which hero is active
    public GameObject selector;

    // Attack Movements
    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 10f;
    private bool alive = true;

    [Header("Hero Panel Stats")]
    private HeroPanelStats heroPanelStats;
    public GameObject heroPanel;
    private Transform heroPanelSpacer;


    void Start ()
    {
        // Use currentCooldown = Random.Range(0, agility) to increase ability to see who starts first

        battleManager = GameObject.Find("Battle Area").GetComponent<BattleManager>();

        // Hero Panel Spawning
        heroPanelSpacer = GameObject.Find("Battle Canvas").transform.Find("Hero Panel").transform.Find("Hero Panel Spacer");
        CreateHeroPanel();
        UpdatePlayerStats();

        currentState = TurnState.PROCESSING;
        selector.SetActive(false);
        startPosition = transform.position;
        
    }
	

	void Update ()
    {

        print("Player State: "+ currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.ADDTOLIST):
                battleManager.HerosToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;

                break;

            case (TurnState.WAITING): // Idle State

                break;

            case (TurnState.ACTION): // Action State
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                    if(!alive)
                    {
                        return;
                    }
                    else
                    {
                    // Change Tag
                    this.gameObject.tag = "DeadHero";
                    // Can't be attacked
                    battleManager.HerosInGame.Remove(this.gameObject);
                    // Not able to use hero
                    battleManager.HerosToManage.Remove(this.gameObject);
                    // De-Activate Selector
                    selector.SetActive(false);
                    // Reset GUI
                    battleManager.actionsPanel.SetActive(false);
                    battleManager.enemySelectPanel.SetActive(false);

                    // Remove from Perform List
                    if (battleManager.HerosInGame.Count > 0)
                    {
                        for (int i = 0; i < battleManager.PerformList.Count; i++)
                            if (i != 0)
                            {
                                {
                                    if (battleManager.PerformList[i].AttackersGameObject == this.gameObject)
                                    {
                                        battleManager.PerformList.Remove(battleManager.PerformList[i]);
                                    }

                                    if (battleManager.PerformList[i].AttackersTarget == this.gameObject)
                                    {
                                        battleManager.PerformList[i].AttackersTarget = battleManager.HerosInGame[Random.Range(0, battleManager.HerosInGame.Count)];
                                    }
                                }
                            }
                    }

                    // Change Colour/ Play Animation
                    this.gameObject.SetActive(false);
                    // Reset Hero Input
                    battleManager.battleStates = BattleManager.PerformAction.CHECKALIVE;


                    alive = false;
                    }

                break;
        }
    }

    void UpgradeProgressBar()
    {
        currentCooldown = currentCooldown + Time.deltaTime;

        if (currentCooldown >= maxCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        // Moving towards Hero to Attack
        actionStarted = true;
        Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x - 1.5f, enemyToAttack.transform.position.y, enemyToAttack.transform.position.z);
        while (MoveTowardsEnemy(enemyPosition))
        {
            yield return null;
        }

        // Wait for x seconds infront of Hero
        yield return new WaitForSeconds(0.5f);

        // Damage Target
        DoDomage();

        // Move back to start Position
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        // Remove from the Battle Manager Perform List
        battleManager.PerformList.RemoveAt(0);

        if (battleManager.battleStates != BattleManager.PerformAction.WIN && battleManager.battleStates != BattleManager.PerformAction.LOSE)
        {
            // Resetting the Battle Manager State Machine
            battleManager.battleStates = BattleManager.PerformAction.WAIT;

            // Reset the state machine into wait cycle
            currentCooldown = 0f;
            currentState = TurnState.PROCESSING;
        }

        else
        {
            currentState = TurnState.WAITING;
        }

        UpdatePlayerStats();
        actionStarted = false;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    void DoDomage()
    {
        float calculateDamage = hero.currentAttack + battleManager.PerformList[0].choosenAttack.attackDamage;
        enemyToAttack.GetComponent<EnemyState>().TakeDamage(calculateDamage);
    }

    public void TakeDamage(float getDamageAmount)
    {
        hero.currentHP -= getDamageAmount;
        if(hero.currentHP <= 0)
        {
            hero.currentHP = 0;
            currentState = TurnState.DEAD;
        }

        UpdateHeroPanel();
        UpdatePlayerStats();
    }

    void CreateHeroPanel()
    {
        heroPanel = Instantiate(heroPanel) as GameObject;
        heroPanelStats = heroPanel.GetComponent<HeroPanelStats>();
        heroPanelStats.heroName.text = hero.theName;
        heroPanelStats.heroHP.text = "HP: " + hero.currentHP + "/" + hero.baseHP;
        heroPanelStats.heroSTA.text = "STA: " + hero.currentStamina + "/" + hero.baseStamina;
        heroPanelStats.heroLVL.text = "LVL: " + hero.heroLevel;
        heroPanelStats.heroXP.text = "XP: " + hero.heroXP;

        heroPanel.transform.SetParent(heroPanelSpacer, false);

    }

    void UpdateHeroPanel()
    {
        heroPanelStats = heroPanel.GetComponent<HeroPanelStats>();
        heroPanelStats.heroHP.text = "HP: " + hero.currentHP + "/" + hero.baseHP;
        heroPanelStats.heroSTA.text = "STA: " + hero.currentStamina + "/" + hero.baseStamina;
    }

    void UpdatePlayerStats()
    {
        if(isBattlePlayer)
        {
            playerWorld.GetComponent<PlayerStats>().playerHealth = hero.currentHP;
            playerWorld.GetComponent<PlayerStats>().stamina = hero.currentStamina;
            playerWorld.GetComponent<PlayerStats>().playerLevel = hero.heroLevel;
            playerWorld.GetComponent<PlayerStats>().currentXP = hero.heroXP;

            print("Updated Player Stats");
        }
        
        else
        {
            print("Not updating Player Stats");
        }
    }

    public void UpdatePlayerLevel ()
    {
        if(isBattlePlayer)
        {
            hero.heroLevel = playerWorld.GetComponent<PlayerStats>().playerLevel;
            hero.heroXP = playerWorld.GetComponent<PlayerStats>().currentXP;
        }
    }
    
}
