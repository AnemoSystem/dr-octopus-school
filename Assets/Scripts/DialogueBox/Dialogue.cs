using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    // Trigger myTrigger = JsonUtils.ImportJson<Trigger>("Json/enter"));

    /*
    private void SetText(int id) {
        name = myTrigger.name;
        sentence[id] = myTrigger.sentence;
    }
    */
}