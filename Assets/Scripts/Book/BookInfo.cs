using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Book", menuName="Products/Book")]
public class BookInfo : ScriptableObject
{
    public Sprite[] pages;
    public string title;

    public Sprite frontSprite;
    public Sprite openedSprite;
    public Sprite backwardSprite;
}