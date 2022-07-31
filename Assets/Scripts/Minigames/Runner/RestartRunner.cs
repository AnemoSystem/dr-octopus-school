using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartRunner : MonoBehaviour
{
    public LoadWithTransition transition;

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            transition.FadeIn("Runner");
        }
    }
}