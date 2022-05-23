using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPrefTextCoin : MonoBehaviour
{
    public string Sname;

    void Update()
    {
        GetComponent<Text>().text=PlayerPrefs.GetInt("Coins")+"";
    }
}
