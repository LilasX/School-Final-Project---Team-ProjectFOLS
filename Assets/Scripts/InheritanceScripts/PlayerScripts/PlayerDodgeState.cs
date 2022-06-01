using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerDodgeState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void Dodge()
    {
        playerEntityInstance.Animator.SetBool("Dive", true);
        playerEntityInstance.Speed = playerEntityInstance.DodgeSpeed;  //Valeur de la vitesse en mode esquive
        playerEntityInstance.GetCurrentStamina -= 10f;
        playerEntityInstance.DodgeTime += Time.deltaTime;
        if (playerEntityInstance.DodgeTime >= 0.2f)
        {
            playerEntityInstance.IsDodging = false;
            playerEntityInstance.DodgeTime = 0;
            playerEntityInstance.Animator.SetBool("Dive", false);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void OnUpdate()
    {
        Dodge();
    }

    public void ExitState()
    {

    }
}