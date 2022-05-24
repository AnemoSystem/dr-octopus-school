using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="PlayerShip")
        {
            col.gameObject.GetComponent<Spaceship>().AddCoin();
            Destroy(gameObject);
        }
    }
}
