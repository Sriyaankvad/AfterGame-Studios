using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public string[] attackNames = new string[16];

    public Transform attackPoint;
    public float[] attackRadius;
    public float[] attackDamage = new float[16];
    public float[] attackSpeed = new float[16];
    public Vector2[] knockback = new Vector2[16];
    public bool[] freezesEnemy = new bool[16];
    public float knockbackMultiplier;
    public LayerMask Enemy;
    public float attackStartDelay;
    public float attackFinishDelay;
    public bool attackInProgress = false;
    public bool multihitAttackInProgress = false;
    int forwardGroundedCounter = 0;
    int forwardAerialCounter = 0;

    public void Attack(float attackIndex)
    {
        float attackToExecute = attackIndex;
        if(attackIndex == 0)
        {
            multihitAttackInProgress = (forwardGroundedCounter != 2);
            attackToExecute += forwardGroundedCounter;
            forwardGroundedCounter = (forwardGroundedCounter + 1) % 3;
        } else if(attackIndex == 8)
        {
            multihitAttackInProgress = (forwardAerialCounter != 2);
            attackToExecute += forwardAerialCounter;
            forwardAerialCounter = (forwardAerialCounter + 1) % 3;
        } else
        {
            forwardGroundedCounter = 0;
            forwardAerialCounter = 0;
        }

        if(!attackInProgress)
        {
            StartCoroutine(ExecuteDelayedAttack(attackToExecute, attackStartDelay, multihitAttackInProgress ? 0 : attackFinishDelay));
            print("Executed " + attackNames[(int)attackToExecute]);
        }

    }

    public IEnumerator ExecuteDelayedAttack(float attackIndex, float aSD, float aFD)
    {
        attackInProgress = true;
        yield return new WaitForSeconds(aSD / 1000);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius[(int)attackIndex], Enemy);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (freezesEnemy[(int)attackIndex])
            {
                enemy.gameObject.GetComponent<Enemy>().ApplyDamage((attackDamage[(int)attackIndex]), new Vector2(0, 0));
                enemy.gameObject.GetComponent<Enemy>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
            }
            else
            {
                enemy.gameObject.GetComponent<Enemy>().ApplyDamage(attackDamage[(int)attackIndex], knockback[(int)attackIndex] * knockbackMultiplier);
            }
            print("Hit " + enemy.name);
        }

        yield return new WaitForSeconds(aFD / 1000);
        attackInProgress = false;
        yield return null;
    }

}
