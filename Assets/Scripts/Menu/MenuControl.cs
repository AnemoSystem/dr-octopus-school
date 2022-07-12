using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour 
{
    public GameObject UIMain;
    public GameObject UIStart;
    public GameObject UIOptions;

    private Animator whichAnimator;

    void Start()
    {
        UIMain.SetActive(true);
        UIStart.SetActive(false);
        UIOptions.SetActive(false);
    }

    IEnumerator TransitionBetweenMenus(GameObject m1, GameObject m2) {
        yield return StartCoroutine(StartTransition(m1, false));
        StartCoroutine(StartTransition(m2, true));
    }

    IEnumerator StartTransition(GameObject g, bool fadeIn) {
        whichAnimator = g.GetComponent<Animator>();
        if(fadeIn) {
            g.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            whichAnimator.Play("Transition_In");
        }
        else {
            whichAnimator.Play("Transition_Out");
            yield return new WaitForSeconds(0.2f);
            g.SetActive(false);
        }
    }

    // ------------------Botao Start------------------ //
    public void ButtonStart() 
    { 
        StartCoroutine(TransitionBetweenMenus(UIMain, UIStart));
        UIOptions.SetActive(false);
    } 

    // ------------------Botao Options------------------ //
    public void ButtonOptions() 
    {  
        StartCoroutine(TransitionBetweenMenus(UIMain, UIOptions));
        UIStart.SetActive(false);
    } 

    // ------------------Iniciar Jogo------------------ //
    public void StartGame() 
    { 
        SceneManager.LoadScene("MainMap");
    } 

    // ------------------Botao Return------------------ //
    public void ButtonReturn() 
    { 
        if(UIOptions.activeSelf) {
            StartCoroutine(TransitionBetweenMenus(UIOptions, UIMain));
            UIStart.SetActive(false);
        }
        else {
            StartCoroutine(TransitionBetweenMenus(UIStart, UIMain));
            UIOptions.SetActive(false);
        }       
    } 

    // ------------------Sair do Jogo------------------ //
    public void ExitGame() 
    {  
        Application.Quit();  
    }  
}  
