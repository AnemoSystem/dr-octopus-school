using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator anim;
    private bool activate;

    void Start() {
        sentences = new Queue<string>();
        activate = false;
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Start Conversation with " + dialogue.name);
        activate = true;
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, 0.05f));
    }

    IEnumerator TypeSentence(string sentence, float wait) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wait);
        }
    }

    void EndDialogue() {
        activate = false;
    }

    public bool getActivate() {
        return activate;
    }
}