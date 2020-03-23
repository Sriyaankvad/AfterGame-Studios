using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D player; // the controller

    public float runSpeed = 40f;  // speed of the player

    float horizontalMove = 0f; // stores the distance that the player will move

    bool jump = false; // stores whether the player wants to jump or not
    bool dash = false; // stores whether the player wants to dash or not

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // negative if the player is moving left, positive if moving right

        // sets dash to true if space (temporary button placement) is pressed
        if (Input.GetButtonDown("Dash")) {
            dash = true;
        }

        // sets dash to true if up is pressed
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    // called everytime the OnLandEvent occurs
    public void OnLand()
    {
        player.Land();
    }

    void FixedUpdate()
    {
        player.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        jump = false;
        dash = false;
    }
}
