using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : IPlayerBaseState
{
    private GameManager gameManager;
    private UIManager uiManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;


    public PlayerMeleeState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.Instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    public void MeleeAttack() //changed to public
    {
        // Using Stick
        //if (playerEntityInstance.IsUsingMelee)
        //{
        uiManager.SwordImage.SetActive(false);
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.MeleeVelocity * playerEntityInstance.meleeSpeed * Time.deltaTime);
        playerEntityInstance.HasUsedMelee = true;
        playerEntityInstance.Animator.SetBool("Attack", true);
        playerEntityInstance.meleeTime += Time.deltaTime;
        //}
        if (playerEntityInstance.meleeTime >= 0.633f)
        {
            playerEntityInstance.Animator.SetBool("Attack", false);
            playerEntityInstance.meleeTime = 0f;
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
        //else
        //{
        //    playerEntityInstance.Stick.SetActive(false);
        //    playerEntityInstance.Animator.SetBool("Attack", false);
        //    playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        //}
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
