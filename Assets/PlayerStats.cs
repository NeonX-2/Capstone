using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerStats : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    
    public HealthBar healthBar; 
    void Start()
    {
        health = maxHealth;    
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
