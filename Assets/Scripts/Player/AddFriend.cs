using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFriend : MonoBehaviour
{
    public MenuController controller;

    [SerializeField]
    private Text friendName;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Collider2D col = hit.collider;
            //Debug.Log(hit.collider.gameObject.name);
            if (col != null && col.tag == "Player" && col.gameObject.name != Server.username) {
                CustomBodyPart cs = col.gameObject.transform.GetChild(4).gameObject.GetComponent<CustomBodyPart>();                
                friendName = col.gameObject.GetComponent<Reference>().usernameRef;
                controller.OpenMenuFriend(cs, friendName.text);
            }
        }
    }
}