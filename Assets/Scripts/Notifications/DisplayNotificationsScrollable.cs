using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNotificationsScrollable : MonoBehaviour
{
    public GameObject template;
    public Transform parent;

    void Start() {
        for(int i = 0; i < 5; i++) {
            Instantiate(template, parent);
        }
    }
}
