using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    [Header("Calling on the Quest and Dialogue Manager Scripts")]
    private QuestManager questManager;
    private DialogueBoxManager dialogueManager;

    [Header("What Quest Number it is")]
    public int questNumber;

    public bool startQuest;
    public bool endQuest;

    [Header("Text File")]
    public TextAsset theText;

    [Header("Start of the Text File")]
    public int startLine;
    public int endLine;

    [Header("End of the Text File")]
    public int startText;
    public int endText;

    [Header("Button Pressing")]
    public bool buttonPress;
    private bool waitForPress;

    [Header("NPC Gameobjects")]
	private GameObject npc;
	public QuestTrigger theQuest;
    public bool destroy;

    [Header("Quest Canvas UI")]
    public Text questStartedText;
    public Text questEndedText;
    public GameObject questDisappearingText;

    private GameObject triggerQuest;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        dialogueManager = FindObjectOfType<DialogueBoxManager>();

        triggerQuest = GameObject.FindGameObjectWithTag("TutQuest");
        npc = GameObject.FindGameObjectWithTag("TutQuest1");

    }

    //void Update()
    //{
    //    if (waitForPress && Input.GetKeyDown(KeyCode.Return))
    //    {
    //        dialogueManager.ReloadScript(theText);
    //        dialogueManager.currentLine = startLine;
    //        dialogueManager.endLine = endLine;
    //        dialogueManager.EnableDialogueBox();
    //        //            //charPortrait.gameObject.SetActive(true);
    //    }
    //    Only needed if the player is using button pressing to initiate the quest(currently bugged and needs fixing)
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //if (buttonPress)
            //{
            //    waitForPress = true;
            //    return;
            //}
            //Not relevant right now

            StartQuest ();

            if (!questManager.questsCompleted[questNumber])
            {
                //StartQuest();
                if (startQuest && !questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].gameObject.SetActive(true);
                    questManager.quests[questNumber].StartQuest();
                    
                    //StartQuest ();
                    //dialogueManager.ReloadScript(theText);
                    //dialogueManager.currentLine = startLine;
                    //dialogueManager.endLine = endLine;
                    //Not relevant right now
                }

                if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.quests[questNumber].EndQuest();
                    triggerQuest.SetActive(false);
                    npc.SetActive(false);
                    //EndQuest();

                    //StartCoroutine(EndedQuest());


                    //dialogueManager.ReloadScript(theText);
                    //dialogueManager.currentLine = startText;
                    //dialogueManager.endLine = endText;
                    //gameObject.SetActive(false);
                    //gameObject.GetComponent<Collider2D>().enabled = false;
                    //Not relevant right now
                }
            }
        }
    }

    public void StartQuest()
    {
        //if (buttonPress)
        //{
        //    waitForPress = true;
        //    //return;
        //}
        //questManager.ShowQuestText();

        dialogueManager.ReloadScript(theText);
        dialogueManager.currentLine = startLine;
        dialogueManager.endLine = endLine;
        dialogueManager.EnableDialogueBox();

        //triggerQuest.GetComponent<Collider2D>().enabled = false;
        triggerQuest.GetComponent<Collider2D>().enabled = false;

        questStartedText.text = "Quest: " + questNumber;
        Debug.Log("Quest Has Started");

        if (destroy)
        {
            Destroy(gameObject);
        }
    }

    public void EndQuest()
    {
        dialogueManager.DisableDialogueBox();
        //questManager.ShowQuestText();

        //dialogueManager.ReloadScript(theText);
        dialogueManager.EnableDialogueBox();

        //gameObject.GetComponent<Collider2D>().enabled = false;
        //theQuest.enabled = false;

        dialogueManager.currentLine = startText;
        dialogueManager.endLine = endText;

        questManager.questsCompleted[questNumber] = true;
        //gameObject.SetActive(false);
        //npc.SetActive(false);

       // npc.GetComponent<Collider2D>().enabled = false;

        npc.GetComponent<Collider2D>().enabled = false;

        questStartedText.enabled = false;
        questEndedText.text = "Quest: " + questNumber + " Completed!";

        if (destroy)
        {
            Destroy(npc);
        }

        Debug.Log("Quest Has Ended");
    }

    //IEnumerator EndedQuest()
    //{
    //    yield return new WaitForSeconds(4);
    //    //questEndedText.text = " ";
    //    questDisappearingText.SetActive(false);

    //    Debug.Log("IS GONE");
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.name == "Player")
    //    {
    //        waitForPress = false;
    //        StartQuest();

    //    }
    //    StartQuest();
    //}
}
