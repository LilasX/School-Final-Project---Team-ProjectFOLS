using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    //private Vector3 playerVelocity;
    //private Quaternion playerRotation;

    public PlayerDodgeState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
        //playerVelocity = playerEntityInstance.Move.normalized;
    }

    private void Dodge()
    {
        //playerEntityInstance.MyCharacter.Move(new Vector3 (playerVelocity.x, 0, playerVelocity.z) * Time.deltaTime);
        //playerEntityInstance.transform.rotation = Quaternion.Euler(Vector3.right);

        playerEntityInstance.Animator.SetBool("Dive", true);
        playerEntityInstance.Speed = playerEntityInstance.DodgeSpeed;  //Valeur de la vitesse en mode esquive
        //playerEntityInstance.GetCurrentStamina -= 5f;
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
