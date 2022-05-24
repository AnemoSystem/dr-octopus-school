using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    public float time;
    void Start()
    {
        Destroy(gameObject,time);
    }
}