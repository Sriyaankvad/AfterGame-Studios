using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 5;
    float currentHealth;
    public Rigidbody2D rigidbody;

    private void Awake()
    {
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamage(float damage, Vector2 knockback)
    {

        currentHealth -= damage;
        rigidbody.AddForce(knockback);
        
        if(currentHealth <= 0)
        {

            print("Enemy died");
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(gameObject);

        }

    }

}
