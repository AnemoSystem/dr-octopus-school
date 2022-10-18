using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class DisplayListFriend : MonoBehaviour
{
    private string id;
    public Image statusIcon;
    public Text friend;
    private List<int> bodyPartsSelected = new List<int>();

    public GameObject menuController;
    public SelectMenuFriend controlMenuFriend;

    void Start() {
        controlMenuFriend = menuController.GetComponent<SelectMenuFriend>();
        gameObject.GetComponent<Button>().onClick.AddListener(Open);
    }

    void Open() {
        if(menuController.activeSelf)
            OpenMenuFriend(false);
        else
            OpenMenuFriend(true);
    }

    public void OpenMenuFriend(bool status) {
        menuController.SetActive(status);
        if(status) StartCoroutine(SearchAnotherParts());
    }

    // Setters
    public void SetOnline() {
        statusIcon.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetOffline() {
        statusIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void SetFriendName(string name) {
        friend.text = name;
    }

    public void SetID(string i) {
        id = i;
    }

    public void SetMenuController(GameObject go) {
        menuController = go;
    }

    IEnumerator SearchAnotherParts() {
        WWWForm form = new WWWForm();
        form.AddField("username", friend.text);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/search_actual_menu_itens.php", 
            form
        );

        yield return www.SendWebRequest();
        
        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("Connection Error");
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Data Processing Error");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error");
                break;
            case UnityWebRequest.Result.Success:
                string result = www.downloadHandler.text.ToString();
                string[] parts = result.Split('-');
                for(int i = 0; i < parts.Length; i++) {
                    int a = Convert.ToInt32(parts[i]);
                    bodyPartsSelected.Add(a);
                }
                controlMenuFriend.SetBodyParts(
                    bodyPartsSelected[0],
                    bodyPartsSelected[1],
                    bodyPartsSelected[2],
                    bodyPartsSelected[3]
                );
                controlMenuFriend.SetUsername(friend.text);
                break;
            default:
                break;
        }
    }
}