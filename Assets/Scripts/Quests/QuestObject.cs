using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    //[Header("Calling on the Quest and Dialogue Manager Scripts")]
    //public QuestManager questManager;
    //public DialogueBoxManager dialogueManager;

    //[Header("What Quest Number it is")]
    //public int questNumber;

    //[Header("Text File")]
    //public TextAsset theText;

    //[Header("Start of the Text File")]
    //public int startLine;
    //public int endLine;

    //[Header("End of the Text File")]
    //public int startText;
    //public int endText;

    //[Header("Button Pressing")]
    //public bool buttonPress;
    //private bool waitForPress;

    //public bool endQuest;
    //private GameObject npc;



    //void Start ()
    //{
    //    dialogueManager = FindObjectOfType<DialogueBoxManager>();
    //    questManager = FindObjectOfType<QuestManager>();

    //    npc = GameObject.FindGameObjectWithTag("NPCQuest1");

    //}

    //void Update()
    //{
    //    //if (waitForPress && Input.GetKeyDown(KeyCode.Return))
    //    //{
    //    //    dialogueManager.ReloadScript(theText);
    //    //    dialogueManager.currentLine = startLine;
    //    //    dialogueManager.endLine = endLine;
    //    //    dialogueManager.EnableDialogueBox();
    //    //    //charPortrait.gameObject.SetActive(true);
    //    //}
    //}

    //public void StartQuest()
    //{
    //    //if (buttonPress)
    //    //{
    //    //    waitForPress = true;
    //    //    //return;
    //    //}
    //    //questManager.ShowQuestText();

    //    dialogueManager.ReloadScript(theText);
    //    dialogueManager.currentLine = startLine;
    //    dialogueManager.endLine = endLine;
    //    dialogueManager.EnableDialogueBox();

    //    //questStartedText.text = "Quest: " + questNumber;
    //    Debug.Log("Quest Has Started");
    //}

    ////public void EndQuest()
    ////{
    ////    //dialogueManager.DisableDialogueBox();
    ////    //questManager.ShowQuestText();

    ////    //dialogueManager.ReloadScript(theText);
    ////    //dialogueManager.EnableDialogueBox();

    ////    //gameObject.GetComponent<Collider2D>().enabled = false;
    ////    //theQuest.enabled = false;

    ////    dialogueManager.currentLine = startText;
    ////    dialogueManager.endLine = endText;

    ////    questManager.questsCompleted[questNumber] = true;
    ////    gameObject.SetActive(false);
    ////    npc.SetActive(false);

    ////    //questStartedText.enabled = false;
    ////    //questEndedText.text = "Quest: " + questNumber + " Completed!";

    ////    Debug.Log("Quest Has Ended");
    ////    //StartCoroutine(EndedQuest());
    ////}
}
