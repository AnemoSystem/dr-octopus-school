using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ListFriends : MonoBehaviour
{
    List<string> friend_names = new List<string>();
    List<string> friend_id = new List<string>();
    List<string> friend_status = new List<string>();

    public GameObject instanceFriend;
    public Transform parent;
    public GameObject[] whichDeactivate;

    void ResetAllArrays() {
        friend_id.Clear();
        friend_names.Clear();
        friend_status.Clear();
    }

    public void StartSearch() {
        Debug.Log("Start Searching!");
        DestroyAllFriends();
        ResetAllArrays();
        StartCoroutine(CoroutineFriends());
    }

    void OnEnable() {
        //Server.username = "jooj";
        StartSearch();
        ActivateGameObjects(false);
    }

    void ActivateGameObjects(bool status) {
        foreach(GameObject g in whichDeactivate)
            g.SetActive(status);
    }

    IEnumerator CoroutineFriends() {
        yield return StartCoroutine(GetFriends(0));
        yield return StartCoroutine(GetFriends(1));
        Debug.Log("All friends were searched with success!");
    }

    void OnDisable() {
        DestroyAllFriends();
    }

    void DestroyAllFriends() {
        int count = parent.childCount;
        for(int i = 0; i < count; i++)
        {
            Transform child = parent.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    IEnumerator GetFriends(int order) {
        WWWForm form = new WWWForm();
        form.AddField("username", Server.username);
        form.AddField("order", order);

        UnityWebRequest www = UnityWebRequest.Post(
            Server.mainServer + "/school-management-system/unity/list_friends.php", 
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
                //Debug.Log(order.ToString() + ": " + result);
                if(result != "error") {
                    string[] friends = result.Split('*');
                    for(int i = 0; i < friends.Length - 1; i++) {
                        string[] t = friends[i].Split('=');
                        for(int j = 0; j < t.Length; j++) {
                            if(j == 0) friend_id.Add(t[j]); 
                            else if(j == 1) friend_names.Add(t[j]);
                            else friend_status.Add(t[j]);
                        }
                    }
                    /*
                    Debug.Log("ID: " + friend_id.Count.ToString());
                    Debug.Log("names: " + friend_names.Count.ToString());
                    Debug.Log("status: " + friend_status.Count.ToString());
                    */
                    for(int i = 0; i < friend_id.Count; i++) {
                        GameObject g = Instantiate(instanceFriend, parent);
                        DisplayListFriend f = g.GetComponent<DisplayListFriend>();

                        if(friend_status[i] == "0") f.SetOffline();
                        else f.SetOnline();

                        f.SetFriendName(friend_names[i]);
                        f.SetID(friend_id[i]);
                        f.SetMenuController(transform.GetChild(3).gameObject);
                    }
                }
                break;
            default:
                break;
        }
    }
}
