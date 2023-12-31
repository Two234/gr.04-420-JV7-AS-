using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    private Rigidbody2D rb;
    private bool isDead;

    public GameManagerScript gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();

    }

    public void TakeDamage(int damage) 
    {
        health -= damage;   
        if (health <= 0 && !isDead) 
        {
            isDead = true;
            gameManager.gameOver();
            Die();
        }
    
    }

    private void Die()
    {
       
        rb.bodyType = RigidbodyType2D.Static;
        
    }
}