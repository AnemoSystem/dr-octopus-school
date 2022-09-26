using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLoading : MonoBehaviour
{   
    public GameObject loadingIndicator;
    public GameObject transitionBackground;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    public void FadeIn() {
        loadingIndicator.SetActive(true);
        transitionBackground.SetActive(true);  
        anim.Play("CrossfadeLoading");
    }
}