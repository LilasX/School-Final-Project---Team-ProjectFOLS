using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerStateMachine playerState;
    private PlayerEntity playerEntityInstance;

    public PlayerDeathState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void PlayerDeath()
    {
        playerEntityInstance.OnDeath();
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
        PlayerDeath();
    }

    public void ExitState()
    {
        return;
    }

    public void OnUpdate()
    {
        
    }


}
