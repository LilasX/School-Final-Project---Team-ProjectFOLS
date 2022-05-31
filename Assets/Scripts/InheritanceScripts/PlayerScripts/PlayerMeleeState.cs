using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerMeleeState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    public void MeleeAttack() //changed to public
    {
        // Using Stick
        if (playerEntityInstance.IsUsingMelee)
        {
            playerEntityInstance.HasUsedMelee = true;
            playerEntityInstance.Stick.SetActive(true);
            playerEntityInstance.Animator.SetBool("Attack", true);
            Debug.Log("Attack");
        }
        else
        {
            playerEntityInstance.Stick.SetActive(false);
            playerEntityInstance.Animator.SetBool("Attack", false);
            playerEntityInstance.HasUsedMelee = false;
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void ExitState()
    {
        return;
    }

    public void OnUpdate()
    {
        MeleeAttack();
    }
}
