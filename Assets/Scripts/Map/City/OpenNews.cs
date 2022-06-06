using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNews : MonoBehaviour
{
    public GameObject Wind;
    public GameObject Close;
    public void OpenWind () {
        Wind.SetActive(true);
    }

    public void CloseWind () {
        Wind.SetActive (false);
    }
}
