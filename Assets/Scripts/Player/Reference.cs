using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reference : MonoBehaviour
{
    public Text usernameRef;
    public bool changeColor = false;

    void Start() {
        if(changeColor)
            usernameRef.color = Color.white;
        else
            usernameRef.color = Color.black;
    }
}