using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerJumpState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void Jump()
    {
        //// Gravité
        //playerEntityInstance.velocity.y -= playerEntityInstance.GravityForce; //Application de la force de gravité
        //playerEntityInstance.MyCharacter.Move(playerEntityInstance.velocity * Time.deltaTime);


        playerEntityInstance.velocity.y = playerEntityInstance.jumpForce; //Application de la force du saut
        playerEntityInstance.Animator.SetBool("Jump", true);
        playerEntityInstance.IsJumping = false;
        //Debug.Log("IsJumping");
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void OnUpdate()
    {
        if(playerEntityInstance.IsJumping)
        {
            Jump();
        }
        else
        {
            playerEntityInstance.Animator.SetBool("Jump", false);
            playerEntityInstance.Animator.SetBool("Fall", true);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }

        if(playerEntityInstance.MyCharacter.isGrounded)
        {
            playerEntityInstance.Animator.SetBool("Fall", false);
        }
    }

    public void ExitState()
    {
        return;
    }
}
