using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager manager;
    
    void Start() {
        manager = FindObjectOfType<DialogueManager>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(manager.getActivate())
                manager.DisplayNextSentence();
            else
                manager.StartDialogue(dialogue);
        }
    }

    public DialogueManager getManager() {
        return manager;
    }
}