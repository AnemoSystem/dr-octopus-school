using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManagerTest : MonoBehaviour
{
    public string playerText;

    IEnumerator SendMessage(string text) {
        playerText = text;
        yield return new WaitForSeconds(4f);
        playerText = "";
    }

    public void startMessage(string text) {
        StartCoroutine(SendMessage(text));
    }
}
