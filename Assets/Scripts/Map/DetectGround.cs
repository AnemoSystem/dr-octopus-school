using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    private bool isDetect;

    void OnMouseDown() {
        isDetect = true;
        //Debug.Log(isDetect);
    }

    void OnMouseUp() {
        isDetect = false;
        //Debug.Log(isDetect);
    }

    public bool getIsDetect() {
        return isDetect;
    }
}
