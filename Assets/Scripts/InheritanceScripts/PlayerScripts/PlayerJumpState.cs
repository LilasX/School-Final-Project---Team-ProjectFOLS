using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerBaseState
{
    //private GameManager gameManager;

    private PlayerEntity playerEntityInstance;

    public PlayerJumpState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }

    private void Awake()
    {
        //playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>();
    }

    private void Jump()
    {
        // Gravité
        playerEntityInstance.velocity.y -= playerEntityInstance.GravityForce; //Application de la force de gravité
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.velocity * Time.deltaTime);

        playerEntityInstance.velocity.y = playerEntityInstance.jumpForce; //Application de la force du saut
        playerEntityInstance.IsJumping = false;
        playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        //Debug.Log("IsJumping");
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void OnUpdate()
    {
        Jump();
    }

    public void ExitState()
    {
        return;
    }
}
