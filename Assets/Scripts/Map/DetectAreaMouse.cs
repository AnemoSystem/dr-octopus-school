using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAreaMouse : MonoBehaviour
{
    private bool isDetected;

    void OnMouseDown() {
        isDetected = true;
    }

    void OnMouseUp() {
        isDetected = false;
    }

    public bool getIsDetected() {
        return isDetected;
    }
}