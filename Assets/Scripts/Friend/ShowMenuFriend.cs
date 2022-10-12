using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenuFriend : MonoBehaviour
{
    public CustomBodyPart customBody;
    private CustomBodyPart friend;
    public Text friendName;

    void Update() {
        SearchParts();
    }
    
    void SearchParts() {
        customBody.idSkin = friend.idSkin;
        customBody.idLegs = friend.idLegs;
        customBody.idTorso = friend.idTorso;
        customBody.idHair = friend.idHair;
    }

    public void SetFriend(CustomBodyPart cs) {
        friend = cs;
    }

    public void SetFriendName(string name) {
        friendName.text = name;
    }
}