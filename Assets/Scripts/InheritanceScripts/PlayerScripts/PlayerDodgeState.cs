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

    }

    private void Dodge()
    {
        playerEntityInstance.hasExecutedDodge = true;
        playerEntityInstance.Speed = playerEntityInstance.DodgeSpeed;  //Valeur de la vitesse en mode esquive
        playerEntityInstance.Animator.SetBool("Dive", true);
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.DodgeVelocity * playerEntityInstance.Speed * Time.deltaTime);
        playerEntityInstance.DodgeTime += Time.deltaTime;
        if (playerEntityInstance.DodgeTime >= 0.3f)
        {
            playerEntityInstance.IsDodging = false;
            playerEntityInstance.DodgeTime = 0;
            playerEntityInstance.Animator.SetBool("Dive", false);
            //gameManager.inputManager.OnEnable();
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
