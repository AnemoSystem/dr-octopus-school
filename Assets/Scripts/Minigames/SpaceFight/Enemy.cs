using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject bullet,explosion,coin;
    public Color bulletcolor;

    public float xSpeed,ySpeed;
    public int score;
    public bool canShoot;
    public float fireRate;
    public float health;

    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject,10);
        if(!canShoot) return;
        fireRate = fireRate+(Random.Range(fireRate/-2,fireRate/2));
        InvokeRepeating("Shoot",fireRate,fireRate);
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed,ySpeed*-1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="PlayerShip")
        {
            col.gameObject.GetComponent<Spaceship>().Damage();
            Die();
        }

        if(col.gameObject.name == "Collider") {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        if((int)Random.Range(0,3)==0)
            Instantiate(coin,transform.position,Quaternion.identity);
        Instantiate(explosion,transform.position,Quaternion.identity);
        PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+score);
        Destroy(gameObject);
    }

    public void Damage()
    {
        health--;
        if(health==0)
            Die();
    }

    void Shoot()
    {
        GameObject temp = (GameObject) Instantiate(bullet,transform.position,Quaternion.identity);
        temp.GetComponent<Bullet>().ChangeDirection();
        temp.GetComponent<Bullet>().ChangeColor(bulletcolor);
    }

}