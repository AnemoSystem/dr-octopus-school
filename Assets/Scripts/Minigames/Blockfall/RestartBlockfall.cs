using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartBlockfall : MonoBehaviour
{
    public LoadWithTransition transition;

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            transition.FadeIn("Blockfall");
        }
    }
}