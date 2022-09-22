using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAccessDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;
    public Dialogue dialogue;
    public GameObject dialogueBoxObject;

    void Start()
    {
        if(Server.firstOpenning)    
            StartCoroutine(PresentationGame());
        else
            trigger.gameObject.SetActive(false);
    }

    IEnumerator PresentationGame() {
        yield return new WaitForSeconds(2f);
        dialogueBoxObject.SetActive(true);
        trigger.getManager().StartDialogue(dialogue);
    }
}