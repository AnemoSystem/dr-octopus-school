using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateMinigameName : MonoBehaviour
{
    public string nameTransition;
    private LoadWithTransition transition;

    void Start() {
        transition = GameObject.Find(nameTransition).GetComponent<LoadWithTransition>();
    }

    public void UpdateMinigame(string whichMinigame) {
        Server.actualMinigame = whichMinigame;
    }

    public void Results() {
        transition.FadeIn("Results");
    }
}