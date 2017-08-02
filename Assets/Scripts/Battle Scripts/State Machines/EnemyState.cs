using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    private BattleManager battleManager;
    public BaseEnemy enemy;

    // Indicator for which enemy is active
    public GameObject selector;


    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    private float currentCooldown = 0.0f;
    private float maxCooldown = 5f;

    private Vector3 startPosition;

    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 10f;

    private bool alive = true;


    void Start()
    {
        currentState = TurnState.PROCESSING;
        selector.SetActive(false);
        battleManager = GameObject.Find("Battle Area").GetComponent<BattleManager>();
        startPosition = transform.position;
        // EnemyStats();
    }


    void Update()
    {
        print(enemy.theName + " Enemy State: " + currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = ("DeadEnemy");
                    battleManager.EnemiesInBattle.Remove(this.gameObject);
                    Destroy(this.gameObject);

                    if (battleManager.EnemiesInBattle.Count > 0)
                    {
                        for (int i = 0; i < battleManager.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (battleManager.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    battleManager.PerformList.Remove(battleManager.PerformList[i]);
                                }
                                if (battleManager.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    battleManager.PerformList[i].AttackersTarget = battleManager.EnemiesInBattle[Random.Range(0, battleManager.EnemiesInBattle.Count)];
                                }
                            }
                        }
                    }
                    this.gameObject.SetActive(false);
                    alive = false;
                    battleManager.EnemyButtons();

                    battleManager.battleStates = BattleManager.PerformAction.CHECKALIVE;
                }
                break;
        }
    }

    void UpgradeProgressBar()
    {
        currentCooldown = currentCooldown + Time.deltaTime;

        if (currentCooldown >= maxCooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }


    }

    void ChooseAction()
    {
        TurnBasedHandler myattack = new TurnBasedHandler();
        myattack.Attacker = enemy.theName;
        myattack.Type = "Enemy";
        myattack.AttackersGameObject = this.gameObject;
        myattack.AttackersTarget = battleManager.HerosInGame[Random.Range(0, battleManager.HerosInGame.Count)];

        int num = Random.Range(0, enemy.attacks.Count);
        myattack.choosenAttack = enemy.attacks[num];
        enemy.currentStamina = enemy.currentStamina-myattack.choosenAttack.attackCost;
        print(this.gameObject.name + " has choosen " + myattack.choosenAttack.attackName + " and does " + myattack.choosenAttack.attackDamage + " damage!");



        battleManager.CollectActions(myattack); // sends the information to perform list via Collect Actions
    }

    private IEnumerator TimeForAction()
    {
        if(actionStarted)
        {
            yield break;
        }

        // Moving towards Hero to Attack
        actionStarted = true;
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
        while (MoveTowardsEnemy(heroPosition))
        {
            yield return null;
        }

        // Wait for x seconds infront of Hero
        yield return new WaitForSeconds(0.5f);

        // Damage Said Hero
        DoDamage();

        // Move back to start Position
        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        // Remove from the Battle Manager Perform List
        battleManager.PerformList.RemoveAt(0);

        // Resetting the Battle Manager State Machine
        battleManager.battleStates = BattleManager.PerformAction.WAIT;

        actionStarted = false;
        // Reset the state machine into wait cycle
        currentCooldown = 0f;
        currentState = TurnState.PROCESSING;

    }

    private bool MoveTowardsEnemy (Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    
    /*
    public void EnemyStats ()
    {
        if (enemy.enemyType == BaseEnemy.Type.WOLF)
        {
            enemy.baseHP = 25f;
            enemy.baseAttack = 10f;
            enemy.baseDefence = 5f;

        }
        if (enemy.enemyType == BaseEnemy.Type.EAGLE)
        {
            enemy.baseHP = 15f;
            enemy.baseAttack = 15f;
            enemy.baseDefence = 3f;
        }
    }

    */
    void DoDamage()
    {

        float calculateDamage = enemy.currentAttack + battleManager.PerformList[0].choosenAttack.attackDamage;
        HeroToAttack.GetComponent<MainHeroState>().TakeDamage(calculateDamage);
    }

    public void TakeDamage(float getDamageAmount)
    {
        enemy.currentHP -= getDamageAmount;
        if (enemy.currentHP <= 0)
        {
            enemy.currentHP = 0;
            currentState = TurnState.DEAD;
        }
    }




} 