using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager manager;
    public GameObject dialogueBox;

    void Start() {
        manager = FindObjectOfType<DialogueManager>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && Server.canStartDialogue) {
            dialogueBox.SetActive(true);
            if(manager.getActivate())
                manager.DisplayNextSentence();
            else
                manager.StartDialogue(dialogue);
        }
    }

    public DialogueManager getManager() {
        return manager;
    }

    public void SetDialogue(Dialogue d) {
        dialogue = d;
    }
}