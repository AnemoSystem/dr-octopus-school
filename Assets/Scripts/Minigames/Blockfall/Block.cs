using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Color[] states;
    public int health { get; private set; }
    public bool unbreakable;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

   private void Start()
    {
        if (!this.unbreakable)
        {
            this.health = this.states.Length;
            this.spriteRenderer.color = this.states[this.health - 1]; // ele volta a funcionar quando apaga o "- 1", mas fica um pouco bugado
        }
        
    }

    private void Hit()
    {
        if (this.unbreakable) {
            return;
        }

        this.health--;

        if (this.health <= 0) 
        {
            this.gameObject.SetActive(false);
        }

        this.spriteRenderer.color = this.states[this.health - 1];
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
