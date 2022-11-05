using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    int delay=0;
    GameObject G1,G2;
    public GameObject bullet,explosion;
    public GameObject hud;
    public GameObject gameOver;

    Rigidbody2D rb;
    public float speed;
    int health = 3;
    int coin = 0;

    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        G1=transform.Find("G1").gameObject;
        G2=transform.Find("G2").gameObject;
    }

    void Start()
    {
        PlayerPrefs.SetInt("Score",0);
        PlayerPrefs.SetInt("Coins",0);
    }

    void Update()
    {
        PlayerPrefs.SetInt("HP",health);
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*speed,0));
        rb.AddForce(new Vector2(0,Input.GetAxis("Vertical")*speed));
        if(Input.GetKey(KeyCode.Space) && delay>90)
            Shoot();
        delay++;
    }

    public void Damage()
    {
        health--;
        StartCoroutine(Blink());
        if(health==0)
        {
            hud.SetActive(false);
            gameOver.SetActive(true);
            Destroy(gameObject);
            Instantiate(explosion,transform.position,Quaternion.identity);

        }
    }

    IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().color= new Color(1,0,0);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color= new Color(1,1,1);
    }

    void Shoot()
    {
        delay=0;
        Instantiate(bullet,G1.transform.position,Quaternion.identity);
        Instantiate(bullet,G2.transform.position,Quaternion.identity);
    }

    public void AddCoin()
    {
        coin++;
        PlayerPrefs.SetInt("Coins",coin);
    }
}