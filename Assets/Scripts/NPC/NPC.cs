using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueTrigger manager;

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Encontrou");
        manager.gameObject.SetActive(true);
        manager.SetDialogue(dialogue);
        Server.canStartDialogue = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        Server.canStartDialogue = false;
    }
}