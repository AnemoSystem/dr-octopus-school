using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadWithTransition : MonoBehaviour
{
    private Animator transitionAnim;
    private GameObject transition;

    void Start()
    {
        transition = transform.GetChild(0).gameObject;
        transitionAnim = transition.GetComponent<Animator>();
        transition.SetActive(true);
        transitionAnim.Play("Crossfade_FadeOut");
    }

    public void FadeIn(string nextScene) {
        StartCoroutine(StartTransition(nextScene));
    }

    IEnumerator StartTransition(string sc) {
        transitionAnim.Play("Crossfade_FadeIn");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sc);
    }

    public void FadeInLevel(string nextScene) {
        StartCoroutine(StartTransitionPhoton(nextScene));
    }

    IEnumerator StartTransitionPhoton(string sc) {
        transitionAnim.Play("Crossfade_FadeIn");
        yield return new WaitForSeconds(0.3f);
        PhotonNetwork.LoadLevel(sc);
    }
}
