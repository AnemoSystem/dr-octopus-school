using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMinigame : MonoBehaviour
{
    public LoadWithTransition transition;
    //public string minigameName;

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            transition.FadeIn(SceneManager.GetActiveScene().name);
        }
    }
}