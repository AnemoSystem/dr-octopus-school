using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator anim;
    private bool activate;
    public GameObject blackBK;
    [SerializeField]
    private bool isWriting;
    private string lastSentence;

    public UnityEvent OnEndDialogue;
    private float waitInit = 1.2f;

    void Start() {
        sentences = new Queue<string>();
        activate = false;
        isWriting = false;
        blackBK.SetActive(false);
        dialogueText.text = "";
    }

    public void StartDialogue(Dialogue dialogue) {
        Server.canMove = false;
        Debug.Log("Start Conversation with " + dialogue.name);
        anim.SetBool("isOpen", true);
        blackBK.SetActive(true);
        activate = true;
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        dialogueText.text = "";
        StartCoroutine(DelayForAnimation());
        //DisplayNextSentence();
    }

    IEnumerator DelayForAnimation() {
        yield return new WaitForSeconds(waitInit);
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0 && !isWriting) {
            EndDialogue();
            return;
        }
        string sentence = "";
        if(!isWriting) {
            sentence = sentences.Dequeue();
            lastSentence = sentence;
        }
        else sentence = lastSentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, 0.05f));
    }

    IEnumerator TypeSentence(string sentence, float wait) {
        if(!isWriting) {
            dialogueText.text = "";
            isWriting = true;
            foreach(char letter in sentence.ToCharArray()) {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wait);
            }
            isWriting = false;
        }
        else {
            dialogueText.text = sentence;
            isWriting = false;
            yield return null;
        }
    }

    void EndDialogue() {
        waitInit = 0.4f;
        Server.canMove = true;
        activate = false;
        isWriting = false;
        anim.SetBool("isOpen", false);
        blackBK.SetActive(false);
        OnEndDialogue.Invoke();
    }

    public bool getActivate() {
        return activate;
    }
}