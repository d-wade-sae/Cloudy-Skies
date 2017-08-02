using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Calling on the Dialogue Manager Script")]
    public DialogueBoxManager dialogueManager;

    [Header("What Quest Number it is")]
    public QuestTrigger[] quests;

    [Header("Yes/No - Quest Completed")]
    public bool[] questsCompleted;

    //[Header("Text File")]
    //public TextAsset theText;

    //[Header("Start of the Text File")]
    //public int startLine;
    //public int endLine;

    void Start ()
    {
        questsCompleted = new bool[quests.Length];
        //New array the same length as the quests
	}
	
	void Update ()
    {
		
	}

    public void ShowQuestText()
    {
        dialogueManager.EnableDialogueBox();
        dialogueManager.currentLine = 0;
    }
}
