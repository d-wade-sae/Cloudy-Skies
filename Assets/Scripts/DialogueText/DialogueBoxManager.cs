using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    public GameObject textBox;
    public Text theText;
    public GameObject npc;
    public GameObject signPosts;
    public GameObject playerTag;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endLine;

    public PlayerController player;
    public ActivateDialogue dialoguePortrait;

    public bool dialogueActive;
    public bool stopMovement;

    [Header("Dialogue Text Speed")]
    private bool isTyping;
    private bool cancelTyping;
    public float typeSpeed;

    void Start()
    {
        npc = GameObject.FindGameObjectWithTag("NPC");
        signPosts = GameObject.FindGameObjectWithTag("World Posts");
        playerTag = GameObject.FindGameObjectWithTag("Player");
        player = FindObjectOfType<PlayerController>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }

        if (dialogueActive)
        {
            EnableDialogueBox();
        }

        else
        {
            DisableDialogueBox();

        }

    }

    void Update()
    {

        if (!dialogueActive)
        {
            return;
        }

        //theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endLine)
                {
                    DisableDialogueBox();
                    player.dialogueMovement = true;

                }

                else
                {
                    StartCoroutine(ScrollingText(textLines[currentLine]));
                }

            }

            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }

        //if (currentLine > endLine)
        //{
        //    DisableDialogueBox();
        //    player.dialogueMovement = true;

        //}
    }

    private IEnumerator ScrollingText (string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }


    public void EnableDialogueBox()
    {

        //if (stopMovement)
        //{
        //    player.dialogueMovement = false;
        //}
        textBox.SetActive(true);
        dialogueActive = true;

        player.dialogueMovement = false;

        npc.GetComponent<Collider2D>().enabled = false;
        signPosts.GetComponent<Collider2D>().enabled = false;
        playerTag.GetComponent<Collider2D>().enabled = false;

        StartCoroutine(ScrollingText(textLines[currentLine]));
}

public void DisableDialogueBox()
    {
        textBox.SetActive(false);
        dialogueActive = false;

        player.dialogueMovement = true;

        npc.GetComponent<Collider2D>().enabled = true;
        signPosts.GetComponent<Collider2D>().enabled = true;
        playerTag.GetComponent<Collider2D>().enabled = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
            
        }
    }

}
