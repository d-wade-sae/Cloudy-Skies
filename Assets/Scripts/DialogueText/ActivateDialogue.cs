using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    public TextAsset theText;
    public Transform charPortrait;

    public int startLine;
    public int endLine;

    public DialogueBoxManager dialogueManager;

    public bool destroy;

    public bool buttonPress;
    private bool waitForPress;

    void Start ()
    {
        dialogueManager = FindObjectOfType<DialogueBoxManager>();
    }

    void Update ()
    {
        if (waitForPress && Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.ReloadScript(theText);
            dialogueManager.currentLine = startLine;
            dialogueManager.endLine = endLine;
            dialogueManager.EnableDialogueBox();
            charPortrait.gameObject.SetActive(true);

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);

            }


            if (destroy)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {

            if (buttonPress)
            {
                waitForPress = true;
                return;
            }

            dialogueManager.ReloadScript(theText);
            dialogueManager.currentLine = startLine;
            dialogueManager.endLine = endLine;
            dialogueManager.EnableDialogueBox();

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(true);

            }

            if (destroy)
            {
                Destroy(gameObject);
            }

        }

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForPress = false;

            if (charPortrait.gameObject.activeInHierarchy == false)
            {
                charPortrait.gameObject.SetActive(false);

            }

        }

    }

}
