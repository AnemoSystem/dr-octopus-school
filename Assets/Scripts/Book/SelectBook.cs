using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBook : MonoBehaviour
{
    public BookDisplay display;
    public BookInfo info;

    public void SetInfoForDisplay() {
        display.info = info;
    }
}