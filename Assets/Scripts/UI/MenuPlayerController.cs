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

    void Start() {
        playerUsername.text = Server.username;
    }

    public void NextPart(int whichPart) {
        playerView.NextPart(whichPart);
        StartCoroutine(UpdatingIcons());
    }

    public void PreviousPart(int whichPart) {
        playerView.PreviousPart(whichPart);
        StartCoroutine(UpdatingIcons());
    }

    IEnumerator UpdatingIcons() {
        playerMenuView.UpdatePlayerBodyParts();
        yield return StartCoroutine(UpdatingBodyParts());
        UpdateIcons();
    }

    IEnumerator UpdatingBodyParts() {
        playerMenuView.UpdatePlayerBodyParts();
        yield return null;
    }
    /*
    void Update() {
        UpdateIcons();
    }
    */
    void UpdateIcons() {
        for(int i = 0; i < icon.Length; i++)
            icon[i].sprite = playerView.spriteRend[i].sprite;
    }
}