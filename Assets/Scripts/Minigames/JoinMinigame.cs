using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinMinigame : MonoBehaviour
{
    private ChangeRoom changeRoom;
    public MenuController controller;
    public Button yesButton;

    // Start is called before the first frame update
    void Start()
    {
        changeRoom = GetComponent<ChangeRoom>();
    }

    void OnMouseDown() {
        controller.OpenWindowConfirm();
        yesButton.onClick.AddListener(Open);
    }

    public void RemoveListener() {
        yesButton.onClick.RemoveListener(Open);
    }

    public void Open() {
        changeRoom.ChangeScene();
    }
}