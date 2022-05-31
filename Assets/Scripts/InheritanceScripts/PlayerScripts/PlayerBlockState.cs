using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerBlockState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void UseShield()
    {
   
            //playerEntityInstance.Shield.SetActive(true);
            playerEntityInstance.Animator.SetBool("Block", true);
            playerEntityInstance.Speed = 0f;

        if (!playerEntityInstance.IsUsingShield)
        {
            //playerEntityInstance.Shield.SetActive(false);
            playerEntityInstance.Speed = playerEntityInstance.ResetSpeedValue;
            playerEntityInstance.Animator.SetBool("Block", false);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void OnUpdate()
    {
        UseShield();
    }

    public void ExitState()
    {
        return;
    }
}
