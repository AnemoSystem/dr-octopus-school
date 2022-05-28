using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour 
{
    public GameObject UIMain;
    public GameObject UIStart;
    public GameObject UIOptions;

    void Start()
    {
        UIMain.SetActive(true);
        UIStart.SetActive(false);
        UIOptions.SetActive(false);
    }

    // ------------------Botao Start------------------ //
    public void ButtonStart() 
    { 
        UIMain.SetActive(false);
        UIStart.SetActive(true);
        UIOptions.SetActive(false);
    } 

    // ------------------Botao Options------------------ //
    public void ButtonOptions() 
    {  
        UIMain.SetActive(false);
        UIStart.SetActive(false);
        UIOptions.SetActive(true);
    } 

    // ------------------Iniciar Jogo------------------ //
    public void StartGame() 
    { 
        SceneManager.LoadScene("MainMap");  
    } 

    // ------------------Botao Return------------------ //
    public void ButtonReturn() 
    {  
        UIMain.SetActive(true);
        UIStart.SetActive(false);
        UIOptions.SetActive(false);
    } 

    // ------------------Sair do Jogo------------------ //
    public void ExitGame() 
    {  
        Application.Quit();  
    }  
}  
