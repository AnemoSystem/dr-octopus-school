using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPrefTextHp : MonoBehaviour
{
    public string Sname;

    void Update()
    {
        GetComponent<Text>().text=PlayerPrefs.GetInt("HP")+"";
    }
}
