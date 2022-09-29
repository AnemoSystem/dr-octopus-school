using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAccessDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;
    public Dialogue dialogue;
    public GameObject dialogueBoxObject;
    public GameObject blackBK;

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

    public void StopDialogueBox() {
        //StartCoroutine(Stopping());
        
        Destroy(trigger.gameObject);
        Destroy(blackBK);
        Destroy(trigger.getManager().gameObject);    
    }

    IEnumerator Stopping() {
        yield return new WaitForSeconds(1f);
        Destroy(trigger.gameObject);
        Destroy(blackBK);
        Destroy(trigger.getManager().gameObject);
    }
}