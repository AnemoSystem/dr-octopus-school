using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    int dir=1;
    public float speed = 5;

    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject,5);
    }

    public void ChangeDirection()
    {
        dir*=-1;
    }

    public void ChangeColor(Color col)
    {
        GetComponent<SpriteRenderer>().color=col;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * dir);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(dir==1)
        {
            if(col.gameObject.tag=="EnemyShip")
            {
                col.gameObject.GetComponent<Enemy>().Damage();
                Destroy(gameObject);
            }
        }
        else
        {
            if(col.gameObject.tag=="PlayerShip")
            {
                col.gameObject.GetComponent<Spaceship>().Damage();
                Destroy(gameObject);
            }
        }
        
        if(col.gameObject.name == "Collider") {
            Destroy(gameObject);
        }
    }
}
