using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Product", menuName="Products/Item")]
public class ProductInfo : ScriptableObject
{
    public int productID;
    public string name;
    public int price;
    public Sprite artwork;
}