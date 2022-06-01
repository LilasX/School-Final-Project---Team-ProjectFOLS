using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerKnockedState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }


    private void KnockedAnimation()
    {
        playerEntityInstance.GetComponent<PlayerEntity>().Animator.SetTrigger("Knocked");
        playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
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
        KnockedAnimation();
    }

}
