using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CustomBodyPart : MonoBehaviour, Photon.Pun.IPunObservable
{
    public int idSkin;
    public int idLegs;
    public int idTorso;
    public int idHair;

    public BodyPart[] skins;
    public BodyPart[] legs;
    public BodyPart[] torso;
    public BodyPart[] hair;

    public SpriteRenderer[] spriteRend;
    PhotonView view;

    void Start() {
        view = GetComponent<PhotonView>();
        spriteRend[0] = this.transform.parent.gameObject.GetComponent<SpriteRenderer>();
        spriteRend[1] = this.transform.parent.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        spriteRend[2] = this.transform.parent.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRend[3] = this.transform.parent.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
    } 

    void Update() {
        if(idSkin > skins.Length - 1) idSkin = 0;
        else if (idSkin < 0) idSkin = skins.Length - 1;

        if(idLegs > legs.Length - 1) idLegs = 0;
        else if (idLegs < 0) idLegs = legs.Length - 1;
        
        if(idTorso > torso.Length - 1) idTorso = 0;
        else if (idTorso < 0) idTorso = torso.Length - 1;

        if(idHair > hair.Length - 1) idHair = 0;
        else if (idHair < 0) idHair = hair.Length - 1; 
    }

    void LateUpdate()
    {
        UpdateAllChoice();
    }

    void UpdateAllChoice() {
        SkinChoice(0);
        SkinChoice(1);
        SkinChoice(2);
        SkinChoice(3);
    }

    public void SkinChoice(int part) {
        string spriteName = "";
        
        string bp = "";
        
        switch(part) {
            case 0:
                spriteName = spriteRend[0].sprite.name;
                bp = "player-map-0_";
                break;
            case 1:
                spriteName = spriteRend[1].sprite.name;
                bp = "torso-0_";
                break;
            case 2:
                spriteName = spriteRend[2].sprite.name;
                bp = "hair-0_";
                break;
            case 3:
                spriteName = spriteRend[3].sprite.name;
                bp = "legs-0_";
                break;
            default:
                break;
        }
        
        spriteName = spriteName.Replace(bp, "");
        int idSprite = int.Parse(spriteName);

        switch(part) {
            case 0:
                spriteRend[0].sprite = skins[idSkin].sprites[idSprite];
                break;
            case 1:
                spriteRend[1].sprite = torso[idTorso].sprites[idSprite];
                break;
            case 2:
                spriteRend[2].sprite = hair[idHair].sprites[idSprite];
                break;
            case 3:
                spriteRend[3].sprite = legs[idLegs].sprites[idSprite];
                break;
            default:
                break;
        }    
    }

    public void NextPart(int whichPart) {
        switch(whichPart) {
            case 0:
                idSkin++;
                break;
            case 1:
                idTorso++;
                break;
            case 2:
                idHair++;
                break;
            case 3:
                idLegs++;
                break;
            default:
                break;
        }
    }

    public void PreviousPart(int whichPart) {
        switch(whichPart) {
            case 0:
                idSkin--;
                break;
            case 1:
                idTorso--;
                break;
            case 2:
                idHair--;
                break;
            case 3:
                idLegs--;
                break;
            default:
                break;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            stream.SendNext(idSkin);
            stream.SendNext(idTorso);
            stream.SendNext(idHair);
            stream.SendNext(idLegs);
        } else if(stream.IsReading) {
            idSkin = (int)stream.ReceiveNext();
            idTorso = (int)stream.ReceiveNext();
            idHair = (int)stream.ReceiveNext();
            idLegs = (int)stream.ReceiveNext();
        }
    }
}

[System.Serializable]
public struct BodyPart {
    public string name;
    public Sprite[] sprites;
}