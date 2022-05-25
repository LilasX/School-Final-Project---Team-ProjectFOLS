using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : IPlayerBaseState
{
    //private GameManager gameManager;

    private PlayerEntity playerEntityInstance;

    public PlayerMeleeState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }

    private void Awake()
    {
        //playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>();
    }

    public void MeleeAttack() //changed to public
    {
        // Using Stick
        if (playerEntityInstance.IsUsingStick)
        {
            if (playerEntityInstance.GetCurrentStamina >= 5)
            {
                playerEntityInstance.Stick.SetActive(true);
                playerEntityInstance.Animator.SetBool("Attack", true);
                Debug.Log("Attack");
                playerEntityInstance.GetCurrentStamina -= 5f;
            }
        }
        else
        {
            playerEntityInstance.Stick.SetActive(false);
            playerEntityInstance.Animator.SetBool("Attack", false);
            playerEntityInstance.MeleePerformed = false;
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
