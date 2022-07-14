using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductDisplay : MonoBehaviour
{
    public ProductInfo product;

    public Text nameText;
    public Text priceText;
    public Image artworkImage;

    void Start() {
        nameText.text = product.name;
        priceText.text = product.price.ToString();
        artworkImage.sprite = product.artwork;
    }
}