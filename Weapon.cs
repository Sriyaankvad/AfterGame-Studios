using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public string[] attackNames = new string[12]; // names of attacks, can use for debug or weapon flavoring

    public Transform attackPoint; // where the attack is generated
    public float[] attackRadius; // reach of each attack
    public float[] attackDamage = new float[12]; // how much damage each attack does
    public float[] attackSpeed = new float[12]; // how much time player has to wait before attacking again for each attack
    public bool[] attackFreezesPlayer = new bool[12]; // if each attack freezes the player in place
    public bool[] attackFreezesEnemy = new bool[12]; // if each attack freezes the enemy in place
    public Vector2[] knockback = new Vector2[12]; // enemy knockback for each attack
    public LayerMask Enemy;

    public Rigidbody2D rigidbody; // player rigidbody

    public float Attack(float attackIndex)
    {

        print("Executed " + attackNames[(int)attackIndex]); // debug

        if (attackFreezesPlayer[(int)attackIndex])
        {

            rigidbody.velocity = new Vector2(0, 1);

        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius[(int)attackIndex], Enemy); // generates a circle w/ center attackPoint and radius attackRadius as a hitbox

        //runs through each hit enemy and assigns knockback
        foreach(Collider2D enemy in hitEnemies)
        {

            Vector2 kb = knockback[(int)attackIndex];
            // if the attack freezes the enemy, sets the enemy's vel to (0,1)
            if (attackFreezesEnemy[(int)attackIndex])
            {

                enemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);

            }
            enemy.gameObject.GetComponent<Enemy>().ApplyDamage(attackDamage[(int) attackIndex], knockback[(int)attackIndex]);
            print("Hit " + enemy.name); // debug

        }

        return attackSpeed[(int)attackIndex];

    }

}
