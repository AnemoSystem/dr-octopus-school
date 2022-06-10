using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBodyPart : MonoBehaviour
{
    public int nSkin;
    public Skins[] skins;
    SpriteRenderer spriteRend;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(nSkin > skins.Length - 1) nSkin = 0;
        else if (nSkin < 0) nSkin = skins.Length - 1;
    }

    void LateUpdate()
    {
        SkinChoice();
    }

    void SkinChoice() {
        string spriteName = spriteRend.sprite.name;
        spriteName = spriteName.Replace("hair-0_", "");
        int nSprite = int.Parse(spriteName);

        spriteRend.sprite = skins[nSkin].sprites[nSprite];
    }
}

[System.Serializable]
public struct Skins {
    public Sprite[] sprites;
}