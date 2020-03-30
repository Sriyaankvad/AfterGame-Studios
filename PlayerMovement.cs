using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D player; // the controller
    public Weapon weapon; // player's weapon

    public float runSpeed = 40f;  // speed of the player

    float horizontalMove = 0f; // stores the distance that the player will move

    bool jump = false; // stores whether the player wants to jump or not
    bool dash = false; // stores whether the player wants to dash or not
    bool attack = false; // stores whether the player wants to attack or not

    float attackIndex = 0; // which attack the player does
    float nextAttackTime = 0f;

    public float dashAttackWindow = 5; // # of frames player has to both input a dash and an attack
    bool[] wasDashing;
    bool canDashAttack = true;

    private void Start()
    {
        wasDashing = new bool[(int)dashAttackWindow];
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // negative if the player is moving left, positive if moving right

        // sets dash to true if space (temporary button placement) is pressed
        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }

        // sets jump to true if up is pressed
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //attack checks
        if (Input.GetButtonDown("ForwardAttack"))
        {

            attack = true;
            if (player.grounded()) {
                if (dashedInWindow() && player.CanDash() && canDashAttack) {
                    attackIndex = 3;
                    canDashAttack = false;
                } else {
                    attackIndex = 0;
                }
            } else {
                if (dashedInWindow() && player.CanDash() && canDashAttack) {
                    attackIndex = 9;
                    canDashAttack = false;
                } else {
                    attackIndex = 6;
                }
            }

        }

        if (Input.GetButtonDown("UpwardAttack"))
        {

            attack = true;
            if (player.grounded())
            {
                if (dashedInWindow() && player.CanDash() && canDashAttack)
                {
                    attackIndex = 4;
                    canDashAttack = false;
                }
                else
                {
                    attackIndex = 1;
                }
            }
            else
            {
                if (dashedInWindow() && player.CanDash() && canDashAttack)
                {
                    attackIndex = 10;
                    canDashAttack = false;
                }
                else
                {
                    attackIndex = 7;
                }
            }

        }

        if (Input.GetButtonDown("DownwardAttack"))
        {

            attack = true;
            if (player.grounded())
            {
                if (dashedInWindow() && player.CanDash() && canDashAttack)
                {
                    attackIndex = 5;
                    canDashAttack = false;
                }
                else
                {
                    attackIndex = 2;
                }
            }
            else
            {
                if (dashedInWindow() && player.CanDash() && canDashAttack)
                {
                    attackIndex = 11;
                    canDashAttack = false;
                }
                else
                {
                    attackIndex = 8;
                }
            }

        }

    }

    // called everytime the OnLandEvent occurs
    public void OnLand()
    {
        player.Land();
        canDashAttack = true;
    }

    void FixedUpdate()
    {
        updateDashingWindow();
        player.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);

        // if the player inputs an attack & can attack again
        if(attack && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + (1f / weapon.Attack(attackIndex)); // set next attack time to current time + (1 / attack speed)
        }

        jump = false;
        dash = false;
        attack = false;

    }

    public void ApplyDamage(float damage)
    {



    }

    // checks if the player dashed in the dashing window
    public bool dashedInWindow()
    {

        foreach(bool dashed in wasDashing)
        {
            if (dashed) return true;
        }
        return false;

    }

    // shifts data one cell to the left, and sets the last cell's value to the current dash value
    public void updateDashingWindow()
    {

        for(int i = 0; i < wasDashing.Length - 1; i++)
        {

            wasDashing[i] = wasDashing[i + 1];

        }

        wasDashing[wasDashing.Length - 1] = dash;

    }

}