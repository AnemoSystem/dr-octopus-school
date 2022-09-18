using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadWithTransition : MonoBehaviour
{
    private Animator transitionAnim;

    void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }

    public void FadeIn(string nextScene) {
        StartCoroutine(StartTransition(nextScene));
    }

    IEnumerator StartTransition(string sc) {
        transitionAnim.Play("Crossfade_Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sc);
    }

    public void FadeInLevel(string nextScene) {
        StartCoroutine(StartTransitionPhoton(nextScene));
    }

    IEnumerator StartTransitionPhoton(string sc) {
        transitionAnim.Play("Crossfade_Start");
        yield return new WaitForSeconds(1f);
        PhotonNetwork.LoadLevel(sc);
    }
}
