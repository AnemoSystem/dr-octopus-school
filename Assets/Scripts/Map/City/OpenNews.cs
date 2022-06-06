using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNews : MonoBehaviour
{
    public GameObject Wind;
    // Update is called once per frame
    public void OpenWind () {
        Wind.SetActive(true);
    }
}
