using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health { get; private set; }
    public Color[] states;
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

   private void Start()
    {
        this.health = this.states.Lengh;
        this.spriteRenderer.color = this.sates[this.health - 1];
    }

    private void Hit()
    {
        this.health--;

        if (this.health <= 0) 
        {
            this.gameObject.SetActive(false);
        }

        this.spriteRenderer.color = this.sates[this.health - 1];
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
