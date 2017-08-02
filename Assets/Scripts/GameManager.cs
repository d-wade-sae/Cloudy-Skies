using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public GameObject inventory;

    [Header("Area Objects")]
    public GameObject battleArea;
    public GameObject world;

    [Header("Canvas and Cameras")]
    public GameObject worldCamera;
    public GameObject battleCamera;
    public GameObject DebugCanvas;
    public bool enableDebugCanvas = false;
    public bool disableMusic = false;
    private AudioSource worldCameraAudio;
    private AudioSource battleCameraAudio;

    [Header("Battle Variables")]
    public int invokeBattleCount = 0;
    private bool resetCount = false;
    public bool roamingEnemyEncounter = false;
    public bool encounter = false;
    public int enemyAmount;

   [System.Serializable]
    public class TileManagerData
    {
        public string tileName;
        public int maxAmountEnemies = 3;
        public string battleBackground;
        public List<GameObject> possibleEnemies = new List<GameObject>();
    }
    

    public List<TileManagerData> tileManagerData = new List<TileManagerData>();

    public List<GameObject> enemiesToBattle = new List<GameObject>();

    public int currentRegion;

    [Header("Prefabs")]
    public static GameManager instance;

    [Header("States")]
    public GameStates gameState;

    public enum GameStates
    {
        WORLD,
        SAFE,
        BATTLE,
        IDLE
    }

    // Private Variables
    private BattleManager battleManager;

    [Header("Start Items")]
    public Item[] startingItems = new Item[5];

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if(!GameObject.Find("Player"))
        {
            GameObject PlayerSpawn = Instantiate(player, Vector2.zero, Quaternion.identity) as GameObject;
            Vector2 playerSpawnLocation = new Vector2(-1, 1);
            PlayerSpawn.transform.position = playerSpawnLocation;
            PlayerSpawn.name = "Player";
        }

        world.SetActive(true);
        battleArea.SetActive(true);

        battleManager = GameObject.Find("Battle Area").GetComponent<BattleManager>();

        worldCameraAudio = this.GetComponent<AudioSource>();
        battleCameraAudio = battleCamera.GetComponent<AudioSource>();

        GameStart(); // all set up variables (just moved them out of awake function

    }

    void Update()
    {
        switch(gameState)
        {
            case (GameStates.WORLD):
                if (invokeBattleCount == 1)
                {
                    gameState = GameStates.BATTLE;
                }
                break;

            case (GameStates.SAFE):

                break;

            case (GameStates.BATTLE):
                gameState = GameStates.IDLE;
                break;

            case (GameStates.IDLE):
                if (invokeBattleCount == 0)
                {
                    gameState = GameStates.WORLD;
                }
                break;
        }

        if (enableDebugCanvas == false)
        {
            DebugCanvas.SetActive(false);
        }

        if (disableMusic == true)
        {
            worldCameraAudio.enabled = false;
            battleCameraAudio.enabled = false;
        }

    }

    public void BattleReset()
    {
        StartCoroutine(InvokeBattleReset());
    }

    public IEnumerator InvokeBattleReset()
    {
        print("Invoking Battle Reset");
        if (resetCount)
        {
            yield break;
        }

        resetCount = true;

        yield return new WaitForSeconds(5f);

        battleManager.battleStates = BattleManager.PerformAction.WAIT;
        invokeBattleCount = 0;
        
        print("End of Battle Reset");
        resetCount = false;
    }

    public void Encounter()
    {
        print("Encounter");
        enemyAmount = Random.Range(1, tileManagerData[currentRegion].maxAmountEnemies + 1);
        for (int i = 0; i < enemyAmount; i++)
        {
            enemiesToBattle.Add(tileManagerData[currentRegion].possibleEnemies[Random.Range(0, tileManagerData[currentRegion].possibleEnemies.Count)]);
        }

        battleManager.BattleLoad();
        player.SetActive(false);
    }

    public void GameStart()
    {
        world.SetActive(true);
        battleArea.SetActive(false);
        worldCamera.SetActive(true);
        battleCamera.SetActive(false);
        player.SetActive(true);
        PlayerInventoryStart();
    }

    public void PlayerInventoryStart ()
    {
        foreach (Item item in startingItems)
        {
            Inventory playerInventory = inventory.GetComponent<Inventory>();
            playerInventory.AddItem(item);
        }
    }
}
