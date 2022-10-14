using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayerController : MonoBehaviour
{
    public CustomBodyPart playerView;
    public CustomBodyPartMenu playerMenuView;
    public Text playerUsername;

    public SpriteRenderer[] icon;

    public GameObject[] options;
    public GameObject loading;

    void Start() {
        InvertLoading(false);
        playerUsername.text = Server.username;
    }

    public void InvertLoading(bool status) {
        loading.SetActive(status);
        foreach(GameObject option in options)
            option.SetActive(!status);
    }

    public void NextPart(int whichPart) {
        //playerView.NextPart(whichPart);
        StartCoroutine(DelayUpdateIcons(1, whichPart));
    }

    public void PreviousPart(int whichPart) {
        //playerView.PreviousPart(whichPart);
        StartCoroutine(DelayUpdateIcons(0, whichPart));
    }

    IEnumerator UpdatingIcons(int type, int whichPart) {
        if(type == 0)
            playerView.PreviousPart(whichPart);
        else
            playerView.NextPart(whichPart);
        yield return null;
    }

    IEnumerator DelayUpdateIcons(int type, int whichPart) {
        InvertLoading(true);
        yield return StartCoroutine(UpdatingIcons(type, whichPart));
        InvertLoading(false);
    }

    /*
    IEnumerator UpdatingIcons() {
        playerMenuView.UpdatePlayerBodyParts();
        yield return StartCoroutine(UpdatingBodyParts());
        UpdateIcons();
    }

    IEnumerator UpdatingBodyParts() {
        playerMenuView.UpdatePlayerBodyParts();
        yield return null;
    }
    */

    void Update() {
        UpdateIcons();
    }
    
    void UpdateIcons() {
        // sim, isso é feio, mas é como eu consegui fazer :p
        for(int i = 0; i < icon.Length; i++)
            icon[i].sprite = playerView.spriteRend[i].sprite;
    }
}